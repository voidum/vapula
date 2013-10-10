#include "stdafx.h"
#include "task.h"
#include "worker.h"

#include "tcm_library.h"
#include "tcm_executor.h"
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
	if(xdoc == NULL) return NULL;

	TaskEx* task = new TaskEx();
	xml_node<>* xe_root = xdoc->first_node("root");
	xml_node<>* xe_target = xe_root->first_node("target");
	xml_node<>* xe_ext = xe_root->first_node("extension");

	PCWSTR dir = xml::ValueW(xe_target->first_node("dir"));
	PCWSTR cid = xml::ValueW(xe_target->first_node("cid"));
	wstring str_path = dir;
	str_path += cid;
	str_path += L".tcm.xml";
	task->_Com = Component::Load(str_path.c_str());
	delete path;
	delete cid;
	task->_FuncId = xml::ValueInt(xe_target->first_node("fid"));
	task->_Executor = task->_Com->CreateExecutor(task->_FuncId);

	Envelope* env = task->_Executor->GetEnvelope();
	xml_node<>* xe_param = (xml_node<>*)xml::Path(xe_target, 2, "params", "param");
	while (xe_param)
	{
		int pid = xml::ValueInt(xe_param->first_attribute("id"));
		PCSTR pv = FixEncoding(xml::ValueA(xe_param));
		env->CastWriteA(pid, pv);
		delete pv;
		xe_param = xe_param->next_sibling();
	}

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
	int retcode = worker->Run(this);
	QueryPerformanceCounter(&t2);
	Config* config = Config::GetInstance();
	Flag* flag = config->GetFlag();
	if(!flag->Valid(TCM_CONFIG_SILENT))
	{
		wstring str = L"TCM host has executed task:\n";
		str += _Com->GetComponentId();
		str += L"=>";
		str += ValueToStrW(_FuncId);
		str += L"\nReturn code:";
		str += ValueToStrW(retcode);
		str += L"\nElapsed time:";
		str += ValueToStrW((t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart);
		str += L"(s)";
		ShowMsgbox(str.c_str(),L"TCM Host");
	}
	if(retcode != TCM_RETURN_NORMAL) return false;
	else return true;
}