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

cstr8 InvokerCLR::GetHandle()
{
	if(_Handle == null) 
	{
		uint64 v = (uint64)this;
		_Handle = str::ValueTo(v);
	}
	return _Handle;
}

bool InvokerCLR::Initialize(Library* lib, int fid)
{
	if(strcmp(lib->GetRuntimeId(), _vf_runtime_id) != 0)
		return false;
	Invoker::Initialize(lib, fid);
	DriverCLR* drv = DriverCLR::GetInstance();
	string arg = GetHandle();
	arg += "|";
	LibraryCLR* lib_clr = dynamic_cast<LibraryCLR*>(lib);
	arg += lib_clr->GetHandle();
	cstr16 s16 = str::ToCh16(arg.c_str());
	drv->CallBridge(L"InitInvoker", s16);
	delete s16;
	return true;
}

void InvokerCLR::_Entry()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	cstr16 s16 = str::ToCh16(GetHandle());
	drv->CallBridge(L"CallEntry", s16);
	delete s16;
}