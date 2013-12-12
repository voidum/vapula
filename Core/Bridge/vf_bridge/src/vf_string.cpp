#include "vf_string.h"
#include "unicode/ucnv.h"

namespace vapula
{
	cstrw MbToWc(cstr src, cstr code)
	{
		if(src == null) 
			return null;
		UErrorCode err = U_ZERO_ERROR;
		cstr cp = (code == null ? ucnv_getDefaultName() : code);
		UConverter* conv = ucnv_open(cp, &err);
		if(conv == null) 
			return null;
		int len = ucnv_toUChars(conv, null, 0, src, -1, &err) + 1;
		wchar_t* dst = new wchar_t[len];
		memset(dst, 0, len * 2);
		err = U_ZERO_ERROR;
		ucnv_toUChars(conv, dst, len, src, -1, &err);
		ucnv_close(conv);
		return dst;
	}

	cstr WcToMb(cstrw src, cstr code)
	{
		if(src == null) 
			return null;
		UErrorCode err = U_ZERO_ERROR;
		cstr cp = (code == null ? ucnv_getDefaultName() : code);
		UConverter* conv = ucnv_open(cp, &err);
		if(conv == null) 
			return null;
		int len = ucnv_fromUChars(conv, null, 0, src, -1, &err) + 1;
		char* dst = new char[len];
		memset(dst, 0, len);
		err = U_ZERO_ERROR;
		len = ucnv_fromUChars(conv, dst, len, src, -1, &err);
		ucnv_close(conv);
		return dst;
	}

	cstr CopyStrA(cstr src)
	{
		if(src == null)
			return null;
		size_t len = strlen(src);
		char* dst = new char[len + 1];
		memcpy(dst, src, len);
		dst[len] = '\0';
		return const_cast<cstr>(dst);
	}

	cstrw CopyStrW(cstrw src)
	{
		if(src == null) 
			return null;
		size_t len = wcslen(src);
		wchar_t* dst = new wchar_t[len + 1];
		memcpy(dst, src, len * 2);
		dst[len] = L'\0';
		return const_cast<cstrw>(dst);
	}

	cstr ReplaceStrA(cstr src, cstr from, cstr to)
	{
		int len = strlen(from);
		if(len < 1) 
			return CopyStrA(src);
		string str_src = src;
		string str_dst = "";
		for(;;)
		{
			uint32 p1 = str_src.find(from);
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

	cstrw ReplaceStrW(cstrw src, cstrw from, cstrw to)
	{
		int len = wcslen(from);
		if(len < 1) return CopyStrW(src);
		wstring str_src = src;
		wstring str_dst = L"";
		for(;;)
		{
			uint32 p1 = str_src.find(from);
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