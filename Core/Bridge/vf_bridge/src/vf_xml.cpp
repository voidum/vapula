#include "vf_xml.h"
#include "rapidxml/rapidxml.hpp"
#include "rapidxml/rapidxml_utils.hpp"
#include "rapidxml/rapidxml_print.hpp"

namespace vapula
{
	namespace xml
	{
		using namespace rapidxml;

		object Load(cstrw path, cstr& data)
		{
			cstr path_a = WcToMb(path);
			if(path_a == null) 
				return null;
			data = null;
			try
			{
				file<> xfile(path_a);
				Clear(path_a);
				data = CopyStrA(xfile.data());
			}
			catch (std::exception e)
			{
				Clear(path_a);
				return null;
			}
			if(data == null) return null;
			xml_document<>* xdoc = new xml_document<>();
			xdoc->parse<0>(const_cast<char*>(data));
			return xdoc;
		}

		object Parse(cstr xml)
		{
			xml_document<>* xdoc = new xml_document<>();
			xdoc->parse<0>(const_cast<char*>(xml));
			return xdoc;
		}

		cstr Print(object src)
		{
			xml_node<>* xml = (xml_node<>*)src;
			string s;
			print(std::back_inserter(s), *xml, 0);
			return CopyStrA(s.c_str());
		}

		object Path(object src, int count, ...)
		{
			va_list arg_ptr;
			va_start(arg_ptr,count);
			xml_node<>* xe = (xml_node<>*)src;
			for (int i = 0; i < count; i++)
			{
				if(xe != null) 
					xe = xe->first_node(va_arg(arg_ptr, str));
				else break;
			}
			va_end(arg_ptr);
			return xe;
		}

		cstr ValueA(object src)
		{
			if(src == null) 
				return null;
			xml_base<>* xbase = (xml_base<>*)src;
			cstr tmp = xbase->value();
			if(strlen(tmp) > 0) return CopyStrA(tmp);
			else
			{
				xml_node<>* xe = (xml_node<>*)src;
				xml_node<>* xec = xe->first_node();
				if(xec != null && xec->type() == node_cdata)
				{
					tmp = xec->value();
					return CopyStrA(tmp);
				}
			}
			return null;
		}

		cstrw ValueW(object src)
		{
			cstr tmpa = ValueA(src);
			if(tmpa == null) return null;
			return MbToWc(tmpa);
		}

		int ValueInt(object src)
		{
			if(src == null) 
				return 0;
			cstr tmpa = ValueA(src);
			int ret = atoi(tmpa);
			delete tmpa;
			return ret;
		}

		double ValueReal(object src)
		{
			if(src == null) 
				return 0;
			cstr tmpa = ValueA(src);
			double ret = atof(tmpa);
			delete tmpa;
			return ret;
		}

		bool ValueBool(object src, cstr value)
		{
			if(src == null || value == null) 
				return 0;
			cstr tmpa = ValueA(src);
			bool ret = (strcmp(tmpa, value) == 0);
			delete tmpa;
			return ret;
		}
	}
}