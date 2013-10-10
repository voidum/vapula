#pragma once

#include "tcm_library.h"

using namespace tcm;

class LibraryCRT : public Library
{
public:
	LibraryCRT();
	~LibraryCRT();
private:
	HMODULE _Module;
	LPVOID _Entry;
public:
	PCSTR GetRuntimeId();

	PCWSTR GetBinExt();

	bool Mount();

	void Unmount();
public:
	LPVOID GetEntry();
};