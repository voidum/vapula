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

	Function* Function::Parse(pcstr xml)
	{
		XML* xobj = XML::Parse(xml);
		Handle autop_xml(xobj);
		if(xml == null)
			return null;
		object xdoc = xobj->GetEntity();
		object xe = XML::XElem(xdoc, "function");
		object xe_id = XML::XElem(xe, "id");
		object xe_entry = XML::XElem(xe, "entry");
		object xe_params = XML::XElem(xe, "params");

		Function* func = new Function();
		func->_Id = XML::ValStr(xe_id);
		func->_EntrySym = XML::ValStr(xe_entry);
		pcstr cs8_params = XML::Print(xe_params);
		func->_Envelope = Envelope::Parse(cs8_params);
		delete cs8_params;
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

	pcstr Function::GetFunctionId()
	{
		return _Id;
	}
	
	pcstr Function::GetEntrySym()
	{
		return _EntrySym;
	}

	Envelope* Function::GetEnvelope()
	{
		return _Envelope;
	}
}