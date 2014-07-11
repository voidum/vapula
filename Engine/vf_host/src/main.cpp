#include "vf_host.h"
#include "vf_loop.h"

#pragma comment(linker, \
	"/manifestdependency:\"type='win32' \
	name='Microsoft.Windows.Common-Controls' \
	version='6.0.0.0' \
	processorArchitecture='*' \
	publicKeyToken='6595b64144ccf1df' language='*'\"")

using namespace vapula;

void CheckOption(int argc, pwstr* argv);
void ShowHelp();

int APIENTRY wWinMain(HINSTANCE, HINSTANCE, LPWSTR, int)
{
	int argc = 0;
	pwstr* argv = CommandLineToArgvW(GetCommandLineW(), &argc);

	if(argc < 2)
	{
		ShowHelp();
		return VFH_RETURN_ERROR;
	}

	CheckOption(argc, argv);
	pcstr cs8_path = str::ToStr(argv[1]);
	Scoped autop1((raw)cs8_path);

	if(!vapula::TryOpenRead(cs8_path))
	{
		ShowMsgbox("failed to open loop mode file", _vf_host);
		return VFH_RETURN_ERROR;
	}

	Loop* loop = Loop::Load(cs8_path);
	if (loop != null)
	{
		bool ret = loop->Run();
		return ret ? VFH_RETURN_NORMAL : VFH_RETURN_ERROR;
	}
	return VFH_RETURN_ERROR;
}

void CheckOption(int argc, LPWSTR* argv)
{
	Setting* setting = Setting::Instance();
	Flag* flag = setting->GetFlag();
	for (int i = 0; i < argc; i++)
	{
		if (wcscmp(argv[i], L"silent") == 0)
		{
			flag->Enable(VF_SETTING_SILENT);
			continue;
		}
		if (wcscmp(argv[i], L"realtime") == 0)
		{
			flag->Enable(VF_SETTING_REALTIME);
			continue;
		}
	}
}

void ShowHelp()
{
	ostringstream oss;
	oss << "version:\n";
	oss << " bridge - " << _vf_version << "\n";
	oss << " host - " << _vf_host_version << "\n";
	oss << "\ncommand lines:\n";
	oss << " vf_host [loop mode] [option]\n";
	oss << "\noption:\n";
	oss << " \"silent\" - to run without any prompt\n";
	oss << " \"realtime\" - to monitor in high CPU usage\n";
	ShowMsgbox(oss.str().c_str(), _vf_host);
}