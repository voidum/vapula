#include "stdafx.h"
#include "tcm_drv_crt.h"
#include "tcm_lib_crt.h"
#include "tcm_exec_crt.h"

using namespace tcm;

DriverCRT::DriverCRT() { }

DriverCRT::~DriverCRT() { }

PCSTR DriverCRT::GetRuntimeId() 
{
	return RUNTIME_ID;
}

Library* DriverCRT::CreateLibrary()
{
	LibraryCRT* lib = new LibraryCRT();
	return lib;
}

Executor* DriverCRT::CreateExecutor()
{
	ExecutorCRT* exec = new ExecutorCRT();
	return exec;
}

Driver* GetDriverInstance()
{
	DriverCRT* driver = new DriverCRT();
	return driver;
}