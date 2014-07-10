#include "vf_method.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_dataset.h"
#include "vf_xml.h"

namespace vapula
{
	Method::Method()
	{
		_Id = null;
		_HasProtect = false;
		_ProcessSym = null;
		_RollbackSym = null;
		_Dataset = null;
	}

	Method::~Method()
	{
		Clear(_Id);
		Clear(_ProcessSym);
		Clear(_RollbackSym);
		Clear(_Dataset);
	}

	Method* Method::Parse(raw xml)
	{
		raw xe_id = XML::XElem(xml, "id");
		raw xe_protect = XML::XElem(xml, "protect");
		raw xe_symbols = XML::XElem(xml, "symbols");
		raw xe_sym_process = XML::XElem(xe_symbols, "process");
		raw xe_sym_rollback = XML::XElem(xe_symbols, "rollback");
		raw xe_dataset = XML::XElem(xml, "dataset");

		Method* mt = new Method();
		mt->_Id = XML::ValStr(xe_id);
		mt->_HasProtect = XML::ValBool(xe_protect, "true");
		mt->_ProcessSym = XML::ValStr(xe_sym_process);
		mt->_RollbackSym = XML::ValStr(xe_sym_rollback);
		mt->_Dataset = Dataset::Parse(xe_dataset);

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

	bool Method::HasProtect()
	{
		return _HasProtect;
	}
	
	pcstr Method::GetProcessSym()
	{
		return _ProcessSym;
	}

	pcstr Method::GetRollbackSym()
	{
		return _RollbackSym;
	}

	Dataset* Method::GetDataset()
	{
		return _Dataset;
	}
}