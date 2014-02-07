#include "vf_drv_clr.h"
#include "vf_lib_clr.h"

LibraryCLR::LibraryCLR() 
{
	_Handle = null;
}

LibraryCLR::~LibraryCLR()
{
	Clear(_Handle);
}

pcstr LibraryCLR::GetHandle()
{
	if(_Handle == null) 
	{
		uint64 v = (uint64)this;
		_Handle = str::Value(v);
	}
	return _Handle;
}

bool LibraryCLR::Mount()
{
	string arg = GetHandle();
	arg += "|";
	arg += _Path;

	pcwstr cs16 = str::ToStrW(arg.c_str());
	DriverCLR* drv = DriverCLR::GetInstance();
	int ret = drv->CallBridge(L"Mount", cs16);
	delete cs16;

	return ret > 0;
}

void LibraryCLR::Unmount()
{
	pcwstr cs16 = str::ToStrW(GetHandle());
	DriverCLR* drv = DriverCLR::GetInstance();
	drv->CallBridge(L"Unmount", cs16);
	delete cs16;
}