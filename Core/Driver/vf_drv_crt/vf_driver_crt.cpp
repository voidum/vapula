#include "vf_driver_crt.h"
#include "vf_library_crt.h"
#include "vf_task_crt.h"

DriverCRT::DriverCRT() { }

DriverCRT::~DriverCRT() { }

pcstr DriverCRT::GetRuntimeId() 
{
	return _vf_runtime_id;
}

pcstr DriverCRT::GetBinExt()
{
	return "dll";
}

Library* DriverCRT::CreateLibrary()
{
	LibraryCRT* lib = new LibraryCRT();
	return lib;
}

Task* DriverCRT::CreateTask()
{
	TaskCRT* task = new TaskCRT();
	return task;
}

Driver* GetDriverInstance()
{
	DriverCRT* driver = new DriverCRT();
	return driver;
}