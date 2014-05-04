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
	string args = GetHandle();
	args += "|";
	args += _Path;

	DriverCLR* drv = DriverCLR::GetInstance();
	int ret = drv->CallBridge("Mount", args.c_str());
	return ret == TRUE;
}

void LibraryCLR::Unmount()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	drv->CallBridge("Unmount", GetHandle());
}