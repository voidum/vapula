#include "stdafx.h"
#include "tcm_base.h"
#include <ctime>
#include <iostream>

namespace tcm
{
	using std::string;
	using std::wstring;

	int GetTypeUnit(int type)
	{
		switch(type)
		{
		case TCM_DATA_POINTER:
		case TCM_DATA_CSTRA:
		case TCM_DATA_CSTRW:
			return sizeof(LPVOID);
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
			return 0;
		}
	}

	PCWSTR MbToWc(PCSTR src, UINT cp)
	{
		if(src == NULL) return NULL;
		int len = MultiByteToWideChar(cp, 0, src, -1, NULL, 0);
		wchar_t* dst = new wchar_t[len];
		MultiByteToWideChar(cp, 0, src, -1, dst, len);
		return dst;
	}

	PCSTR WcToMb(PCWSTR src, UINT cp)
	{
		if(src == NULL) return NULL;
		int len = WideCharToMultiByte(cp, 0, src, -1, NULL, 0, NULL, FALSE);
		char* dst = new char[len];
		WideCharToMultiByte(cp, 0, src, -1, dst, len, NULL, FALSE);
		return dst;
	}

	PCSTR FixEncoding(PCSTR src, UINT cpMid, UINT cpAim)
	{
		if(src == NULL) return NULL;
		std::locale::global(std::locale(""));
		PCWSTR tmpw = MbToWc(src, cpMid);
		PCSTR tmpa = WcToMb(tmpw, cpAim);
		delete tmpw;
		return tmpa;
	}

	PCSTR CopyStrA(PCSTR src)
	{
		if(src == NULL) return NULL;
		size_t len = strlen(src);
		char* dst = new char[len + 1];
		memcpy(dst, src, len);
		dst[len] = '\0';
		return const_cast<PCSTR>(dst);
	}

	PCWSTR CopyStrW(PCWSTR src)
	{
		if(src == NULL) return NULL;
		size_t len = wcslen(src);
		wchar_t* dst = new wchar_t[len + 1];
		memcpy(dst, src, len * 2);
		dst[len] = L'\0';
		return const_cast<PCWSTR>(dst);
	}

	PCSTR ReplaceStrA(PCSTR src, PCSTR from, PCSTR to)
	{
		int len = strlen(from);
		if(len < 1) return CopyStrA(src);
		string str_src = src;
		string str_dst = "";
		for(;;)
		{
			int p1 = str_src.find(from);
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

	PCWSTR ReplaceStrW(PCWSTR src, PCWSTR from, PCWSTR to)
	{
		int len = wcslen(from);
		if(len < 1) return CopyStrW(src);
		wstring str_src = src;
		wstring str_dst = L"";
		for(;;)
		{
			int p1 = str_src.find(from);
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

	PCSTR GetRandomHexA(int len)
	{
		string hex = "";
		srand((UINT)time(0));
		int i,j;
		for (i=0; i<len; i++)
		{
			int c;
			j=(int)(16.0*rand()/(RAND_MAX+1.0));
			if(j<10) c=j+48;
			else c=j+87;
			hex += ((char)c);
		}
		PCSTR ret = CopyStrA(hex.c_str());
		return ret;
	}

	PCWSTR GetRandomHexW(int len)
	{
		PCSTR tmp = GetRandomHexA(len);
		PCWSTR ret = MbToWc(tmp);
		delete tmp;
		return ret;
	}

	PCSTR GetTimeStrA()
	{
		SYSTEMTIME time;
		PCSTR tmp = NULL;
		string str;

		GetLocalTime(&time);
		tmp = ValueToStrA(time.wYear);
		str += tmp; delete tmp;
		if(time.wMonth < 10) str += "0";
		tmp = ValueToStrA(time.wMonth);
		str += tmp; delete tmp;
		if(time.wDay < 10) str += "0";
		tmp = ValueToStrA(time.wDay);
		str += tmp; delete tmp;
		if(time.wHour < 10) str += "0";
		tmp = ValueToStrA(time.wHour);
		str += tmp; delete tmp;
		if(time.wMinute < 10) str += "0";
		tmp = ValueToStrA(time.wMinute);
		str += tmp; delete tmp;
		if(time.wSecond < 10) str += "0";
		tmp = ValueToStrA(time.wSecond);
		str += tmp; delete tmp;
		tmp = CopyStrA(str.c_str());
		return tmp;
	}

	PCWSTR GetTimeStrW()
	{
		PCSTR tmp = GetTimeStrA();
		PCWSTR ret = MbToWc(tmp);
		delete tmp;
		return ret;
	}

	void ShowMsgbox(PCWSTR value, PCWSTR caption)
	{
		MessageBoxW(NULL, value == NULL ? L"" : value, caption == NULL ? L"" : caption, 0);
	}

	void ShowMsgbox(PCSTR value, PCSTR caption)
	{
		MessageBoxA(NULL, value == NULL ? "" : value, caption == NULL ? "" : caption, 0);
	}

	PCWSTR GetRuntimeDir()
	{
		HMODULE mod = GetModuleHandle(L"tcm_bridge");
		WCHAR path[MAX_PATH];
		GetModuleFileName(mod, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		PCWSTR ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	PCWSTR GetAppName()
	{
		WCHAR path[MAX_PATH];
		GetModuleFileName(NULL, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(str_full.rfind(L'\\') + 1);
		PCWSTR ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	PCWSTR GetAppDir()
	{
		WCHAR path[MAX_PATH];
		GetModuleFileName(NULL, path, MAX_PATH);
		wstring str_full = path;
		wstring str_ret = str_full.substr(0, str_full.rfind(L'\\') + 1);
		PCWSTR ret = CopyStrW(str_ret.c_str());
		return ret;
	}

	PCWSTR GetDirPath(PCWSTR path, bool isfile)
	{
		if(wcslen(path) < 1) return CopyStrW(L"\\");
		PCWSTR str_fix = ReplaceStrW(path, L"/", L"\\");
		wstring str = str_fix;
		int p = str.rfind(L'\\');
		if(p == wstring::npos)
		{
			if(isfile) str = L"\\";
			else str += L'\\';
		}
		else if((UINT)p != str.size() - 1) 
		{
			if(isfile) str = str.substr(0, p+1);
			else str += L'\\';
		}
		delete str_fix;
		return CopyStrW(str.c_str());
	}

	bool CanOpenRead(PCWSTR file)
	{
		HANDLE handle = CreateFile(file,0,FILE_SHARE_READ,NULL,OPEN_EXISTING,NULL,NULL);
		if(handle == INVALID_HANDLE_VALUE) return false;
		CloseHandle(handle);
		return true;
	}
}