#include "vf_function.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_envelope.h"
#include "vf_invoker.h"
#include "vf_xml.h"

namespace vapula
{
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
		Scoped<XML> obj(XML::Parse(xml));
		if(obj.empty())
			return null;
		object xdoc = obj->GetEntity();
		object xe = XML::XElem(xdoc, "function");
		object xe_id = XML::XElem(xe, "id");
		object xe_entry = XML::XElem(xe, "entry");
		object xe_params = XML::XElem(xe, "params");

		Function* func = new Function();
		func->_Id = XML::ValCh8(xe_id);
		func->_EntrySym = XML::ValCh8(xe_entry);
		astr8 s8_params(XML::Print(xe_params));
		func->_Envelope = Envelope::Parse(s8_params.get());
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