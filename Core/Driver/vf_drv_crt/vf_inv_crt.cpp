#include "vf_inv_crt.h"
#include "vf_lib_crt.h"
#include "process.h"

InvokerCRT::InvokerCRT()
{
	_PtrEntry = null;
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
	_PtrEntry = (Delegate)lib_crt->GetEntry();
	return true;
}

void InvokerCRT::_Entry()
{
	int retcode = _PtrEntry();
	Context* ctx = _Stack->GetContext();
	ctx->SetReturnCode(retcode);
}