#include "vf_drv_clr.h"
#include "vf_lib_clr.h"
#include "vf_inv_clr.h"

using namespace vapula;

DriverCLR::DriverCLR()
{
	cstr8 dir = GetRuntimeDir();
	string s = dir;
	s += "vf_bridge_x.dll";
	_BridgePath = str::ToCh16(s.c_str());
	delete dir;

	HRESULT hr;
	_MetaHost = NULL;
	_RuntimeInfo = NULL;
	_RuntimeHost = NULL;
	hr = CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, (LPVOID*)&_MetaHost);

	DWORD ver_len = 128;
	WCHAR version[128];
	hr = _MetaHost->GetVersionFromFile(_BridgePath, version, &ver_len);
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

cstr8 DriverCLR::GetRuntimeId() 
{
	return _vf_runtime_id;
}

Library* DriverCLR::CreateLibrary()
{
	LibraryCLR* lib = new LibraryCLR();
	return lib;
}

Invoker* DriverCLR::CreateInvoker()
{
	InvokerCLR* inv = new InvokerCLR();
	return inv;
}

DriverCLR* DriverCLR::GetInstance()
{
	DriverHub* drv_hub = DriverHub::GetInstance();
	DriverCLR* drv = 
		dynamic_cast<DriverCLR*>(drv_hub->GetDriver(_vf_runtime_id));
	return drv;
}

Driver* GetDriverInstance()
{
	DriverCLR* driver = new DriverCLR();
	return driver;
}

int DriverCLR::CallBridge(cstr16 name, cstr16 arg)
{
	DWORD ret = 0;
	HRESULT hr = _RuntimeHost->ExecuteInDefaultAppDomain(
		_BridgePath, 
		L"Vapula.Runtime.DriverEntry",
		name, arg, &ret);
	if(hr != S_OK)
	{
		if(Config::GetInstance()->IsSilent())
		{
			ostringstream oss;
			oss<<"error occured at [CallBridge]\n";
			oss<<"code:"<<hr;
			ShowMsgbox(oss.str().c_str(), _vf_clr);
		}
	}
	return ret;
}