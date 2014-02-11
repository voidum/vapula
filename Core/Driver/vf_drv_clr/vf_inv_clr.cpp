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

bool InvokerCLR::Bind(Method* mt)
{
	Invoker::Bind(mt);
	DriverCLR* drv = DriverCLR::GetInstance();
	string arg = GetHandle();
	arg += "|";
	Library* lib = mt->GetLibrary();
	LibraryCLR* lib_clr = dynamic_cast<LibraryCLR*>(lib);
	arg += lib_clr->GetHandle();
	pcwstr cs16 = str::ToStrW(arg.c_str());
	drv->CallBridge(L"InitInvoker", cs16);
	delete cs16;
	return true;
}

void InvokerCLR::OnProcess()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	pcwstr cs16 = str::ToStrW(GetHandle());
	drv->CallBridge(L"CallProcess", cs16);
	delete cs16;
}

void InvokerCLR::OnRollback()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	pcwstr cs16 = str::ToStrW(GetHandle());
	drv->CallBridge(L"CallRollback", cs16);
	delete cs16;
}