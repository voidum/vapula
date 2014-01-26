#pragma once

#include "vf_base.h"
#include <cstdarg>

namespace vapula
{
	//XML wrapper
	class VAPULA_API XML
	{
	private:
		XML();
	public:
		~XML();

	private:
		cstr8 _Data;
		object _Entity;

	public:
		object GetEntity();

	public:
		//load XML
		static XML* Load(cstr8 path);

		//parse XML from UTF8 char string
		static XML* Parse(cstr8 src);

		//print XML to char string
		static cstr8 Print(object xml);

	public:
		//get next sibling
		static object Next(object xml);

		//get element by name
		static object XElem(object xml, cstr8 name);

		//get attribute by name
		static object XAttr(object xml, cstr8 name);

		//query child node by XPath-like method
		static object XPath(object xml, int count, ...);

	public:
		//read node value as UTF8 char string
		static cstr8 ValCh8(object xml);

		//read node value as wide char string
		static cstr16 ValCh16(object xml);

		//read node value as integer
		static int ValInt(object xml);

		//read node value as real
		static double ValReal(object xml);

		//real node value as bool judged by specified string
		static bool ValBool(object xml, cstr8 judge);
	};
}