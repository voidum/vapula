#include "stdafx.h"
#include "tcm_inv_crt.h"
#include "tcm_lib_crt.h"
#include "process.h"

InvokerCRT::InvokerCRT()
{
	_Entry = NULL;
}

InvokerCRT::~InvokerCRT()
{
}

bool InvokerCRT::Initialize(Library* lib, int fid)
{
	if(strcmp(lib->GetRuntimeId(), RUNTIME_ID) != 0)
		return false;
	Invoker::Initialize(lib, fid);
	LibraryCRT* lib_crt = dynamic_cast<LibraryCRT*>(lib);
	_Entry = (Delegate)lib_crt->GetEntry();
	return true;
}

UINT InvokerCRT::_ThreadProc()
{
	int retcode = _Entry(_FuncId, _Envelope, _Context);
	_Context->SetReturnCode(_ContextToken, retcode);
	_Context->SetState(_ContextToken, TCM_STATE_IDLE);
	return 0;
}