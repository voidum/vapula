#include "vf_drv_crt.h"
#include "vf_lib_crt.h"
#include "vf_inv_crt.h"

DriverCRT::DriverCRT() { }

DriverCRT::~DriverCRT() { }

cstr8 DriverCRT::GetRuntimeId() 
{
	return _vf_runtime_id;
}

Library* DriverCRT::CreateLibrary()
{
	LibraryCRT* lib = new LibraryCRT();
	return lib;
}

Invoker* DriverCRT::CreateInvoker()
{
	InvokerCRT* inv = new InvokerCRT();
	return inv;
}

Driver* GetDriverInstance()
{
	DriverCRT* driver = new DriverCRT();
	return driver;
}