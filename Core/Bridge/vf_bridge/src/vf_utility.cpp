#include "vf_utility.h"
#include "vf_setting.h"

namespace vapula
{
	void ShowMsgbox(pcstr value, pcstr caption)
	{
		Setting* setting = Setting::GetInstance();
		if(!setting->IsSilent())
			MessageBoxA(null, value, 
			caption == null ? _vf_bridge : caption, 0);
	}

	void ShowMsgbox(pcwstr value, pcwstr caption)
	{
		Setting* setting = Setting::GetInstance();
		if(!setting->IsSilent())
			MessageBoxW(null, value,
			caption == null ? str::ToStrW(_vf_bridge) : caption, 0);
	}

	pcstr GetRuntimeDir()
	{
		HMODULE mod = GetModuleHandle(L"vf_bridge");
		pwstr s16_path = new wchar_t[_vf_path_len];
		GetModuleFileName(mod, s16_path, _vf_path_len);
		pcstr cs8_path = str::ToStr(s16_path);
		delete s16_path;
		Handle autop1((object)cs8_path);
		string str_full = cs8_path;
		string str_fix = str_full.substr(0, str_full.rfind(L'\\') + 1);
		pcstr ret = str::Copy(str_fix.c_str());
		return ret;
	}

	pcstr GetProcessDir()
	{
		pwstr s16_path = new wchar_t[_vf_path_len];
		GetModuleFileName(null, s16_path, _vf_path_len);
		pcstr cs8_path = str::ToStr(s16_path);
		delete s16_path;
		Handle autop1((object)cs8_path);
		string str_full = cs8_path;
		string str_fix = str_full.substr(0, str_full.rfind(L'\\') + 1);
		pcstr ret = str::Copy(str_fix.c_str());
		return ret;
	}

	pcstr GetProcessName()
	{
		pwstr s16_path = new wchar_t[_vf_path_len];
		GetModuleFileName(null, s16_path, _vf_path_len);
		pcstr cs8_path = str::ToStr(s16_path);
		delete s16_path;
		Handle autop1((object)cs8_path);
		string str_full = cs8_path;
		string str_fix = str_full.substr(str_full.rfind(L'\\') + 1);
		pcstr ret = str::Copy(str_fix.c_str());
		return ret;
	}

	pcstr GetDirPath(pcstr path, bool isfile)
	{
		uint32 len = strlen(path);
		if(len < 1) 
			return str::Copy("\\");
		pcstr s8_fix = str::Replace(path, "/", "\\");
		string str = s8_fix;
		uint32 pos = str.rfind('\\');
		if(!isfile)
		{
			if(pos == string::npos || len != pos + 1)
				str += L'\\';
		}
		else
		{
			if(pos == string::npos)
				str = "\\";
			else if(pos != str.size() - 1) 
				str = str.substr(0, pos + 1);
		}
		pcstr s8_ret = str::Copy(str.c_str());
		return s8_ret;
	}

	bool CanOpenRead(pcstr file)
	{
		pcwstr cs16_file(str::ToStrW(file));
		HANDLE handle = 
			CreateFile(cs16_file, null, 
			FILE_SHARE_READ, null, 
			OPEN_EXISTING, null, null);
		if(handle == INVALID_HANDLE_VALUE) 
			return false;
		CloseHandle(handle);
		return true;
	}
}