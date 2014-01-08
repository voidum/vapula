#pragma once

#include "vf_crt.h"

using namespace vapula;

class DriverCRT: public Driver
{
public:
	DriverCRT();
	~DriverCRT();

public:
	cstr8 GetRuntimeId();
	cstr8 GetBinExt();
	Library* CreateLibrary();
	Invoker* CreateInvoker();
	Aspect* CreateAspect();
};

extern "C" __declspec(dllexport) Driver* GetDriverInstance();