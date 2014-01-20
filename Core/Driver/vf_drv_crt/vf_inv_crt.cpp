#include "vf_inv_crt.h"
#include "vf_lib_crt.h"
#include "process.h"

InvokerCRT::InvokerCRT()
{
	_Action = null;
}

InvokerCRT::~InvokerCRT()
{
}

bool InvokerCRT::Initialize(Function* func)
{
	Invoker::Initialize(func);
	Library* lib = func->GetLibrary();
	LibraryCRT* lib_crt = dynamic_cast<LibraryCRT*>(lib);
	_Action = (Action)lib_crt->GetEntry(func->GetEntrySym());
	return true;
}

void InvokerCRT::_Entry()
{
	_Action();
}