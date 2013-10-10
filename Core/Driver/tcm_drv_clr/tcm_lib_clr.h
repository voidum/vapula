#pragma once

#include "tcm_library.h"
#include <metahost.h>

using namespace tcm;
using std::wstring;

class LibraryCLR : public Library
{
public:
	LibraryCLR();
	~LibraryCLR();
private:
	PCWSTR _Handle;
public:
	PCWSTR GetHandle();
public:
	PCSTR GetRuntimeId();

	PCWSTR GetBinExt();

	bool Mount();

	void Unmount();
};