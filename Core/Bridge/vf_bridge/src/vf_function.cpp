#include "vf_function.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_envelope.h"
#include "vf_invoker.h"
#include "vf_xml.h"
#include "rapidxml/rapidxml.hpp"

namespace vapula
{
	using rapidxml::xml_node;
	using rapidxml::xml_document;
	using std::wstring;

	Function::Function()
	{
		_Id = null;
		_EntrySym = null;
		_Envelope = null;
	}

	Function::~Function()
	{
		Clear(_Id);
		Clear(_EntrySym);
		Clear(_Envelope);
	}

	Function* Function::Parse(cstr8 xml)
	{
		xml_node<>* xdoc
			= (xml_node<>*)xml::Parse(xml);
		if(xdoc == null) 
			return null;
		xml_node<>* xe = xdoc->first_node("function");

		Function* func = new Function();
		func->_Id = xml::ValueCh8(xe->first_node("id"));
		func->_EntrySym = xml::ValueCh8(xe->first_node("entry"));

		cstr8 s8_params = xml::Print(xe->first_node("params"));
		func->_Envelope = Envelope::Parse(s8_params);
		delete s8_params;

		return func;
	}

	Library* Function::GetLibrary()
	{
		return _Library;
	}

	void Function::SetLibrary(Library* lib)
	{
		_Library = lib;
	}

	cstr8 Function::GetFunctionId()
	{
		return _Id;
	}
	
	cstr8 Function::GetEntrySym()
	{
		return _EntrySym;
	}

	Envelope* Function::GetEnvelope()
	{
		return _Envelope;
	}
}