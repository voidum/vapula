#include "vf_task.h"
#include "vf_worker.h"
#include "vf_setting.h"

#include "worker_null.h"
#include "worker_pipe.h"

#pragma comment(linker, \
	"/manifestdependency:\"type='win32' \
	name='Microsoft.Windows.Common-Controls' \
	version='6.0.0.0' \
	processorArchitecture='*' \
	publicKeyToken='6595b64144ccf1df' language='*'\"")  // NOLINT(whitespace/line_length)

void CheckOption(int argc, str16* argv);
void ShowHelp();

int APIENTRY wWinMain(HINSTANCE, HINSTANCE, LPWSTR, int)
{
	int argc = 0;
	str16* argv = CommandLineToArgvW(GetCommandLineW(), &argc);

	if(argc < 2)
	{
		ShowHelp();
		return VF_HOST_RETURN_INVALIDCMD;
	}

	CheckOption(argc, argv);
	astr8 path(str::ToCh8(argv[1]));

	if(!CanOpenRead(path.get()))
	{
		ShowMsgbox("Fail to open task file.", _vf_host);
		return VF_HOST_RETURN_INVALIDTASK;
	}

	Scoped<Task> task(Task::Parse(path.get()));
	if(task.empty())
	{
		ShowMsgbox("Fail to parse task file.", _vf_host);
		return VF_HOST_RETURN_INVALIDTASK;
	}
	
	Worker* wk = null;
	switch(task->GetCtrlMode())
	{
		case VF_HOST_CJ_NULL:
			wk = new Worker_NULL(); break;
		case VF_HOST_CJ_PIPE:
			wk = new Worker_PIPE(); break;
	}
	Scoped<Worker> worker(wk);
	bool ret = task->RunAs(wk);
	if(ret) return VF_HOST_RETURN_NORMAL;
	else return VF_HOST_RETURN_FAILEXEC;
}

void CheckOption(int argc, LPWSTR* argv)
{
	Setting* setting = Setting::GetInstance();
	Flag* flag = setting->GetFlag();
	for (int i=2; i<argc; i++)
	{
		if (wcscmp(argv[i], L"silent") == 0)
		{
			flag->Enable(VF_SETTING_SILENT);
			continue;
		}
		if (wcscmp(argv[i], L"rtmon") == 0)
		{
			flag->Enable(VF_SETTING_RTMON);
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
	ShowMsgbox(oss.str().c_str(), _vf_host);
}