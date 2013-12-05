#include "stdafx.h"
#include "tcm_base.h"
#include "tcm_config.h"

#include <windows.h>

namespace tcm
{
	int GetTypeUnit(int type)
	{
		switch(type)
		{
		case TCM_DATA_POINTER:
		case TCM_DATA_STRING:
			return sizeof(object);
		case TCM_DATA_BOOL:
		case TCM_DATA_INT8:
		case TCM_DATA_UINT8:
			return 1;
		case TCM_DATA_INT16:
		case TCM_DATA_UINT16:
			return 2;
		case TCM_DATA_INT32:
		case TCM_DATA_UINT32:
		case TCM_DATA_REAL32:
			return 4;
		case TCM_DATA_INT64:
		case TCM_DATA_UINT64:
		case TCM_DATA_REAL64:
			return 8;
		default:
			throw invalid_argument(_tcm_err_0);
		}
	}

	str GetLuidA()
	{
		std::ostringstream oss;
		oss.imbue(std::locale("C"));
		const time_t t = time(null);
		tm* ct = localtime(&t);

		oss<<"TCM_"<<ct->tm_year<<ct->tm_mon<<ct->tm_yday;
		oss<<ct->tm_hour<<ct->tm_min<<ct->tm_sec<<"_";

		for(int8 i=0; i<5; i++)
		{
			int rnd = rand() % 10;
			oss<<rnd;
		}
		return CopyStrA(oss.str().c_str());
	}

	strw GetLuidW()
	{
		str guid = GetLuidA();
		strw ret = MbToWc(guid, "utf8");
		delete guid;
		return ret;
	}

	void ShowMsgStr(str value, str caption)
	{
		Config* config = Config::GetInstance();
		if(!config->IsSilent())
			MessageBoxA(null, 
				value, 
				caption == null ? "" : caption, 0);
	}

	void ShowMsgStr(strw value, strw caption)
	{
		Config* config = Config::GetInstance();
		if(!config->IsSilent())
			MessageBoxW(null, 
				value,
				caption == null ? L"" : caption, 0);
	}

	strw GetRuntimeDir()
	{
		HMODULE mod = GetModuleHandle(L"tcm_bridge");
		WCHAR path[MAX_PATH];
		GetModuleFileName(mod, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		strw ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	strw GetAppName()
	{
		wchar_t path[MAX_PATH];
		GetModuleFileName(null, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(str_full.rfind(L'\\') + 1);
		strw ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	strw GetAppDir()
	{
		wchar_t path[MAX_PATH];
		GetModuleFileName(null, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		strw ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	strw GetDirPath(strw path, bool isfile)
	{
		if(wcslen(path) < 1) return CopyStrW(L"\\");
		strw str_fix = ReplaceStrW(path, L"/", L"\\");
		wstring str = str_fix;
		uint64 p = str.rfind(L'\\');
		if(p == wstring::npos)
		{
			if(isfile) str = L"\\";
			else str += L'\\';
		}
		else if(p != str.size() - 1) 
		{
			if(isfile) str = str.substr(0, p+1);
			else str += L'\\';
		}
		delete str_fix;
		return CopyStrW(str.c_str());
	}

	bool CanOpenRead(PCWSTR file)
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