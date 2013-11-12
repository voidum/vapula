#pragma once

#include <metahost.h>

#pragma comment(lib, "mscoree.lib")
#import "mscorlib.tlb" raw_interfaces_only \
	high_property_prefixes("_get","_put","_putref") \
	rename("ReportEvent", "InteropServices_ReportEvent")

#include "tcm_driver.h"

class DriverCLR: public tcm::Driver
{
public:
	DriverCLR();
	~DriverCLR();
private:
	tcm::strw _BridgePath;
	tcm::strw _RuntimeVersion;
	ICLRMetaHost* _MetaHost;
	ICLRRuntimeInfo* _RuntimeInfo;
	ICLRRuntimeHost* _RuntimeHost;
private:
	tcm::strw GetCLRVersion();
public:
	tcm::str GetRuntimeId();

	tcm::Library*
		CreateLibrary();

	tcm::Invoker*
		CreateInvoker();
public:
	static DriverCLR* GetInstance();

	int CallBridge(tcm::strw name, tcm::strw arg);
};

extern "C" __declspec(dllexport) tcm::Driver* GetDriverInstance();