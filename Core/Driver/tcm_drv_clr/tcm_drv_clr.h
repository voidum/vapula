#pragma once

#include "tcm_driver.h"
#include <metahost.h>

#pragma comment(lib, "mscoree.lib")
#import "mscorlib.tlb" raw_interfaces_only \
	high_property_prefixes("_get","_put","_putref") \
	rename("ReportEvent", "InteropServices_ReportEvent")

using namespace tcm;

class DriverCLR: public Driver
{
public:
	DriverCLR();
	~DriverCLR();
private:
	PCWSTR _BridgePath;
	PCWSTR _RuntimeVersion;
	ICLRMetaHost* _MetaHost;
	ICLRRuntimeInfo* _RuntimeInfo;
	ICLRRuntimeHost* _RuntimeHost;
private:
	PCWSTR GetCLRVersion();
public:
	PCSTR GetRuntimeId();

	Library*
		CreateLibrary();

	Invoker*
		CreateInvoker();
public:
	static DriverCLR* GetInstance();

	int CallBridge(PCWSTR name, PCWSTR arg);
};

extern "C" __declspec(dllexport) Driver* GetDriverInstance();