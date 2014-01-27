#include "vf_task.h"
#include "vf_worker.h"
#include "vf_utility.h"
#include "vf_xml.h"
#include "vf_driver.h"
#include "vf_stack.h"
#include "vf_library.h"
#include "vf_function.h"
#include "vf_context.h"
#include "vf_envelope.h"

namespace vapula
{
	Task::Task()
	{
		_Library = null;
		_FunctionId = null;
		_StageTime = new float[3];
		for(int i=0; i<3; i++)
			_StageTime[i] = 0;
		_CtrlMode = VF_HOST_CJ_NULL;
		_CtrlSetting = null;
	}

	Task::~Task()
	{
		Clear(_Library);
		Clear(_StageTime, true);
		Clear(_CtrlSetting);
	}

	void Task::SetStageTime(int stage, float time)
	{
		_StageTime[stage] = time;
	}

	float Task::GetStageTime(int stage)
	{
		return _StageTime[stage];
	}

	Library* Task::GetLibrary()
	{
		return _Library;
	}

	cstr8 Task::GetFunctionId()
	{
		return _FunctionId;
	}

	Invoker* Task::GetInvoker()
	{
		return _Invoker;
	}

	int Task::GetCtrlMode() 
	{
		return _CtrlMode; 
	}

	cstr8 Task::GetCtrlSetting() 
	{
		return _CtrlSetting; 
	}

	Task* Task::Parse(cstr8 path)
	{
		Scoped<XML> xml(XML::Load(path));
		if(xml.empty())
		{
			ShowMsgbox("Fail to load task file.", _vf_host);
			return null;
		}
		object xdoc = xml->GetEntity();
		
		Task* task = new Task();
		object xe_root = XML::XElem(xdoc, "task");
		object xe_target = XML::XElem(xe_root, "target");
		object xe_ext = XML::XElem(xe_root, "extension");

		DriverHub* driver_hub = DriverHub::GetInstance();
		astr8 driver_id(XML::ValCh8(XML::XElem(xe_target, "runtime")));
		if(!driver_hub->Link(driver_id.get()))
		{
			ShowMsgbox("Fail to link driver.", _vf_host);
			return null;
		}

		astr8 path_lib_utf8(XML::ValCh8(XML::XElem(xe_target, "path")));
		astr8 path_lib(str::EncodeCh8(path_lib_utf8.get(), _vf_msg_cp, null));
		task->_Library = Library::Load(path);
		if(task->_Library == null)
		{
			ShowMsgbox("Fail to load library.", _vf_host);
			return null;
		}

		if(!task->_Library->Mount())
		{
			ShowMsgbox("Fail to mount library", _vf_host);
			return null;
		}

		task->_FunctionId = XML::ValCh8(XML::XElem(xe_target, "function"));
		task->_Invoker = task->_Library->CreateInvoker(task->_FunctionId);
		
		Stack* stack = task->_Invoker->GetStack();
		Envelope* env = stack->GetEnvelope();
		object xe_param = XML::XPath(xe_target, 2, "params", "param");
		while (xe_param)
		{
			int pid = XML::ValInt(XML::XAttr(xe_param, "id"));
			cstr8 pv = XML::ValCh8(xe_param);
			env->CastWriteValue(pid, pv);
			delete pv;
			xe_param = XML::Next(xe_param);
		}
		//TODO: output params for validation?

		object xe_ctrl_mode = XML::XPath(xe_ext, 2, "control", "mode");
		object xe_ctrl_setting = XML::XPath(xe_ext, 2, "control", "setting");
		task->_CtrlMode = XML::ValInt(xe_ctrl_mode);
		task->_CtrlSetting = XML::Print(xe_ctrl_setting);
		return task;
	}

	bool Task::RunAs(Worker* worker)
	{
		LARGE_INTEGER freq,t1,t2;
		QueryPerformanceFrequency(&freq);
		QueryPerformanceCounter(&t1);
		int ret_worker = worker->Run(this);
		QueryPerformanceCounter(&t2);

		bool ret = false;
		if(ret_worker == VF_HOST_RETURN_NORMAL)
		{
			Stack* stack = _Invoker->GetStack();
			int ret_task = stack->GetContext()->GetReturnCode();
			ostringstream oss;
			oss<<"Vapula host has done with task:\n";
			oss<<_Library->GetLibraryId()<<"=>"<<_FunctionId;
			oss<<"\nReturn code:"<<ret_task;
			oss<<"\nElapsed time:"<<((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart)<<"(s)";
			ShowMsgbox(oss.str().c_str(), _vf_host);
			ret = true;
		}
		else if(ret_worker > VF_HOST_RETURN_NORMAL)
		{
			ostringstream oss;
			oss<<"Vapula host has NOT done with task:\n";
			oss<<_Library->GetLibraryId()<<"=>"<<str::ValueTo(_FunctionId);
			oss<<"\nLast stage: "<<('A' + ret_worker - 1);
			oss<<"\nElapsed time:"<<((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart)<<"(s)";
			ShowMsgbox(oss.str().c_str(), _vf_host);
		}
		return ret;
	}
}