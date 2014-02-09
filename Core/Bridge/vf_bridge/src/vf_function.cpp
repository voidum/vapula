#include "vf_function.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_envelope.h"
#include "vf_xml.h"

namespace vapula
{
	Function::Function()
	{
		_Id = null;
		_ProcessSym = null;
		_RollbackSym = null;
		_Envelope = null;
	}

	Function::~Function()
	{
		Clear(_Id);
		Clear(_ProcessSym);
		Clear(_RollbackSym);
		Clear(_Envelope);
	}

	Function* Function::Parse(pcstr xml)
	{
		XML* xobj = XML::Parse(xml);
		Handle autop_xml(xobj);
		if(xobj == null)
			return null;

		object xdoc = xobj->GetEntity();
		object xe = XML::XElem(xdoc, "function");
		object xe_id = XML::XElem(xe, "id");
		object xe_symbols = XML::XElem(xe, "symbols");
		object xe_sym_process = XML::XElem(xe_symbols, "process");
		object xe_sym_rollback = XML::XElem(xe_symbols, "rollback");
		object xe_params = XML::XElem(xe, "params");

		Function* func = new Function();
		func->_Id = XML::ValStr(xe_id);
		func->_ProcessSym = XML::ValStr(xe_sym_process);
		func->_RollbackSym = XML::ValStr(xe_sym_rollback);

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
	
	pcstr Function::GetProcessSym()
	{
		return _ProcessSym;
	}

	pcstr Function::GetRollbackSym()
	{
		return _RollbackSym;
	}

	Envelope* Function::GetEnvelope()
	{
		return _Envelope;
	}
}