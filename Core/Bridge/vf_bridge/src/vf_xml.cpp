#include "vf_xml.h"
#include "rapidxml/rapidxml.hpp"
#include "rapidxml/rapidxml_utils.hpp"
#include "rapidxml/rapidxml_print.hpp"

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

	object XML::GetEntity()
	{
		return _Entity;
	}

	XML* XML::Load(cstr8 path)
	{
		try {
			file<> xf(path);
			return XML::Parse(xf.data());
		} catch (std::exception e) {
			return null;
		}
	}

	XML* XML::Parse(cstr8 src)
	{
		XML* xml = new XML();
		xml->_Data = str::Copy(src);
		xml_document<>* xdoc = new xml_document<>();
		xdoc->parse<0>(const_cast<str8>(xml->_Data));
		xml->_Entity = xdoc;
		return xml;
	}

	cstr8 XML::Print(object xml)
	{
		xml_node<>* obj = (xml_node<>*)xml;
		string s;
		print(std::back_inserter(s), *obj, 0);
		return str::Copy(s.c_str());
	}

	object XML::Next(object xml)
	{
		xml_node<>* xe = (xml_node<>*)xml;
		return xe->next_sibling();
	}

	object XML::XElem(object xml, cstr8 name)
	{
		xml_node<>* xe = (xml_node<>*)xml;
		return xe->first_node(name);
	}

	object XML::XAttr(object xml, cstr8 name)
	{
		xml_node<>* xe = (xml_node<>*)xml;
		return xe->first_attribute(name);
	}

	object XML::XPath(object xml, int count, ...)
	{
		va_list arg_ptr;
		va_start(arg_ptr, count);
		xml_node<>* xe = (xml_node<>*)xml;
		int idx = 0;
		while(idx < count && xe != null)
		{
			xe = xe->first_node(va_arg(arg_ptr, cstr8));
			idx++;
		}
		va_end(arg_ptr);
		return xe;
	}

	cstr8 XML::ValCh8(object xml)
	{
		if(xml == null) 
			return null;
		xml_base<>* xbase = (xml_base<>*)xml;
		cstr8 v = xbase->value();
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

	cstr16 XML::ValCh16(object xml)
	{
		astr8 s8(ValCh8(xml));
		cstr16 s16 = str::ToCh16(s8.get(), _vf_msg_cp);
		return s16;
	}

	int XML::ValInt(object xml)
	{
		if(xml == null) 
			return 0;
		astr8 s8(ValCh8(xml));
		int ret = atoi(s8.get());
		return ret;
	}

	double XML::ValReal(object xml)
	{
		if(xml == null) 
			return 0;
		astr8 s8(ValCh8(xml));
		double ret = atof(s8.get());
		return ret;
	}

	bool XML::ValBool(object xml, cstr8 judge)
	{
		if(xml == null || judge == null) 
			return 0;
		astr8 s8(ValCh8(xml));
		bool ret = (strcmp(s8.get(), judge) == 0);
		return ret;
	}
}