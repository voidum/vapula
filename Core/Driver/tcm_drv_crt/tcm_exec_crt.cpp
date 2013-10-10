#include "stdafx.h"
#include "tcm_exec_crt.h"
#include "tcm_lib_crt.h"
#include "process.h"

ExecutorCRT::ExecutorCRT()
{
	_Entry = NULL;
}

ExecutorCRT::~ExecutorCRT()
{
}

bool ExecutorCRT::Initialize(Library* lib, int fid)
{
	if(strcmp(lib->GetRuntimeId(), RUNTIME_ID) != 0)
		return false;
	Executor::Initialize(lib, fid);
	LibraryCRT* lib_crt = dynamic_cast<LibraryCRT*>(lib);
	_Entry = (Delegate)lib_crt->GetEntry();
	return true;
}

UINT ExecutorCRT::_ThreadProc()
{
	Stopwatch* sw = _Context->GetStopwatch();
	sw->Start();
	int retcode = _Entry(_FuncId, _Envelope, _Context);
	sw->Stop();
	_Context->SetReturnCode(_ContextToken, retcode);
	_Context->SetState(_ContextToken, TCM_STATE_IDLE);
	return 0;
}