#include "vf_xml.h"
#include "rapidxml/rapidxml.hpp"
#include "rapidxml/rapidxml_utils.hpp"
#include "rapidxml/rapidxml_print.hpp"

namespace vapula
{
	namespace xml
	{
		using namespace rapidxml;

		object Load(cstr8 path, cstr8& data)
		{
			data = null;
			try
			{
				file<> xfile(path);
				data = str::Copy(xfile.data());
			}
			catch (std::exception e)
			{
				return null;
			}
			xml_document<>* xdoc = new xml_document<>();
			xdoc->parse<0>(const_cast<char*>(data));
			return xdoc;
		}

		object Parse(cstr8 xml)
		{
			xml_document<>* xdoc = new xml_document<>();
			xdoc->parse<0>(const_cast<char*>(xml));
			return xdoc;
		}

		cstr8 Print(object src)
		{
			xml_node<>* xml = (xml_node<>*)src;
			string s;
			print(std::back_inserter(s), *xml, 0);
			return str::Copy(s.c_str());
		}

		object Path(object src, int count, ...)
		{
			va_list arg_ptr;
			va_start(arg_ptr,count);
			xml_node<>* xe = (xml_node<>*)src;
			for (int i = 0; i < count; i++)
			{
				if(xe != null) 
					xe = xe->first_node(va_arg(arg_ptr, cstr8));
				else break;
			}
			va_end(arg_ptr);
			return xe;
		}

		cstr8 ValueCh8(object src)
		{
			if(src == null) 
				return null;
			xml_base<>* xbase = (xml_base<>*)src;
			cstr8 tmp = xbase->value();
			if(strlen(tmp) > 0)
				return str::Copy(tmp);
			else
			{
				xml_node<>* xe = (xml_node<>*)src;
				xml_node<>* xec = xe->first_node();
				if(xec != null && xec->type() == node_cdata)
				{
					tmp = xec->value();
					return str::Copy(tmp);
				}
			}
			return null;
		}

		cstr16 ValueCh16(object src)
		{
			cstr8 s8 = ValueCh8(src);
			cstr16 s16 = str::ToCh16(s8);
			delete s8;
			return s16;
		}

		int ValueInt(object src)
		{
			if(src == null) 
				return 0;
			cstr8 s8 = ValueCh8(src);
			int ret = atoi(s8);
			delete s8;
			return ret;
		}

		double ValueReal(object src)
		{
			if(src == null) 
				return 0;
			cstr8 s8 = ValueCh8(src);
			double ret = atof(s8);
			delete s8;
			return ret;
		}

		bool ValueBool(object src, cstr8 value)
		{
			if(src == null || value == null) 
				return 0;
			cstr8 s8 = ValueCh8(src);
			bool ret = (strcmp(s8, value) == 0);
			delete s8;
			return ret;
		}
	}
}