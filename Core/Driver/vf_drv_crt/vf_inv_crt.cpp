#include "vf_inv_crt.h"
#include "vf_lib_crt.h"
#include "process.h"

InvokerCRT::InvokerCRT()
{
	_Action = null;
}

InvokerCRT::~InvokerCRT()
{
}

bool InvokerCRT::Initialize(Library* lib, int fid)
{
	if(strcmp(lib->GetRuntimeId(), _vf_runtime_id) != 0)
		return false;
	Invoker::Initialize(lib, fid);
	LibraryCRT* lib_crt = dynamic_cast<LibraryCRT*>(lib);
	_Action = (Action)lib_crt->GetEntry();
	return true;
}

void InvokerCRT::_Entry()
{
	_Action();
}