#include "stdafx.h"
#include "task.h"
#include "worker.h"
#include "tcm_config.h"

#pragma comment(linker, "/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")  // NOLINT(whitespace/line_length)

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

int APIENTRY wWinMain(
	HINSTANCE hInstance, 
	HINSTANCE hPrevInstance, 
	LPWSTR lpCmdLine, 
	int nCmdShow)
{
	int argc = 0;
	LPWSTR* argv = CommandLineToArgvW(GetCommandLineW(),&argc);

	if(argc < 2)
	{
		ShowHelp();
		return TCM_HOST_RETURN_INVALIDCMD;
	}

	CheckOption(argc, argv);

	if(!CanOpenRead(argv[1]))
	{
		ShowMsgStr(L"Fail to open task file.",L"TCM Host");
		return TCM_HOST_RETURN_INVALIDTASK;
	}

	TaskEx* task = dynamic_cast<TaskEx*>(TaskEx::Parse(argv[1]));
	if(task == NULL)
	{
		ShowMsgStr(L"Fail to parse task file.",L"TCM Host");
		return TCM_HOST_RETURN_INVALIDTASK;
	}
	
	Worker* worker = NULL;
	int mode = task->GetCtrlMode();
	switch(mode)
	{
		case TCM_HOST_CJ_NULL:
			worker = new Worker_NULL(); break;
		case TCM_HOST_CJ_PIPE:
			worker = new Worker_PIPE(); break;
	}
	bool ret = task->RunAs(worker);
	
	delete worker;
	delete task;
	
	if(ret) return TCM_HOST_RETURN_NORMAL;
	else return TCM_HOST_RETURN_FAILEXEC;
}

void CheckOption(int argc, LPWSTR* argv)
{
	Config* config = Config::GetInstance();
	Flag* flag = config->GetFlag();
	for (int i=2; i<argc; i++)
	{
		if (wcscmp(argv[i], L"silent") == 0)
		{
			flag->Enable(TCM_CONFIG_SILENT);
			continue;
		}
		if (wcscmp(argv[i], L"rtmon") == 0)
		{
			flag->Enable(TCM_CONFIG_RTMON);
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
	ShowMsgStr(str.c_str(),L"TCM Host");
}
