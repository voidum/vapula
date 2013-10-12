#include "stdafx.h"
#include "task.h"
#include "worker.h"
#include "tcm_config.h"

using std::wstring;
using namespace tcm;

enum HostReturnCode
{
	TCM_HOST_RETURN_NORMAL = 0,
	TCM_HOST_RETURN_INVALIDCMD = 1,
	TCM_HOST_RETURN_INVALIDTASK = 2,
	TCM_HOST_RETURN_FAILEXEC = 3
};

void CheckOption(int argc, LPWSTR* argv);
void ShowHelp();

Flag* _Flag = NULL;

int APIENTRY wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nCmdShow)
{
	int argc = 0;
	LPWSTR* argv = CommandLineToArgvW(GetCommandLineW(),&argc);

	if(argc < 2)
	{
		ShowHelp();
		return TCM_HOST_RETURN_INVALIDCMD;
	}

	Config* config = Config::GetInstance();
	_Flag = config->GetFlag();

	if(argc > 2) CheckOption(argc, argv);

	if(!CanOpenRead(argv[1]))
	{
		if(!_Flag->Valid(TCM_CONFIG_SILENT))
			ShowMsgbox(L"Fail to open task file.",L"TCM Host");
		return TCM_HOST_RETURN_INVALIDTASK;
	}

	TaskEx* task = dynamic_cast<TaskEx*>(TaskEx::Parse(argv[1]));
	if(task == NULL)
	{
		if(!_Flag->Valid(TCM_CONFIG_SILENT))
			ShowMsgbox(L"Fail to parse task file.",L"TCM Host");
		return TCM_HOST_RETURN_INVALIDTASK;
	}
	
	Worker* worker = NULL;
	int mode = task->GetCtrlMode();
	switch(mode)
	{
		case TCM_HOST_CJ_NULL: worker = new Worker_NULL(); break;
		case TCM_HOST_CJ_PIPE: worker = new Worker_PIPE(); break;
		case TCM_HOST_CJ_SCP_LUA: worker = new Worker_SCP_LUA(); break;
	}
	bool ret = task->RunAs(worker);
	
	delete worker;
	delete task;
	
	if(ret) return TCM_HOST_RETURN_NORMAL;
	else return TCM_HOST_RETURN_FAILEXEC;
}

void CheckOption(int argc, LPWSTR* argv)
{
	for (int i=2; i<argc; i++)
	{
		if (wcscmp(argv[i],L"silent") == 0)
		{
			_Flag->Enable(TCM_CONFIG_SILENT);
			continue;
		}
		if (wcscmp(argv[i],L"rtmon") == 0)
		{
			_Flag->Enable(TCM_CONFIG_RTMON);
			continue;
		}
	}
}

void ShowHelp()
{
	wstring str = L"command lines:\n";
	str += L" tcm_host [task file] [option]\n";
	str += L"option:\n";
	str += L" \"silent\" - to run without any prompt\n";
	str += L" \"rtmon\" - to monitor in high CPU usage\n";
	ShowMsgbox(str.c_str(),L"TCM Host");
}
