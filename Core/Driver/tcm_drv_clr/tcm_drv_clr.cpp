#include "stdafx.h"
#include "tcm_drv_clr.h"
#include "tcm_lib_clr.h"
#include "tcm_exec_clr.h"

using namespace tcm;
using std::wstring;

DriverCLR::DriverCLR()
{
	PCWSTR dir = GetRuntimeDir();
	wstring str_dir = dir;
	str_dir += L"tcm_xbridge.dll";
	_BridgePath = CopyStrW(str_dir.c_str());
	delete dir;

	HRESULT hr;
	_MetaHost = NULL;
	_RuntimeInfo = NULL;
	_RuntimeHost = NULL;
	hr = CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, (LPVOID*)&_MetaHost);

	DWORD ver_len = 128;
	WCHAR version[128];
	hr = _MetaHost->GetVersionFromFile(_BridgePath, version, &ver_len);
	_RuntimeVersion = CopyStrW(version);

	hr = _MetaHost->GetRuntime(version, IID_PPV_ARGS(&_RuntimeInfo));
	hr = _RuntimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_PPV_ARGS(&_RuntimeHost));
	hr = _RuntimeHost->Start();
}

DriverCLR::~DriverCLR() 
{
	HRESULT hr;
	hr = _RuntimeHost->Stop();
	_RuntimeHost->Release();
	_RuntimeInfo->Release();
	_MetaHost->Release();
}

PCSTR DriverCLR::GetRuntimeId() 
{
	return RUNTIME_ID;
}

Library* DriverCLR::CreateLibrary()
{
	LibraryCLR* lib = new LibraryCLR();
	return lib;
}

Executor* DriverCLR::CreateExecutor()
{
	ExecutorCLR* exec = new ExecutorCLR();
	return exec;
}

DriverCLR* DriverCLR::GetInstance()
{
	DriverHub* drv_hub = DriverHub::GetInstance();
	DriverCLR* drv = 
		dynamic_cast<DriverCLR*>(drv_hub->GetDriver(RUNTIME_ID));
	return drv;
}

Driver* GetDriverInstance()
{
	DriverCLR* driver = new DriverCLR();
	return driver;
}

int DriverCLR::CallBridge(PCWSTR name, PCWSTR arg)
{
	DWORD ret = 0;
	HRESULT hr = _RuntimeHost->ExecuteInDefaultAppDomain(
		_BridgePath, 
		L"TCM.Runtime.DriverEntry",
		name, arg, &ret);
	return ret;
}