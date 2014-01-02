#pragma once

#include "vf_base.h"
#include <cstdarg>

namespace vapula
{
	//XML utilities (need: RapidXML)
	namespace xml
	{
		//load XML
		//data should be deleted manually after XML process
		VAPULA_API object Load(cstr8 path, cstr8& data);

		//parse XML from char string
		VAPULA_API object Parse(cstr8 xml);

		//print XML to char string
		VAPULA_API cstr8 Print(object src);

		//query child node as XPath
		VAPULA_API object Path(object src, int count, ...);

		//read node value as char string
		//encoding must be UTF8
		VAPULA_API cstr8 ValueCh8(object src);

		//read node value as wide char string
		VAPULA_API cstr16 ValueCh16(object src);

		//read node value as integer
		VAPULA_API int ValueInt(object src);

		//read node value as real
		VAPULA_API double ValueReal(object src);

		//real node value as bool judged by specified string
		VAPULA_API bool ValueBool(object src, cstr8 value);
	}
}