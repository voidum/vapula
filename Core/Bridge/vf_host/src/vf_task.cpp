#include "vf_task.h"
#include "vf_worker.h"
#include "vf_candy.h"
#include "vf_xml.h"
#include "vf_driver.h"

namespace vapula
{
	Task::Task()
	{
		_Lib = null;
		_FuncId = -1;
		_StageTime = new float[3];
		for(int i=0; i<3; i++)
			_StageTime[i] = 0;
		_CtrlMode = VF_HOST_CJ_NULL;
		_CtrlConfig = null;
		_Tags = new Dict();
	}

	Task::~Task()
	{
		Clear(_Lib);
		Clear(_StageTime, true);
		Clear(_CtrlConfig);
		Clear(_Tags);
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
		return _Lib;
	}

	int Task::GetFunctionId()
	{
		return _FuncId;
	}

	Invoker* Task::GetInvoker()
	{
		return _Invoker;
	}

	int Task::GetCtrlMode() 
	{
		return _CtrlMode; 
	}

	cstr8 Task::GetCtrlConfig() 
	{
		return _CtrlConfig; 
	}

	Dict* Task::GetTags() 
	{
		return _Tags; 
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

		cstr8 dir = xml::ValueCh8(xe_target->first_node("dir"));
		cstr8 lib = xml::ValueCh8(xe_target->first_node("lib"));
		string str_path = dir;
		str_path += lib;
		str_path += ".vapula.library";
		task->_Lib = Library::Load(str_path.c_str());
		delete lib;

		if(task->_Lib == null)
		{
			ShowMsgbox("Fail to load library.", _vf_host);
			return null;
		}

		if(!task->_Lib->Mount())
		{
			ShowMsgbox("Fail to mount library", _vf_host);
			return null;
		}

		task->_FuncId = xml::ValueInt(xe_target->first_node("fid"));
		task->_Invoker = task->_Lib->CreateInvoker(task->_FuncId);

		Envelope* env = task->_Invoker->GetEnvelope();
		xml_node<>* xe_param = (xml_node<>*)xml::Path(xe_target, 2, "params", "param");
		while (xe_param)
		{
			int pid = xml::ValueInt(xe_param->first_attribute("id"));
			cstr8 pv = xml::ValueCh8(xe_param);
			env->CastWrite(pid, pv);
			delete pv;
			xe_param = xe_param->next_sibling();
		}
		//TODO: output params for validation?

		xml_node<>* xe_tag = (xml_node<>*)xml::Path(xe_ext, 2, "tags", "tag");
		while(xe_tag)
		{
			cstr8 key = xml::ValueCh8(xe_tag->first_attribute("key"));
			cstr8 value = xml::ValueCh8(xe_tag);
			task->_Tags->Add(key, value);
			delete key;
			delete value;
			xe_tag = xe_tag->next_sibling();
		}

		xml_node<>* xe_ctrl_mode = (xml_node<>*)xml::Path(xe_ext, 2, "control", "mode");
		xml_node<>* xe_ctrl_config = (xml_node<>*)xml::Path(xe_ext, 2, "control", "config");
		task->_CtrlMode = xml::ValueInt(xe_ctrl_mode);
		task->_CtrlConfig = xml::Print(xe_ctrl_config);
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
			int ret_task = worker->GetTask()->GetInvoker()->GetContext()->GetReturnCode();
			ostringstream oss;
			oss<<"Vapula host has done with task:\n";
			oss<<_Lib->GetLibraryId()<<"=>"<<_FuncId;
			oss<<"\nReturn code:"<<ret_task;
			oss<<"\nElapsed time:"<<((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart)<<"(s)";
			ShowMsgbox(oss.str().c_str(), _vf_host);
			ret = true;
		}
		else if(ret_worker > VF_HOST_RETURN_NORMAL)
		{
			ostringstream oss;
			oss<<"Vapula host has NOT done with task:\n";
			oss<<_Lib->GetLibraryId()<<"=>"<<str::ValueTo(_FuncId);
			oss<<"\nLast stage: "<<('A' + ret_worker - 1);
			oss<<"\nElapsed time:"<<((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart)<<"(s)";
			ShowMsgbox(oss.str().c_str(), _vf_host);
		}
		return ret;
	}
}