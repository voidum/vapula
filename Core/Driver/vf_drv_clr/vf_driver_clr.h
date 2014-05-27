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
	pcwstr _BridgePath;
	ICLRMetaHost* _MetaHost;
	ICLRRuntimeInfo* _RuntimeInfo;
	ICLRRuntimeHost* _RuntimeHost;
public:
	static DriverCLR* Instance();

public:
	pcstr GetRuntimeId();
	pcstr GetBinExt();
	Library* CreateLibrary();
	Task* CreateTask();

public:
	int CallBridge(pcstr name, pcstr args);
};

extern "C" __declspec(dllexport) Driver* GetDriverInstance();