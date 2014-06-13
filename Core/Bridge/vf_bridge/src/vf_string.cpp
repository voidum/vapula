#include "vf_string.h"

namespace vapula
{
	namespace str
	{
		pcstr ToStr(pcwstr src, uint32 cp)
		{
			if (src == null)
				return null;
			int len = WideCharToMultiByte(cp, NULL, src, -1, NULL, 0, NULL, NULL);
			char* dst = new char[len];
			memset(dst, 0, len);
			WideCharToMultiByte(cp, NULL, src, -1, dst, len, NULL, NULL);
			return dst;
		}

		pcwstr ToStrW(pcstr src, uint32 cp)
		{
			if(src == null) 
				return null;
			int len = MultiByteToWideChar(cp, NULL, src, -1, NULL, 0);
			wchar_t* dst = new wchar_t[len];
			memset(dst, 0, len * 2);
			MultiByteToWideChar(cp, NULL, src, -1, dst, len);
			return dst;
		}

		pcstr Encode(pcstr src, uint32 cp_from, uint32 cp_to)
		{
			pcwstr cs16 = ToStrW(src, cp_from);
			pcstr cs8 = ToStr(cs16, cp_to);
			delete cs16;
			return cs8;
		}

		pcstr Copy(pcstr src)
		{
			if(src == null)
				return null;
			size_t len = strlen(src);
			char* dst = new char[len + 1];
			memcpy(dst, src, len);
			dst[len] = 0;
			return const_cast<pcstr>(dst);
		}

		pcwstr Copy(pcwstr src)
		{
			if(src == null) 
				return null;
			size_t len = wcslen(src);
			wchar_t* dst = new wchar_t[len + 1];
			memcpy(dst, src, len * 2);
			dst[len] = 0;
			return const_cast<pcwstr>(dst);
		}

		pcstr Replace(pcstr src, pcstr from, pcstr to)
		{
			pcwstr cs16_src = ToStrW(src, _vf_cp_msg);
			pcwstr cs16_from = ToStrW(from, _vf_cp_msg);
			pcwstr cs16_to = ToStrW(to, _vf_cp_msg);
			pcwstr cs16_ret = Replace(cs16_src, cs16_from, cs16_to);
			pcstr cs8_ret = ToStr(cs16_ret, _vf_cp_msg);
			delete cs16_src;
			delete cs16_from;
			delete cs16_to;
			delete cs16_ret;
			return cs8_ret;
		}

		pcwstr Replace(pcwstr src, pcwstr from, pcwstr to)
		{
			if(src == null || from == null)
				return null;
			int len_from = wcslen(from);
			if(len_from < 1) 
				return Copy(src);
			wstring str_src = src;
			wstring str_dst = L"";
			for(;;)
			{
				uint32 p1 = str_src.find(from);
				if(p1 != wstring::npos) 
				{
					str_dst += str_src.substr(0, p1);
					str_dst += to;
					str_src = str_src.substr(p1 + len_from);
				}
				else
				{
					str_dst += str_src;
					break;
				}
			}
			return Copy(str_dst.c_str());
		}
	}
}