#include "vf_inv_crt.h"
#include "vf_lib_crt.h"
#include "process.h"

InvokerCRT::InvokerCRT()
{
	_Entry = null;
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
	_Entry = (Delegate)lib_crt->GetEntry();
	return true;
}

uint32 InvokerCRT::_ThreadProc()
{
	int retcode = _Entry(_FuncId, _Envelope, _Context);
	_Context->SetReturnCode(retcode);
	_Context->SetState(VF_STATE_IDLE);
	return 0;
}