#include "stdafx.h"
#include "tcm_xml.h"
#include "rapidxml/rapidxml.hpp"
#include "rapidxml/rapidxml_utils.hpp"
#include "rapidxml/rapidxml_print.hpp"

namespace tcm
{
	namespace xml
	{
		using std::string;
		using std::runtime_error;
		using namespace rapidxml;

		LPVOID Load(PCWSTR path, PCSTR& data)
		{
			std::locale::global(std::locale(""));
			PCSTR patha = WcToMb(path, CP_OEMCP);
			if(patha == NULL) return NULL;
			data = NULL;
			try
			{
				file<> xfile(patha);
				Clear(patha);
				data = CopyStrA(xfile.data());
			}
			catch (runtime_error e)
			{
				Clear(patha);
				ShowMsgbox(e.what(), L"TCM Bridge Error");
				return NULL;
			}
			if(data == NULL) return NULL;
			xml_document<>* xdoc = new xml_document<>();
			xdoc->parse<0>(const_cast<PSTR>(data));
			return xdoc;
		}

		LPVOID Parse(PCSTR xml)
		{
			xml_document<>* xdoc = new xml_document<>();
			xdoc->parse<0>(const_cast<PSTR>(xml));
			return xdoc;
		}

		PCSTR Print(LPVOID src)
		{
			xml_node<>* xml = (xml_node<>*)src;
			string s;
			print(std::back_inserter(s), *xml, 0);
			return CopyStrA(s.c_str());
		}

		LPVOID Path(LPVOID src, int count, ...)
		{
			va_list arg_ptr;
			va_start(arg_ptr,count);
			xml_node<>* xe = (xml_node<>*)src;
			for (int i = 0; i < count; i++)
			{
				if(xe != NULL) xe = xe->first_node(va_arg(arg_ptr,PCSTR));
				else break;
			}
			va_end(arg_ptr);
			return xe;
		}

		PCSTR ValueA(LPVOID src)
		{
			if(src == NULL) return NULL;
			xml_base<>* xbase = (xml_base<>*)src;
			PCSTR tmp = xbase->value();
			if(strlen(tmp) > 0) return CopyStrA(tmp);
			else
			{
				xml_node<>* xe = (xml_node<>*)src;
				xml_node<>* xec = xe->first_node();
				if(xec != NULL && xec->type() == node_cdata)
				{
					tmp = xec->value();
					return CopyStrA(tmp);
				}
			}
			return NULL;
		}

		PCWSTR ValueW(LPVOID src)
		{
			PCSTR tmpa = ValueA(src);
			if(tmpa == NULL) return NULL;
			return MbToWc(tmpa);
		}

		int ValueInt(LPVOID src)
		{
			if(src == NULL) return 0;
			PCSTR tmpa = ValueA(src);
			int ret = atoi(tmpa);
			delete tmpa;
			return ret;
		}

		double ValueReal(LPVOID src)
		{
			if(src == NULL) return 0;
			PCSTR tmpa = ValueA(src);
			double ret = atof(tmpa);
			delete tmpa;
			return ret;
		}

		bool ValueBool(LPVOID src, PCSTR value)
		{
			if(src == NULL || value == NULL) return 0;
			PCSTR tmpa = ValueA(src);
			bool ret = (strcmp(tmpa, value) == 0);
			delete tmpa;
			return ret;
		}
	}
}