#pragma once

#include "vf_const.h"

namespace vapula
{
	namespace str
	{
		//convert value to string
		template<typename T>
		VAPULA_API pcstr Value(T value)
		{
			std::ostringstream oss;
			oss.imbue(std::locale("C"));
			oss<<value;
			return Copy(oss.str().c_str());
		}

		//convert UTF16 wide chars to chars
		//to UTF8 by default
		VAPULA_API pcstr ToStr(pcwstr src, uint32 cp = CP_UTF8);

		//convert chars to UTF16 wide chars
		//from OEM by default
		VAPULA_API pcwstr ToStrW(pcstr src, uint32 cp = CP_OEMCP);

		//convert chars encoding
		VAPULA_API pcstr Encode(pcstr src, uint32 cp_from, uint32 cp_to);

		//copy chars
		VAPULA_API pcstr Copy(pcstr src);

		//copy wide chars
		VAPULA_API pcwstr Copy(pcwstr src);

		//replace sub string in string
		//need UTF8 input
		VAPULA_API pcstr Replace(pcstr src, pcstr from, pcstr to);

		//replace sub string in string
		//need UTF16 input
		VAPULA_API pcwstr Replace(pcwstr src, pcwstr from, pcwstr to);
	}
}