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
	Library* lib = mt->GetLibrary();
	LibraryCLR* lib_clr = dynamic_cast<LibraryCLR*>(lib);

	string args = GetHandle();
	args += "|";
	args += lib_clr->GetHandle();

	drv->CallBridge("InitInvoker", args.c_str());
	return true;
}

void InvokerCLR::OnProcess()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	drv->CallBridge("OnProcess", GetHandle());
}

void InvokerCLR::OnRollback()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	drv->CallBridge("OnRollback", GetHandle());
}