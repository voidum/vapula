#pragma once

#include "tcm_driver.h"

using namespace tcm;

class DriverCRT: public Driver
{
public:
	DriverCRT();
	~DriverCRT();
public:
	PCSTR GetRuntimeId();

	Library*
		CreateLibrary();

	Invoker*
		CreateInvoker();
};

extern "C" __declspec(dllexport) Driver* GetDriverInstance();