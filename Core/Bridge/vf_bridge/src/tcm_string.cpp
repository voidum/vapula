#include "stdafx.h"
#include "vf_string.h"
#include "unicode/ucnv.h"

namespace vf
{
	strw MbToWc(str src, str code)
	{
		if(src == null) return null;
		UErrorCode err = U_ZERO_ERROR;
		str cp = (code == null ? ucnv_getDefaultName() : code);
		UConverter* conv = ucnv_open(cp, &err);
		if(conv == null) return null;
		int len = ucnv_toUChars(conv, null, 0, src, -1, &err) + 1;
		wchar_t* dst = new wchar_t[len];
		memset(dst, 0, len * 2);
		err = U_ZERO_ERROR;
		ucnv_toUChars(conv, dst, len, src, -1, &err);
		ucnv_close(conv);
		return dst;
	}

	str WcToMb(strw src, str code)
	{
		if(src == null) return null;
		UErrorCode err = U_ZERO_ERROR;
		str cp = (code == null ? ucnv_getDefaultName() : code);
		UConverter* conv = ucnv_open(cp, &err);
		if(conv == null) return null;
		int len = ucnv_fromUChars(conv, null, 0, src, -1, &err) + 1;
		char* dst = new char[len];
		memset(dst, 0, len);
		err = U_ZERO_ERROR;
		len = ucnv_fromUChars(conv, dst, len, src, -1, &err);
		ucnv_close(conv);
		return dst;
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
}