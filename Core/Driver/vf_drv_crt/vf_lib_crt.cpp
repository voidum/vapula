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
	cstr16 s16 = str::ToCh16(_Path);
	_Module = LoadLibrary(s16);
	delete s16;
	return (_Module != null);
}

void LibraryCRT::Unmount()
{
	FreeLibrary(_Module);
	_Module = null;
}

object LibraryCRT::GetEntry(cstr8 id)
{
	object entry = GetProcAddress(
		_Module, 
		id == null ? "Run" : id);
	if(entry == null)
		std::cout<<GetLastError()<<std::endl;
	return entry;
}