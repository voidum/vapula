#include "vf_drv_clr.h"
#include "vf_lib_clr.h"
#include "vf_inv_clr.h"

using namespace vapula;

DriverCLR::DriverCLR()
{
	pcstr cs8_dir = GetRuntimeDir();
	string str1 = cs8_dir;
	str1 += "vf_bridge_x.dll";
	_BridgePath = str::ToStrW(str1.c_str());
	delete cs8_dir;

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

pcstr DriverCLR::GetRuntimeId() 
{
	return _vf_runtime_id;
}

pcstr DriverCLR::GetBinExt()
{
	return "dll";
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

int DriverCLR::CallBridge(pcwstr name, pcwstr arg)
{
	DWORD ret = 0;
	HRESULT hr = _RuntimeHost->ExecuteInDefaultAppDomain(
		_BridgePath, 
		L"Vapula.Runtime.DriverEntry",
		name, arg, &ret);
	if(hr != S_OK)
	{
		if(Setting::GetInstance()->IsSilent())
		{
			ostringstream oss;
			oss<<"error occured at [CallBridge]\n";
			oss<<"code:"<<hr;
			ShowMsgbox(oss.str().c_str(), _vf_clr);
		}
	}
	return ret;
}