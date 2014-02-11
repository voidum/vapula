#include "vf_task.h"
#include "vf_worker.h"
#include "vf_xml.h"
#include "vf_stack.h"
#include "vf_library.h"
#include "vf_context.h"

namespace vapula
{
	Task::Task()
	{
		_Path = null;
		_Library = null;
		_MethodId = null;
		_CtrlMode = VFH_CTRL_NULL;
		_CtrlSetting = null;
	}

	Task::~Task()
	{
		Clear(_Path);
		Clear(_Library);
		Clear(_MethodId);
		Clear(_CtrlSetting);
	}

	Library* Task::GetLibrary()
	{
		return _Library;
	}

	pcstr Task::GetMethodId()
	{
		return _MethodId;
	}

	int Task::GetCtrlMode() 
	{
		return _CtrlMode; 
	}

	pcstr Task::GetCtrlSetting() 
	{
		return _CtrlSetting; 
	}

	Task* Task::Load(pcstr path)
	{
		XML* xml  = XML::Load(path);
		Handle autop_xml(xml);
		if(xml == null)
		{
			ShowMsgbox("Fail to load task file.", _vf_host);
			return null;
		}
		object xdoc = xml->GetEntity();
		
		Task* task = new Task();
		Handle autop1(task);
		task->_Path = str::Copy(path);

		object xe_root = XML::XElem(xdoc, "task");
		object xe_lib = XML::XElem(xe_root, "library");
		object xe_mt = XML::XElem(xe_root, "method");
		object xe_ctrl_mode = XML::XPath(xe_root, 2, "control", "mode");
		object xe_ctrl_setting = XML::XPath(xe_root, 2, "control", "setting");

		pcstr cs8_lib_utf8 = XML::ValStr(xe_lib);
		Handle autop2((object)cs8_lib_utf8);

		pcstr cs8_lib = str::Encode(cs8_lib_utf8, _vf_msg_cp, null);
		Handle autop3((object)cs8_lib);

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

		task->_MethodId = XML::ValStr(xe_mt);
		task->_CtrlMode = XML::ValInt(xe_ctrl_mode);
		task->_CtrlSetting = XML::Print(xe_ctrl_setting);

		autop1.DeRef();
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
		if(ret_worker == VFH_WORKER_NORMAL)
		{
			Stack* stack = worker->GetStack();
			int ret_task = stack->GetContext()->GetReturnCode();
			ostringstream oss;
			oss<<"Vapula host has done with task:\n";
			oss<<_Library->GetLibraryId()<<"=>"<<_MethodId;
			oss<<"\nReturn code:"<<ret_task;
			oss<<"\nElapsed time:"<<((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart)<<"(s)";
			ShowMsgbox(oss.str().c_str(), _vf_host);
			ret = true;
		}
		else
		{
			ostringstream oss;
			oss<<"Vapula host has NOT done with task:\n";
			oss<<_Library->GetLibraryId()<<"=>"<<_MethodId;
			oss<<"\nLast stage: "<<ret_worker;
			oss<<"\nElapsed time:"<<((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart)<<"(s)";
			ShowMsgbox(oss.str().c_str(), _vf_host);
		}
		return ret;
	}
}