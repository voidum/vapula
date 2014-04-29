#include "vf_worker.h"
#include "vf_task.h"
#include "vf_xml.h"
#include "vf_stack.h"
#include "vf_dataset.h"
#include <fstream>

#pragma warning(disable: 4127)

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
		_StageTime[stage - 1] = time;
	}

	float Worker::GetStageTime(int stage)
	{
		return _StageTime[stage - 1];
	}

	int Worker::Run(Task* task)
	{
		_Task = task;

		LARGE_INTEGER freq,t1,t2;
		float time = 0.0f;
		QueryPerformanceFrequency(&freq);

		for (int stage = VFH_WORKER_PREPARE; true; stage++)
		{
			bool ret = false;
			QueryPerformanceCounter(&t1);
			switch(stage)
			{
			case VFH_WORKER_PREPARE:
				ret = OnPrepare();
				break;
			case VFH_WORKER_PROCESS:
				ret = OnProcess();
				break;
			case VFH_WORKER_FINISH:
				ret = OnFinish();
				break;
			default:
				return VFH_RETURN_NORMAL;
			}
			QueryPerformanceCounter(&t2);
			time = (t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart;
			SetStageTime(stage, time);
			if(!ret)
				return stage;
		}
	}
	
	bool Worker::OnPrepare()
	{
		Library* lib = _Task->GetLibrary();
		_Invoker = lib->CreateInvoker(_Task->GetMethodId());
		Stack* stack = _Invoker->GetStack();
		Dataset* ds = stack->GetDataset();

		pcstr path_data = _Task->GetDataPath();
		XML* xml  = XML::Load(path_data);
		Scoped autop_xml(xml);
		delete path_data;
		if(xml == null)
		{
			ShowMsgbox("Fail to load data file.", _vf_host);
			return false;
		}
		raw xdoc = xml->GetEntity();
		raw xe_root = XML::XElem(xdoc, "data");

		raw xe_param = XML::XPath(xe_root, 2, "params", "param");
		while (xe_param)
		{
			int id = XML::ValInt(XML::XAttr(xe_param, "id"));
			pcstr value = XML::ValStr(xe_param);
			(*ds)[id]->Write(value);
			delete value;
			xe_param = XML::Next(xe_param);
		}

		//TODO: load tags

		return true;
	}

	bool Worker::OnFinish()
	{
		Stack* stack = _Invoker->GetStack();
		Envelope* env = stack->GetEnvelope();

		ostringstream oss;
		oss<<"<?xml version=\"1.0\" encoding=\"utf-8\"?>";
		oss<<"\n<data>\n<params>";
		for(int i = 1; i <= env->GetTotal(); i++) 
		{
			if(env->GetMode(i) != VF_PM_IN)
			{
				oss<<"\n<param id=\"";
				pcstr v = str::Value(i);
				oss<<(pcstr)(Handle((object)v).Get());
				oss<<"\">";
				oss<<env->CastRead(i);
				oss<<"</param>";
			}
		}
		oss<<"\n</params>\n</data>";
		pcstr xml = str::Encode(oss.str().c_str(), null, _vf_msg_cp);

		std::ofstream out;
		pcstr path = _Task->GetDataPath();
		out.open(path, std::ios::out | std::ios::binary);
		out<<xml;
		out.close();
		delete path;
		return true;
	}
}