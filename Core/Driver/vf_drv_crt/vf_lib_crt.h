#pragma once

#include "vf_crt.h"

class LibraryCRT : public Library
{
public:
	LibraryCRT();
	~LibraryCRT();
private:
	HMODULE _Module;
	object _Entry;
public:
	cstr8 GetRuntimeId();

	cstr8 GetBinExt();

	bool Mount();

	void Unmount();
public:
	object GetEntry();
};