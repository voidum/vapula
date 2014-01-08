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

cstr8 LibraryCLR::GetHandle()
{
	if(_Handle == null) 
	{
		uint64 v = (uint64)this;
		_Handle = str::ValueTo(v);
	}
	return _Handle;
}

bool LibraryCLR::Mount()
{
	string arg = GetHandle();
	arg += "|";
	arg += _Path;

	cstr16 s16 = str::ToCh16(arg.c_str());
	DriverCLR* drv = DriverCLR::GetInstance();
	int ret = drv->CallBridge(L"Mount", s16);
	delete s16;

	return ret > 0;
}

void LibraryCLR::Unmount()
{
	cstr16 s16 = str::ToCh16(GetHandle());
	DriverCLR* drv = DriverCLR::GetInstance();
	drv->CallBridge(L"Unmount", s16);
	delete s16;
}