#include "stdafx.h"
#include "tcm_drv_clr.h"
#include "tcm_inv_clr.h"
#include "tcm_lib_clr.h"
#include "process.h"

InvokerCLR::InvokerCLR()
{
	_Handle = NULL;
}

InvokerCLR::~InvokerCLR()
{
	Clear(_Handle);
}

PCWSTR InvokerCLR::GetHandle()
{
	if(_Handle == NULL) 
	{
		ULONG v = (ULONG)this;
		PCSTR ptr_str = ValueToStr(v);
		_Handle = MbToWc(ptr_str);
		delete ptr_str;
	}
	return _Handle;
}

bool InvokerCLR::Initialize(Library* lib, int fid)
{
	if(strcmp(lib->GetRuntimeId(), RUNTIME_ID) != 0) return false;
	Invoker::Initialize(lib, fid);
	DriverCLR* drv = DriverCLR::GetInstance();
	wstring str = GetHandle();
	str += L"|";
	LibraryCLR* lib_clr = dynamic_cast<LibraryCLR*>(lib);
	str += lib_clr->GetHandle();
	drv->CallBridge(L"InitInvoker", str.c_str());
	return true;
}

UINT InvokerCLR::_ThreadProc()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	int retcode = drv->CallBridge(L"CallEntry", GetHandle());
	_Context->SetReturnCode(_ContextToken, retcode);
	_Context->SetState(_ContextToken, TCM_STATE_IDLE);
	return 0;
}