#pragma once

#include "vf_crt.h"

class LibraryCRT : public Library
{
public:
	LibraryCRT();
	~LibraryCRT();
private:
	HMODULE _Module;
public:
	object GetEntry(cstr8 id);

public:
	bool Mount();
	void Unmount();
};