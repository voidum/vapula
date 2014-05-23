#pragma once

#include "vf_clr.h"

class LibraryCLR : public Library
{
public:
	LibraryCLR();
	~LibraryCLR();
private:
	pcstr _Handle;
public:
	pcstr GetHandle();

public:
	bool Mount();
	void Unmount();
};