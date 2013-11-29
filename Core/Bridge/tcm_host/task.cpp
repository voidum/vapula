#include "stdafx.h"
#include "task.h"
#include "worker.h"

#include "tcm_driver.h"
#include "tcm_library.h"
#include "tcm_invoker.h"
#include "tcm_xml.h"
#include "tcm_config.h"

using namespace tcm;
using namespace rapidxml;
using std::wstring;

TaskEx::TaskEx()
{
	_CtrlMode = TCM_HOST_CJ_NULL;
	_CtrlConfig = NULL;
	_Tags = new Dictionary();
}

TaskEx::~TaskEx()
{
	Clear(_CtrlConfig);
	Clear(_Tags);
}

int TaskEx::GetCtrlMode() { return _CtrlMode; }

PCSTR TaskEx::GetCtrlConfig() { return _CtrlConfig; }

Dictionary* TaskEx::GetTags() { return _Tags; }

Task* TaskEx::Parse(PCWSTR path)
{
	PCSTR data = NULL;
	xml_node<>* xdoc = (xml_node<>*)xml::Load(path, data);
	if(xdoc == NULL)
	{
		ShowMsgStr("Fail to load TCM task file.", "TCM Host");
		return NULL;
	}

	TaskEx* task = new TaskEx();
	xml_node<>* xe_root = xdoc->first_node("task");
	xml_node<>* xe_target = xe_root->first_node("target");
	xml_node<>* xe_ext = xe_root->first_node("extension");

	DriverHub* driver_hub = DriverHub::GetInstance();
	PCSTR driver_id = xml::ValueA(xe_target->first_node("runtime"));
	if(!driver_hub->Link(driver_id))
	{
		ShowMsgStr("Fail to link driver.", "TCM Host");
		return NULL;
	}
	delete driver_id;

	PCWSTR dir = xml::ValueW(xe_target->first_node("dir"));
	PCWSTR lib = xml::ValueW(xe_target->first_node("lib"));
	wstring str_path = dir;
	str_path += lib;
	str_path += L".tcm.xml";
	task->_Lib = Library::Load(str_path.c_str());
	delete lib;

	if(task->_Lib == NULL)
	{
		ShowMsgStr("Fail to load library.", "TCM Host");
		return NULL;
	}

	if(!task->_Lib->Mount())
	{
		ShowMsgStr("Fail to mount library", "TCM Host");
		return NULL;
	}
	
	task->_FuncId = xml::ValueInt(xe_target->first_node("fid"));
	task->_Invoker = task->_Lib->CreateInvoker(task->_FuncId);

	Envelope* env = task->_Invoker->GetEnvelope();
	xml_node<>* xe_param = (xml_node<>*)xml::Path(xe_target, 2, "params", "param");
	while (xe_param)
	{
		int pid = xml::ValueInt(xe_param->first_attribute("id"));
		str pv = xml::ValueA(xe_param);
		env->CastWriteA(pid, pv);
		delete pv;
		xe_param = xe_param->next_sibling();
	}
	//TODO: output params for validation?

	xml_node<>* xe_tag = (xml_node<>*)xml::Path(xe_ext, 2, "tags", "tag");
	while(xe_tag)
	{
		PCWSTR key = xml::ValueW(xe_tag->first_attribute("key"));
		PCWSTR value = xml::ValueW(xe_tag);
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

bool TaskEx::RunAs(Worker* worker)
{
	LARGE_INTEGER freq,t1,t2;
	QueryPerformanceFrequency(&freq);
	QueryPerformanceCounter(&t1);
	int ret_worker = worker->Run(this);
	QueryPerformanceCounter(&t2);

	bool ret = false;
	if(ret_worker == TCM_TASK_RETURN_NORMAL)
	{
		int ret_task = worker->GetTask()->GetInvoker()->GetContext()->GetReturnCode();
		wstring str = L"TCM host has done with task:\n";
		str += _Lib->GetLibraryId();
		str += L"=>";
		str += MbToWc(ValueToStr(_FuncId));
		str += L"\nReturn code:";
		str += MbToWc(ValueToStr(ret_task));
		str += L"\nElapsed time:";
		str += MbToWc(ValueToStr((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart));
		str += L"(s)";
		ShowMsgStr(str.c_str(), L"TCM Host");
		ret = true;
	}
	else if(ret_worker > TCM_TASK_RETURN_NORMAL)
	{
		wstring str = L"TCM host has NOT done with task:\n";
		str += _Lib->GetLibraryId();
		str += L"=>";
		str += MbToWc(ValueToStr(_FuncId));
		str += L"\nLast stage: ";
		str += MbToWc(ValueToStr('A' + ret_worker - 1));
		str += L"\nElapsed time:";
		str += MbToWc(ValueToStr((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart));
		str += L"(s)";
		ShowMsgStr(str.c_str(), L"TCM Host");
	}
	return ret;
}