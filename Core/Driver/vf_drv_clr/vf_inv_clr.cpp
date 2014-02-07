#include "vf_drv_clr.h"
#include "vf_inv_clr.h"
#include "vf_lib_clr.h"

InvokerCLR::InvokerCLR()
{
	_Handle = null;
}

InvokerCLR::~InvokerCLR()
{
	Clear(_Handle);
}

pcstr InvokerCLR::GetHandle()
{
	if(_Handle == null) 
	{
		uint64 v = (uint64)this;
		_Handle = str::Value(v);
	}
	return _Handle;
}

bool InvokerCLR::Initialize(Function* func)
{
	Invoker::Initialize(func);
	DriverCLR* drv = DriverCLR::GetInstance();
	string arg = GetHandle();
	arg += "|";
	Library* lib = func->GetLibrary();
	LibraryCLR* lib_clr = dynamic_cast<LibraryCLR*>(lib);
	arg += lib_clr->GetHandle();
	pcwstr cs16 = str::ToStrW(arg.c_str());
	drv->CallBridge(L"InitInvoker", cs16);
	delete cs16;
	return true;
}

void InvokerCLR::_Entry()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	pcwstr cs16 = str::ToStrW(GetHandle());
	drv->CallBridge(L"CallEntry", cs16);
	delete cs16;
}