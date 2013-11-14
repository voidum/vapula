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

	strw MbToWc(str src, uint32 cp)
	{
		if(src == null) return null;
		int len = MultiByteToWideChar(cp, 0, src, -1, null, 0);
		wchar_t* dst = new wchar_t[len];
		MultiByteToWideChar(cp, 0, src, -1, dst, len);
		return dst;
	}

	str WcToMb(strw src, uint32 cp)
	{
		if(src == null) return null;
		int len = WideCharToMultiByte(cp, 0, src, -1, null, 0, null, FALSE);
		char* dst = new char[len];
		WideCharToMultiByte(cp, 0, src, -1, dst, len, null, FALSE);
		return dst;
	}

	str FixEncoding(str src, uint32 cp_by, uint32 cp_to)
	{
		if(src == null) return null;
		std::locale::global(std::locale(""));
		strw tmpw = MbToWc(src, cp_by);
		str tmpa = WcToMb(tmpw, cp_to);
		delete tmpw;
		return tmpa;
	}

	str CopyStrA(str src)
	{
		if(src == null) return null;
		size_t len = strlen(src);
		char* dst = new char[len + 1];
		memcpy(dst, src, len);
		dst[len] = '\0';
		return const_cast<str>(dst);
	}

	strw CopyStrW(strw src)
	{
		if(src == null) return null;
		size_t len = wcslen(src);
		wchar_t* dst = new wchar_t[len + 1];
		memcpy(dst, src, len * 2);
		dst[len] = L'\0';
		return const_cast<strw>(dst);
	}

	str ReplaceStrA(str src, str from, str to)
	{
		int len = strlen(from);
		if(len < 1) return CopyStrA(src);
		string str_src = src;
		string str_dst = "";
		for(;;)
		{
			uint64 p1 = str_src.find(from);
			if(p1 != string::npos) 
			{
				str_dst += str_src.substr(0, p1);
				str_dst += to;
				str_src = str_src.substr(p1 + len);
			}
			else
			{
				str_dst += str_src;
				break;
			}
		}
		return CopyStrA(str_dst.c_str());
	}

	strw ReplaceStrW(strw src, strw from, strw to)
	{
		int len = wcslen(from);
		if(len < 1) return CopyStrW(src);
		wstring str_src = src;
		wstring str_dst = L"";
		for(;;)
		{
			uint64 p1 = str_src.find(from);
			if(p1 != wstring::npos) 
			{
				str_dst += str_src.substr(0, p1);
				str_dst += to;
				str_src = str_src.substr(p1 + len);
			}
			else
			{
				str_dst += str_src;
				break;
			}
		}
		return CopyStrW(str_dst.c_str());
	}

	str GetRandomHexA(int len)
	{
		string hex = "";
		srand((uint32)time(0));
		int i,j;
		for (i=0; i<len; i++)
		{
			int c;
			j = (int)(16.0 * rand() / (RAND_MAX + 1.0));
			if(j < 10) c = j + 48;
			else c = j + 87;
			hex += ((char)c);
		}
		str ret = CopyStrA(hex.c_str());
		return ret;
	}

	strw GetRandomHexW(int len)
	{
		str tmp = GetRandomHexA(len);
		strw ret = MbToWc(tmp);
		delete tmp;
		return ret;
	}

	str GetTimeStrA()
	{
		SYSTEMTIME time;
		str tmp = null;
		string str;

		GetLocalTime(&time);
		tmp = ValueToStr(time.wYear);
		str += tmp; delete tmp;
		if(time.wMonth < 10) str += "0";
		tmp = ValueToStr(time.wMonth);
		str += tmp; delete tmp;
		if(time.wDay < 10) str += "0";
		tmp = ValueToStr(time.wDay);
		str += tmp; delete tmp;
		if(time.wHour < 10) str += "0";
		tmp = ValueToStr(time.wHour);
		str += tmp; delete tmp;
		if(time.wMinute < 10) str += "0";
		tmp = ValueToStr(time.wMinute);
		str += tmp; delete tmp;
		if(time.wSecond < 10) str += "0";
		tmp = ValueToStr(time.wSecond);
		str += tmp; delete tmp;
		tmp = CopyStrA(str.c_str());
		return tmp;
	}

	strw GetTimeStrW()
	{
		str tmp = GetTimeStrA();
		strw ret = MbToWc(tmp);
		delete tmp;
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