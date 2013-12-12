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

	cstr GetLuidA()
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
		return CopyStrA(oss.str().c_str());
	}

	cstrw GetLuidW()
	{
		cstr guid = GetLuidA();
		cstrw ret = MbToWc(guid, "utf8");
		delete guid;
		return ret;
	}

	void ShowMsgStr(cstr value, cstr caption)
	{
		Config* config = Config::GetInstance();
		if(!config->IsSilent())
			MessageBoxA(null, 
				value, 
				caption == null ? "" : caption, 0);
	}

	void ShowMsgStr(cstrw value, cstrw caption)
	{
		Config* config = Config::GetInstance();
		if(!config->IsSilent())
			MessageBoxW(null, 
				value,
				caption == null ? L"" : caption, 0);
	}

	cstrw GetRuntimeDir()
	{
		HMODULE mod = GetModuleHandle(L"vf_bridge");
		WCHAR path[MAX_PATH];
		GetModuleFileName(mod, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		cstrw ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	cstrw GetAppName()
	{
		wchar_t path[MAX_PATH];
		GetModuleFileName(null, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(str_full.rfind(L'\\') + 1);
		cstrw ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	cstrw GetAppDir()
	{
		wchar_t path[MAX_PATH];
		GetModuleFileName(null, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		cstrw ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	cstrw GetDirPath(cstrw path, bool isfile)
	{
		if(wcslen(path) < 1) 
			return CopyStrW(L"\\");
		cstrw strw_fix = ReplaceStrW(path, L"/", L"\\");
		wstring str = strw_fix;
		if(!isfile)
			str += L'\\';
		else
		{
			uint32 p = str.rfind(L'\\');
			if(p == wstring::npos)
				str = L"\\";
			else if(p != str.size() - 1) 
				str = str.substr(0, p+1);
		}
		delete strw_fix;
		return CopyStrW(str.c_str());
	}

	bool CanOpenRead(cstrw file)
	{
		HANDLE handle = 
			CreateFile(file, 0, 
				FILE_SHARE_READ, null, 
				OPEN_EXISTING, null, null);
		if(handle == INVALID_HANDLE_VALUE) 
			return false;
		CloseHandle(handle);
		return true;
	}
}