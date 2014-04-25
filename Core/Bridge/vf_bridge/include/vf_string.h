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

		//convert wide char string to char string
		//from UTF16, default to system
		VAPULA_API pcstr ToStr(pcwstr src, pcstr cp = null);

		//convert char string to wide char string
		//default from system, to UTF16
		VAPULA_API pcwstr ToStrW(pcstr src, pcstr cp = null);

		//convert encoding of char string
		VAPULA_API pcstr Encode(pcstr src, pcstr cp_from, pcstr cp_to);

		//copy string
		VAPULA_API pcstr Copy(pcstr src);
		VAPULA_API pcwstr Copy(pcwstr src);

		//replace sub string in string
		//default char string in UTF8
		VAPULA_API pcstr Replace(pcstr src, pcstr from, pcstr to);
		VAPULA_API pcwstr Replace(pcwstr src, pcwstr from, pcwstr to);
	}
}