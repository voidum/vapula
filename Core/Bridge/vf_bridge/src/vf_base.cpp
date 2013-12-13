#include "vf_base.h"
#include "vf_config.h"

namespace vapula
{
	int GetTypeUnit(int type)
	{
		switch(type)
		{
		case VF_DATA_POINTER:
		case VF_DATA_STRING:
			return sizeof(object);
		case VF_DATA_BOOL:
		case VF_DATA_INT8:
		case VF_DATA_UINT8:
			return 1;
		case VF_DATA_INT16:
		case VF_DATA_UINT16:
			return 2;
		case VF_DATA_INT32:
		case VF_DATA_UINT32:
		case VF_DATA_REAL32:
			return 4;
		case VF_DATA_INT64:
		case VF_DATA_UINT64:
		case VF_DATA_REAL64:
			return 8;
		default:
			throw invalid_argument(_vf_err_0);
		}
	}

	cstr8 GetLuid()
	{
		std::ostringstream oss;
		oss.imbue(std::locale("C"));
		const time_t t = time(null);
		oss<<"VAPULA_"<<t<<"_";
		srand((uint32)time(null));
		for(int8 i=0; i<5; i++)
		{
			int rnd = rand() % 10;
			oss<<rnd;
		}
		return str::Copy(oss.str().c_str());
	}

	void ShowMsgbox(cstr8 value, cstr8 caption)
	{
		Config* config = Config::GetInstance();
		if(!config->IsSilent())
			MessageBoxA(null, value, 
			caption == null ? _vf_bridge : caption, 0);
	}

	void ShowMsgbox(cstr16 value, cstr16 caption)
	{
		Config* config = Config::GetInstance();
		if(!config->IsSilent())
			MessageBoxW(null, value,
				caption == null ? str::ToCh16(_vf_bridge) : caption, 0);
	}

	cstr8 GetRuntimeDir()
	{
		HMODULE mod = GetModuleHandle(L"vf_bridge");
		wchar_t path[MAX_PATH];
		GetModuleFileName(mod, path, MAX_PATH);
		cstr8 path8 = str::ToCh8(path);
		string str_full = path8;
		string str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		cstr8 ret = str::Copy(str_ret.c_str());
		delete path8;
		return ret;
	}

	cstr8 GetAppName()
	{
		wchar_t path[MAX_PATH];
		GetModuleFileName(null, path, MAX_PATH);
		cstr8 path8 = str::ToCh8(path);
		string str_full = path8;
		string str_ret = str_full.substr(str_full.rfind(L'\\') + 1);
		cstr8 ret = str::Copy(str_ret.c_str());
		delete path8;
		return ret;
	}

	cstr8 GetAppDir()
	{
		wchar_t path[MAX_PATH];
		GetModuleFileName(null, path, MAX_PATH);
		cstr8 path8 = str::ToCh8(path);
		string str_full = path8;
		string str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		cstr8 ret = str::Copy(str_ret.c_str());
		delete path8;
		return ret;
	}

	cstr8 GetDirPath(cstr8 path, bool isfile)
	{
		if(strlen(path) < 1) 
			return str::Copy("\\");
		cstr8 str_fix = str::Replace(path, "/", "\\");
		string s = str_fix;
		if(!isfile)
			s += L'\\';
		else
		{
			uint32 p = s.rfind('\\');
			if(p == wstring::npos)
				s = "\\";
			else if(p != s.size() - 1) 
				s = s.substr(0, p+1);
		}
		delete str_fix;
		return str::Copy(s.c_str());
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