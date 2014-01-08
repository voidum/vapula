#include "vf_lib_crt.h"

LibraryCRT::LibraryCRT() 
{
	_Module = null;
	_Entry = null;
}

LibraryCRT::~LibraryCRT() { }

bool LibraryCRT::Mount()
{
	cstr16 s16 = str::ToCh16(_Path);
	_Module = LoadLibrary(s16);
	delete s16;
	if(_Module == null)
		return false;
	_Entry = GetProcAddress(
		_Module, _EntrySym == null ? "Run" : _EntrySym);
	return true;
}

void LibraryCRT::Unmount()
{
	FreeLibrary(_Module);
}

object LibraryCRT::GetEntry()
{
	return _Entry;
}