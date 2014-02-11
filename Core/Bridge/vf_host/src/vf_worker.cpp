#include "vf_worker.h"
#include "vf_task.h"
#include "vf_xml.h"
#include "vf_stack.h"
#include "vf_envelope.h"

namespace vapula
{
	Worker::Worker()
	{
		_Task = null;
		_Invoker = null;
		_StageTime = new float[STAGE_MAX];
		for(int i = 0; i < STAGE_MAX; i++)
			_StageTime[i] = 0;
	}

	Worker::~Worker() 
	{
		Clear(_StageTime, true);
		Clear(_Invoker);
	}

	Task* Worker::GetTask()
	{
		return _Task;
	}

	Stack* Worker::GetStack()
	{
		return _Invoker->GetStack();
	}

	void Worker::SetStageTime(int stage, float time)
	{
		_StageTime[stage] = time;
	}

	float Worker::GetStageTime(int stage)
	{
		return _StageTime[stage];
	}

	int Worker::Run(Task* task)
	{
		_Task = task;

		LARGE_INTEGER freq,t1,t2;
		QueryPerformanceFrequency(&freq);

		QueryPerformanceCounter(&t1);
		if(!_OnPrepare() || !OnPrepare())
			return VFH_WORKER_PREPARE;
		QueryPerformanceCounter(&t2);
		SetStageTime(VFH_WORKER_PREPARE, (t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart);

		QueryPerformanceCounter(&t1);
		if(!OnProcess())
			return VFH_WORKER_PROCESS;
		QueryPerformanceCounter(&t2);
		SetStageTime(VFH_WORKER_PROCESS, (t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart);

		return VFH_WORKER_NORMAL;
	}

	bool Worker::_OnPrepare()
	{
		Library* lib = _Task->GetLibrary();
		_Invoker = lib->CreateInvoker(_Task->GetMethodId());
		Stack* stack = _Invoker->GetStack();
		Envelope* env = stack->GetEnvelope();

		XML* xml  = XML::Load(_Task->GetDataPath());
		Handle autop_xml(xml);
		if(xml == null)
		{
			ShowMsgbox("Fail to load data file.", _vf_host);
			return false;
		}
		object xdoc = xml->GetEntity();
		object xe_root = XML::XElem(xdoc, "data");

		object xe_param = XML::XPath(xe_root, 2, "params", "param");
		while (xe_param)
		{
			int pid = XML::ValInt(XML::XAttr(xe_param, "id"));
			pcstr pv = XML::ValStr(xe_param);
			env->CastWriteValue(pid, pv);
			delete pv;
			xe_param = XML::Next(xe_param);
		}

		//TODO: load tags

		return true;
	}
}