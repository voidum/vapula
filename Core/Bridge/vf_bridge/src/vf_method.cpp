#include "vf_method.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_envelope.h"
#include "vf_xml.h"

namespace vapula
{
	Method::Method()
	{
		_Id = null;
		_ProcessSym = null;
		_RollbackSym = null;
		_Envelope = null;
	}

	Method::~Method()
	{
		Clear(_Id);
		Clear(_ProcessSym);
		Clear(_RollbackSym);
		Clear(_Envelope);
	}

	Method* Method::Parse(pcstr xml)
	{
		XML* xobj = XML::Parse(xml);
		Handle autop_xml(xobj);
		if(xobj == null)
			return null;

		object xdoc = xobj->GetEntity();
		object xe = XML::XElem(xdoc, "method");
		object xe_id = XML::XElem(xe, "id");
		object xe_symbols = XML::XElem(xe, "symbols");
		object xe_sym_process = XML::XElem(xe_symbols, "process");
		object xe_sym_rollback = XML::XElem(xe_symbols, "rollback");
		object xe_params = XML::XElem(xe, "params");

		Method* mt = new Method();
		mt->_Id = XML::ValStr(xe_id);
		mt->_ProcessSym = XML::ValStr(xe_sym_process);
		mt->_RollbackSym = XML::ValStr(xe_sym_rollback);

		pcstr cs8_params = XML::Print(xe_params);
		mt->_Envelope = Envelope::Parse(cs8_params);
		delete cs8_params;

		return mt;
	}

	Library* Method::GetLibrary()
	{
		return _Library;
	}

	void Method::SetLibrary(Library* lib)
	{
		_Library = lib;
	}

	pcstr Method::GetMethodId()
	{
		return _Id;
	}
	
	pcstr Method::GetProcessSym()
	{
		return _ProcessSym;
	}

	pcstr Method::GetRollbackSym()
	{
		return _RollbackSym;
	}

	Envelope* Method::GetEnvelope()
	{
		return _Envelope;
	}
}