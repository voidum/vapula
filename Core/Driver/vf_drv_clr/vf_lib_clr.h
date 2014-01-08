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
	bool Mount();
	void Unmount();
};