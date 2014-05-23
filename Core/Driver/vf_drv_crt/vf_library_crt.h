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
	raw GetEntry(pcstr id);

public:
	bool Mount();
	void Unmount();
};