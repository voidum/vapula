#include "vf_task_crt.h"
#include "vf_library_crt.h"

TaskCRT::TaskCRT()
{
	_EntryProcess = null;
	_EntryRollback = null;
}

TaskCRT::~TaskCRT()
{
}

bool TaskCRT::Bind(Method* method)
{
	Task::Bind(method);
	LibraryCRT* library = (LibraryCRT*)method->GetLibrary();
	_EntryProcess = (Action)library->GetEntry(method->GetProcessSym());
	_EntryRollback = (Action)library->GetEntry(method->GetRollbackSym());
	return true;
}

void TaskCRT::OnProcess()
{
	if(_EntryProcess != null)
		_EntryProcess();
}

void TaskCRT::OnRollback()
{
	if(_EntryRollback != null)
		_EntryRollback();
}