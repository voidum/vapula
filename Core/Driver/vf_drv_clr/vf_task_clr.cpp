#include "vf_driver_clr.h"
#include "vf_task_clr.h"
#include "vf_library_clr.h"

TaskCLR::TaskCLR()
{
	_Handle = null;
}

TaskCLR::~TaskCLR()
{
	Clear(_Handle);
}

pcstr TaskCLR::GetHandle()
{
	if(_Handle == null) 
	{
		uint64 v = (uint64)this;
		_Handle = str::Value(v);
	}
	return _Handle;
}

bool TaskCLR::Bind(Method* method)
{
	Task::Bind(method);
	DriverCLR* driver = DriverCLR::GetInstance();
	LibraryCLR* library = (LibraryCLR*)method->GetLibrary();

	string args = GetHandle();
	args += "|";
	args += library->GetHandle();

	driver->CallBridge("InitInvoker", args.c_str());
	return true;
}

void TaskCLR::OnProcess()
{
	DriverCLR* driver = DriverCLR::GetInstance();
	driver->CallBridge("OnProcess", GetHandle());
}

void TaskCLR::OnRollback()
{
	DriverCLR* driver = DriverCLR::GetInstance();
	driver->CallBridge("OnRollback", GetHandle());
}