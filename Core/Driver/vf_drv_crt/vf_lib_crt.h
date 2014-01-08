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
	object GetEntry();

public:
	bool Mount();
	void Unmount();
};