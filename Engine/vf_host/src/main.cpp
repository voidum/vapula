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

void CheckOption(int argc, pwstr* argv);
void ShowHelp();

int APIENTRY wWinMain(HINSTANCE, HINSTANCE, LPWSTR, int)
{
	int argc = 0;
	pwstr* argv = CommandLineToArgvW(GetCommandLineW(), &argc);

	if(argc < 2)
	{
		ShowHelp();
		return VFH_RETURN_INVALIDCMD;
	}

	CheckOption(argc, argv);
	pcstr cs8_path = str::ToStr(argv[1]);
	Handle autop1((object)cs8_path);

	if(!CanOpenRead(cs8_path))
	{
		ShowMsgbox("Fail to open task file.", _vf_host);
		return VFH_RETURN_INVALIDTASK;
	}

	Task* task = Task::Load(cs8_path);
	Handle autop_task(task);
	if(task == null)
	{
		ShowMsgbox("Fail to parse task file.", _vf_host);
		return VFH_RETURN_INVALIDTASK;
	}
	
	Worker* worker = null;
	switch(task->GetCtrlMode())
	{
		case VFH_CTRL_NULL:
			worker = new Worker_Null(); break;
		case VFH_CTRL_PIPE:
			worker = new Worker_Pipe(); break;
	}
	Handle autop_wk(worker);
	bool ret = task->RunAs(worker);
	if(ret) return VFH_RETURN_NORMAL;
	else return VFH_RETURN_FAILEXEC;
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
	oss<<"version:\n";
	oss<<" bridge - "<<_vf_version<<"\n";
	oss<<" host - "<<_vf_host_version<<"\n";
	oss<<"\ncommand lines:\n";
	oss<<" vf_host [task file] [option]\n";
	oss<<"\noption:\n";
	oss<<" \"silent\" - to run without any prompt\n";
	oss<<" \"rtmon\" - to monitor in high CPU usage\n";
	ShowMsgbox(oss.str().c_str(), _vf_host);
}