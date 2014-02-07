#include "vf_lib_crt.h"

LibraryCRT::LibraryCRT() 
{
	_Module = null;
}

LibraryCRT::~LibraryCRT()
{
	if(_Module != null)
		FreeLibrary(_Module);
}

bool LibraryCRT::Mount()
{
	pcwstr cs16 = str::ToStrW(_Path);
	_Module = LoadLibrary(cs16);
	delete cs16;
	return (_Module != null);
}

void LibraryCRT::Unmount()
{
	FreeLibrary(_Module);
	_Module = null;
}

object LibraryCRT::GetEntry(pcstr id)
{
	object entry = GetProcAddress(
		_Module, 
		id == null ? "Run" : id);
	if(entry == null)
		std::cout<<GetLastError()<<std::endl;
	return entry;
}