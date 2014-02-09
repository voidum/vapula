#include "vf_inv_crt.h"
#include "vf_lib_crt.h"
#include "process.h"

InvokerCRT::InvokerCRT()
{
	_EntryProcess = null;
	_EntryRollback = null;
}

InvokerCRT::~InvokerCRT()
{
}

bool InvokerCRT::Bind(Function* func)
{
	Invoker::Bind(func);
	Library* lib = func->GetLibrary();
	LibraryCRT* lib_crt = dynamic_cast<LibraryCRT*>(lib);
	_EntryProcess = (Action)lib_crt->GetEntry(func->GetProcessSym());
	_EntryRollback = (Action)lib_crt->GetEntry(func->GetRollbackSym());
	return true;
}

void InvokerCRT::OnProcess()
{
	if(_EntryProcess != null)
		_EntryProcess();
}

void InvokerCRT::OnRollback()
{
	if(_EntryRollback != null)
		_EntryRollback();
}