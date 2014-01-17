#include "vf_utility.h"
#include "vf_setting.h"
#include "vf_token.h"

namespace vapula
{
	RequireTI::RequireTI()
	{
		_Token = new Token();
	}

	RequireTI::~RequireTI()
	{
		delete _Token;
	}

	bool RequireTI::AssertOffTI()
	{
		return _Token->IsOff();
	}

	void RequireTI::TokenOff(uint8& key)
	{
		_Token->Off(key);
	}

	void RequireTI::TokenOn(uint8 key)
	{
		_Token->On(key);
	}

	void ShowMsgbox(cstr8 value, cstr8 caption)
	{
		Setting* setting = Setting::GetInstance();
		if(!setting->IsSilent())
			MessageBoxA(null, value, 
			caption == null ? _vf_bridge : caption, 0);
	}

	void ShowMsgbox(cstr16 value, cstr16 caption)
	{
		Setting* setting = Setting::GetInstance();
		if(!setting->IsSilent())
			MessageBoxW(null, value,
			caption == null ? str::ToCh16(_vf_bridge) : caption, 0);
	}

	cstr8 GetRuntimeDir()
	{
		HMODULE mod = GetModuleHandle(L"vf_bridge");
		str16 path = new wchar_t[_vf_path_len];
		GetModuleFileName(mod, path, _vf_path_len);
		cstr8 path8 = str::ToCh8(path);
		delete path;
		string str_full = path8;
		string str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		cstr8 ret = str::Copy(str_ret.c_str());
		delete path8;
		return ret;
	}

	cstr8 GetAppName()
	{
		str16 path = new wchar_t[_vf_path_len];
		GetModuleFileName(null, path, _vf_path_len);
		cstr8 path8 = str::ToCh8(path);
		delete path;
		string str_full = path8;
		string str_ret = str_full.substr(str_full.rfind(L'\\') + 1);
		cstr8 ret = str::Copy(str_ret.c_str());
		delete path8;
		return ret;
	}

	cstr8 GetAppDir()
	{
		str16 path = new wchar_t[_vf_path_len];
		GetModuleFileName(null, path, _vf_path_len);
		cstr8 path8 = str::ToCh8(path);
		delete path;
		string str_full = path8;
		string str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		cstr8 ret = str::Copy(str_ret.c_str());
		delete path8;
		return ret;
	}

	cstr8 GetDirPath(cstr8 path, bool isfile)
	{
		uint32 len = strlen(path);
		if(len < 1) 
			return str::Copy("\\");
		cstr8 s8_fix = str::Replace(path, "/", "\\");
		string s = s8_fix;
		uint32 pos = s.rfind('\\');
		if(!isfile)
		{
			if(pos == string::npos || len != pos + 1)
				s += L'\\';
		}
		else
		{
			if(pos == string::npos)
				s = "\\";
			else if(pos != s.size() - 1) 
				s = s.substr(0, pos + 1);
		}
		cstr8 s8_ret = str::Copy(s.c_str());
		delete s8_fix;
		return s8_ret;
	}

	bool CanOpenRead(cstr8 file)
	{
		cstr16 file16 = str::ToCh16(file);
		HANDLE handle = 
			CreateFile(file16, 0, 
			FILE_SHARE_READ, null, 
			OPEN_EXISTING, null, null);
		delete file16;
		if(handle == INVALID_HANDLE_VALUE) 
			return false;
		CloseHandle(handle);
		return true;
	}
}