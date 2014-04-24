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
		raw _Entity;

	public:
		raw GetEntity();

	public:
		//load XML
		static XML* Load(pcstr path);

		//parse XML from UTF8 char string
		static XML* Parse(pcstr src);

		//print XML to char string
		static pcstr Print(raw xml);

	public:
		//get next sibling
		static raw Next(raw xml);

		//get element by name
		static raw XElem(raw xml, pcstr name);

		//get attribute by name
		static raw XAttr(raw xml, pcstr name);

		//query child node by XPath-like method
		static raw XPath(raw xml, int count, ...);

	public:
		//read node value as UTF8 char string
		static pcstr ValStr(raw xml);

		//read node value as wide char string
		static pcwstr ValStrW(raw xml);

		//read node value as integer
		static int ValInt(raw xml);

		//read node value as real
		static double ValReal(raw xml);

		//real node value as bool judged by specified string
		static bool ValBool(raw xml, pcstr judge);
	};
}