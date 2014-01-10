#include "vf_task.h"
#include "vf_worker.h"
#include "vf_utility.h"
#include "vf_xml.h"
#include "vf_driver.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_envelope.h"

namespace vapula
{
	Task::Task()
	{
		_Library = null;
		_FunctionId = -1;
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

	int Task::GetFunctionId()
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
		cstr8 data = null;
		xml_node<>* xdoc = (xml_node<>*)xml::Load(path, data);
		if(xdoc == null)
		{
			ShowMsgbox("Fail to load task file.", _vf_host);
			return null;
		}

		Task* task = new Task();
		xml_node<>* xe_root = xdoc->first_node("task");
		xml_node<>* xe_target = xe_root->first_node("target");
		xml_node<>* xe_ext = xe_root->first_node("extension");

		DriverHub* driver_hub = DriverHub::GetInstance();
		cstr8 driver_id = xml::ValueCh8(xe_target->first_node("runtime"));
		if(!driver_hub->Link(driver_id))
		{
			ShowMsgbox("Fail to link driver.", _vf_host);
			return null;
		}
		delete driver_id;

		cstr8 path_lib_utf8 = xml::ValueCh8(xe_target->first_node("path"));
		cstr8 path_lib = str::EncodeCh8(path_lib_utf8, _vf_msg_cp, null);
		delete path_lib_utf8;

		LibraryHub* library_hub = LibraryHub::GetInstance();
		task->_Library = library_hub->Load(path);
		delete path_lib;

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

		task->_FunctionId = xml::ValueInt(xe_target->first_node("function"));
		task->_Invoker = task->_Library->CreateInvoker(task->_FunctionId);

		Stack* stack = task->_Invoker->GetStack();
		Envelope* env = stack->GetEnvelope();
		xml_node<>* xe_param = (xml_node<>*)xml::Path(xe_target, 2, "params", "param");
		while (xe_param)
		{
			int pid = xml::ValueInt(xe_param->first_attribute("id"));
			cstr8 pv = xml::ValueCh8(xe_param);
			env->CastWriteValue(pid, pv);
			delete pv;
			xe_param = xe_param->next_sibling();
		}
		//TODO: output params for validation?

		xml_node<>* xe_ctrl_mode = (xml_node<>*)xml::Path(xe_ext, 2, "control", "mode");
		xml_node<>* xe_ctrl_setting = (xml_node<>*)xml::Path(xe_ext, 2, "control", "setting");
		task->_CtrlMode = xml::ValueInt(xe_ctrl_mode);
		task->_CtrlSetting = xml::Print(xe_ctrl_setting);
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