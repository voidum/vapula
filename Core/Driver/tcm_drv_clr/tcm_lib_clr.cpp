#include "stdafx.h"
#include "tcm_drv_clr.h"
#include "tcm_lib_clr.h"

LibraryCLR::LibraryCLR() 
{
	_Handle = NULL;
}

LibraryCLR::~LibraryCLR()
{
	Clear(_Handle);
}

PCWSTR LibraryCLR::GetHandle()
{
	if(_Handle == NULL) 
	{
		ULONG v = (ULONG)this;
		_Handle = ValueToStrW(v);
	}
	return _Handle;
}

PCSTR LibraryCLR::GetRuntimeId()
{
	return RUNTIME_ID;
}

PCWSTR LibraryCLR::GetBinExt()
{
	return L".dll";
}

bool LibraryCLR::Mount()
{
	wstring arg = GetHandle();
	arg += L"|";
	arg += _Dir;
	arg += _LibId;
	arg += GetBinExt();

	DriverCLR* drv = DriverCLR::GetInstance();
	int ret = drv->CallBridge(L"Mount", arg.c_str());
	if(ret > 0) return false;

	Library::Mount();
	return true;
}

void LibraryCLR::Unmount()
{
	DriverCLR* drv = DriverCLR::GetInstance();
	drv->CallBridge(L"Unmount", GetHandle());
	Library::Unmount();
}