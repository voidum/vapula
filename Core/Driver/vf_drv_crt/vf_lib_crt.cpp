#include "vf_lib_crt.h"

LibraryCRT::LibraryCRT() 
{
	_Module = null;
	_Entry = null;
}

LibraryCRT::~LibraryCRT() { }

cstr8 LibraryCRT::GetRuntimeId()
{
	return _vf_runtime_id;
}

cstr8 LibraryCRT::GetBinExt() 
{
	return ".dll";
}

bool LibraryCRT::Mount()
{
	std::string path = _Dir;
	path += _LibId;
	path += GetBinExt();
	cstr16 s16 = str::ToCh16(path.c_str());
	_Module = LoadLibrary(s16);
	delete s16;
	if(_Module == null)
		return false;
	_Entry = GetProcAddress(
		_Module, _EntryDpt == null ? "Run" : _EntryDpt);
	Library::Mount();
	return true;
}

void LibraryCRT::Unmount()
{
	FreeLibrary(_Module);
	Library::Unmount();
}

object LibraryCRT::GetEntry()
{
	return _Entry;
}