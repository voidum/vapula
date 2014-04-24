#include "vf_string.h"
#include "unicode\ucnv.h"
#include "modp_b64\modp_b64.h"

namespace vapula
{
	namespace str
	{
		pcstr ToStr(pcwstr src, pcstr cp)
		{
			if(src == null) 
				return null;
			UErrorCode err = U_ZERO_ERROR;
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

		pcwstr ToStrW(pcstr src, pcstr cp)
		{
			if(src == null) 
				return null;
			UErrorCode err = U_ZERO_ERROR;
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

		pcstr Encode(pcstr src, pcstr cp_from, pcstr cp_to)
		{
			pcwstr s16 = ToStrW(src, cp_from);
			pcstr s8 = ToStr(s16, cp_to);
			delete s16;
			return s8;
		}

		pcstr Copy(pcstr src)
		{
			if(src == null)
				return null;
			size_t len = strlen(src);
			pstr dst = new char[len + 1];
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
			pcwstr src16 = ToStrW(src, _vf_msg_cp);
			pcwstr from16 = ToStrW(from, _vf_msg_cp);
			pcwstr to16 = ToStrW(to, _vf_msg_cp);
			pcwstr ret16 = Replace(src16, from16, to16);
			pcstr ret = ToStr(ret16, _vf_msg_cp);
			delete src16;
			delete from16;
			delete to16;
			delete ret16;
			return ret;
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

		pcstr ToBase64(raw src, uint32 size)
		{
			uint32 dst_size = modp_b64_encode_len(size);
			pstr dst = new char[dst_size];
			int ret = modp_b64_encode(dst, (pcstr)src, size);
			if (ret == -1)
			{
				delete dst;
				dst = null;
			}
			return dst;
		}

		raw FromBase64(pcstr src)
		{
			uint32 src_size = strlen(src);
			uint32 dst_size = modp_b64_decode_len(src_size);
			pstr dst = new char[dst_size];
			int ret = modp_b64_decode(dst, src, src_size);
			if (ret == -1)
			{
				delete dst;
				dst = null;
			}
			return dst;
		}
	}
}