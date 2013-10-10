#include "stdafx.h"
#include "tcm_lib_crt.h"

LibraryCRT::LibraryCRT() { }

LibraryCRT::~LibraryCRT() { }

PCSTR LibraryCRT::GetRuntimeId()
{
	return RUNTIME_ID;
}

PCWSTR LibraryCRT::GetBinExt() 
{
	return L".dll";
}

bool LibraryCRT::Mount()
{
	std::wstring str = _Dir;
	str += _LibId;
	str += GetBinExt();
	_Module = LoadLibrary(str.c_str());
	if(_Module == NULL) return false;
	_Entry = GetProcAddress(_Module, "Run");
	Library::Mount();
	return true;
}

void LibraryCRT::Unmount()
{
	FreeLibrary(_Module);
	Library::Unmount();
}

LPVOID LibraryCRT::GetEntry()
{
	return _Entry;
}