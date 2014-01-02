#pragma once

#include "vf_const.h"

namespace vapula
{
	namespace str
	{
		//convert value to string
		template<typename T>
		VAPULA_API cstr8 ValueTo(T value)
		{
			std::ostringstream oss;
			oss.imbue(std::locale("C"));
			oss<<value;
			return Copy(oss.str().c_str());
		}

		//convert wide char string to char string
		//from UTF16, default to system
		VAPULA_API cstr8 ToCh8(cstr16 src, cstr8 cp = null);

		//convert char string to wide char string
		//default from system, to UTF16
		VAPULA_API cstr16 ToCh16(cstr8 src, cstr8 cp = null);

		//convert encoding of char string
		VAPULA_API cstr8 EncodeCh8(cstr8 src, cstr8 cp_from, cstr8 cp_to);

		//copy string
		VAPULA_API cstr8 Copy(cstr8 src);
		VAPULA_API cstr16 Copy(cstr16 src);

		//replace sub string in string
		//default char string in UTF8
		VAPULA_API cstr8 Replace(cstr8 src, cstr8 from, cstr8 to);
		VAPULA_API cstr16 Replace(cstr16 src, cstr16 from, cstr16 to);
	}
}