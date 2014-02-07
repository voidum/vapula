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
		pcstr _Data;
		object _Entity;

	public:
		object GetEntity();

	public:
		//load XML
		static XML* Load(pcstr path);

		//parse XML from UTF8 char string
		static XML* Parse(pcstr src);

		//print XML to char string
		static pcstr Print(object xml);

	public:
		//get next sibling
		static object Next(object xml);

		//get element by name
		static object XElem(object xml, pcstr name);

		//get attribute by name
		static object XAttr(object xml, pcstr name);

		//query child node by XPath-like method
		static object XPath(object xml, int count, ...);

	public:
		//read node value as UTF8 char string
		static pcstr ValStr(object xml);

		//read node value as wide char string
		static pcwstr ValStrW(object xml);

		//read node value as integer
		static int ValInt(object xml);

		//read node value as real
		static double ValReal(object xml);

		//real node value as bool judged by specified string
		static bool ValBool(object xml, pcstr judge);
	};
}