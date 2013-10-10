#include "stdafx.h"
#include "tcm_drv_clr.h"
#include "tcm_exec_clr.h"
#include "tcm_lib_clr.h"
#include "process.h"

ExecutorCLR::ExecutorCLR()
{
	_Handle = NULL;
}

ExecutorCLR::~ExecutorCLR()
{
	Clear(_Handle);
}

PCWSTR ExecutorCLR::GetHandle()
{
	if(_Handle == NULL) 
	{
		ULONG v = (ULONG)this;
		_Handle = ValueToStrW(v);
	}
	return _Handle;
}

bool ExecutorCLR::Initialize(Library* lib, int fid)
{
	if(strcmp(lib->GetRuntimeId(), RUNTIME_ID) != 0) return false;
	Executor::Initialize(lib, fid);
	DriverCLR* drv = DriverCLR::GetInstance();
	wstring str = GetHandle();
	str += L"|";
	LibraryCLR* lib_clr = dynamic_cast<LibraryCLR*>(lib);
	str += lib_clr->GetHandle();
	drv->CallBridge(L"InitExec", str.c_str());
	return true;
}

UINT ExecutorCLR::_ThreadProc()
{
	Stopwatch* sw = _Context->GetStopwatch();
	sw->Start();
	DriverCLR* drv = DriverCLR::GetInstance();
	int retcode = drv->CallBridge(L"CallEntry", GetHandle());
	sw->Stop();
	_Context->SetReturnCode(_ContextToken, retcode);
	_Context->SetState(_ContextToken, TCM_STATE_IDLE);
	return 0;
}