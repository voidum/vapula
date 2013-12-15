#pragma once

#include "vf_clr.h"

class LibraryCLR : public Library
{
public:
	LibraryCLR();
	~LibraryCLR();
private:
	cstr8 _Handle;
public:
	cstr8 GetHandle();
public:
	cstr8 GetRuntimeId();

	cstr8 GetBinExt();

	bool Mount();

	void Unmount();
};