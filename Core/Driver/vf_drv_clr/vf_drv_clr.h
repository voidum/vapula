#pragma once

#include <metahost.h>

#pragma comment(lib, "mscoree.lib")
#import "mscorlib.tlb" raw_interfaces_only \
	high_property_prefixes("_get","_put","_putref") \
	rename("ReportEvent", "InteropServices_ReportEvent")

#include "vf_clr.h"

class DriverCLR: public Driver
{
public:
	DriverCLR();
	~DriverCLR();
private:
	cstr16 _BridgePath;
	ICLRMetaHost* _MetaHost;
	ICLRRuntimeInfo* _RuntimeInfo;
	ICLRRuntimeHost* _RuntimeHost;
public:
	cstr8 GetRuntimeId();
	Library* CreateLibrary();
	Invoker* CreateInvoker();
public:
	static DriverCLR* GetInstance();
	int CallBridge(cstr16 name, cstr16 arg);
};

extern "C" __declspec(dllexport) Driver* GetDriverInstance();