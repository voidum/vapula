#include "vf_driver_clr.h"
#include "vf_task_clr.h"
#include "vf_library_clr.h"
#include "vf_stack.h"

TaskCLR::TaskCLR()
{
	_Method = null;
}

TaskCLR::~TaskCLR()
{
}

pcstr TaskCLR::GetHandle()
{
	LibraryCLR* library = (LibraryCLR*)_Method->GetLibrary();
	return library->GetHandle();
}

bool TaskCLR::Bind(Method* method)
{
	Task::Bind(method);
	_Method = method;
	return true;
}

void TaskCLR::OnProcess()
{
	string args = GetHandle();
	args += "|";
	args += _Method->GetProcessSym();

	DriverCLR* driver = DriverCLR::Instance();
	driver->CallBridge("OnProcess", args.c_str());
}

void TaskCLR::OnRollback()
{
	string args = GetHandle();
	args += "|";
	args += _Method->GetRollbackSym();

	DriverCLR* driver = DriverCLR::Instance();
	driver->CallBridge("OnRollback", args.c_str());
}