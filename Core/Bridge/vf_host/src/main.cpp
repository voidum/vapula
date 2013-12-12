#include "vf_task.h"
#include "vf_worker.h"
#include "vf_config.h"

#include "worker_null.h"
#include "worker_pipe.h"

#pragma comment(linker, \
	"/manifestdependency:\"type='win32' \
	name='Microsoft.Windows.Common-Controls' \
	_version='6.0.0.0' \
	processorArchitecture='*' \
	publicKeyToken='6595b64144ccf1df' language='*'\"")  // NOLINT(whitespace/line_length)

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
		return VF_HOST_RETURN_INVALIDCMD;
	}

	CheckOption(argc, argv);

	if(!CanOpenRead(argv[1]))
	{
		ShowMsgStr("Fail to open task file.", _vf_host_appname);
		return VF_HOST_RETURN_INVALIDTASK;
	}

	Task* task = dynamic_cast<Task*>(Task::Parse(argv[1]));
	if(task == null)
	{
		ShowMsgStr("Fail to parse task file.", _vf_host_appname);
		return VF_HOST_RETURN_INVALIDTASK;
	}
	
	Worker* worker = null;
	int mode = task->GetCtrlMode();
	switch(mode)
	{
		case VF_HOST_CJ_NULL:
			worker = new Worker_NULL(); break;
		case VF_HOST_CJ_PIPE:
			worker = new Worker_PIPE(); break;
	}
	bool ret = task->RunAs(worker);
	
	delete worker;
	delete task;
	
	if(ret) return VF_HOST_RETURN_NORMAL;
	else return VF_HOST_RETURN_FAILEXEC;
}

void CheckOption(int argc, LPWSTR* argv)
{
	Config* config = Config::GetInstance();
	Flag* flag = config->GetFlag();
	for (int i=2; i<argc; i++)
	{
		if (wcscmp(argv[i], L"silent") == 0)
		{
			flag->Enable(VF_CONFIG_SILENT);
			continue;
		}
		if (wcscmp(argv[i], L"rtmon") == 0)
		{
			flag->Enable(VF_CONFIG_RTMON);
			continue;
		}
	}
}

void ShowHelp()
{
	ostringstream oss;
	oss<<"command lines:\n";
	oss<<" vf_host [task file] [option]\n";
	oss<<"option:\n";
	oss<<" \"silent\" - to run without any prompt\n";
	oss<<" \"rtmon\" - to monitor in high CPU usage\n";
	ShowMsgStr(oss.str().c_str(), _vf_host_appname);
}