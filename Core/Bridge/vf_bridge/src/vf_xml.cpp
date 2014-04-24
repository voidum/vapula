#include "vf_xml.h"
#include "rapidxml\rapidxml.hpp"
#include "rapidxml\rapidxml_utils.hpp"
#include "rapidxml\rapidxml_print.hpp"

namespace vapula
{
	using namespace rapidxml;

	XML::XML()
	{
		_Data = null;
		_Entity = null;
	}

	XML::~XML()
	{
		Clear(_Entity);
		Clear(_Data);
	}

	raw XML::GetEntity()
	{
		return _Entity;
	}

	XML* XML::Load(pcstr path)
	{
		try {
			file<> xf(path);
			return XML::Parse(xf.data());
		} catch (std::exception e) {
			return null;
		}
	}

	XML* XML::Parse(pcstr src)
	{
		XML* xml = new XML();
		xml->_Data = str::Copy(src);
		xml_document<>* xdoc = new xml_document<>();
		xdoc->parse<0>(const_cast<pstr>(xml->_Data));
		xml->_Entity = xdoc;
		return xml;
	}

	pcstr XML::Print(raw xml)
	{
		xml_node<>* obj = (xml_node<>*)xml;
		string s;
		print(std::back_inserter(s), *obj, 0);
		return str::Copy(s.c_str());
	}

	raw XML::Next(raw xml)
	{
		xml_node<>* xe = (xml_node<>*)xml;
		return xe->next_sibling();
	}

	raw XML::XElem(raw xml, pcstr name)
	{
		xml_node<>* xe = (xml_node<>*)xml;
		return xe->first_node(name);
	}

	raw XML::XAttr(raw xml, pcstr name)
	{
		xml_node<>* xe = (xml_node<>*)xml;
		return xe->first_attribute(name);
	}

	raw XML::XPath(raw xml, int count, ...)
	{
		va_list arg_ptr;
		va_start(arg_ptr, count);
		xml_node<>* xe = (xml_node<>*)xml;
		int idx = 0;
		while(idx < count && xe != null)
		{
			xe = xe->first_node(va_arg(arg_ptr, pcstr));
			idx++;
		}
		va_end(arg_ptr);
		return xe;
	}

	pcstr XML::ValStr(raw xml)
	{
		if(xml == null) 
			return null;
		xml_base<>* xbase = (xml_base<>*)xml;
		pcstr v = xbase->value();
		if(strlen(v) > 0)
			return str::Copy(v);
		else
		{
			xml_node<>* xe = (xml_node<>*)xml;
			xml_node<>* xec = xe->first_node();
			if(xec != null && xec->type() == node_cdata)
				return str::Copy(xec->value());
		}
		return null;
	}

	pcwstr XML::ValStrW(raw xml)
	{
		pcstr s8 = ValStr(xml);
		pcwstr s16 = str::ToStrW(s8, _vf_msg_cp);
		delete s8;
		return s16;
	}

	int XML::ValInt(raw xml)
	{
		if(xml == null) 
			return 0;
		pcstr s8 = ValStr(xml);
		int ret = atoi(s8);
		delete s8;
		return ret;
	}

	double XML::ValReal(raw xml)
	{
		if(xml == null) 
			return 0;
		pcstr s8 = ValStr(xml);
		double ret = atof(s8);
		delete s8;
		return ret;
	}

	bool XML::ValBool(raw xml, pcstr judge)
	{
		if(xml == null || judge == null) 
			return 0;
		pcstr s8 = ValStr(xml);
		bool ret = (strcmp(s8, judge) == 0);
		delete s8;
		return ret;
	}
}