#include "vf_driver_clr.h"
#include "vf_library_clr.h"
#include "vf_task_clr.h"

using namespace vapula;

DriverCLR::DriverCLR()
{
	Runtime* runtime = Runtime::Instance();
	pcstr cs8_dir = runtime->GetRuntimeDir();
	string str1 = cs8_dir;
	str1 += "clr.driver.dll";
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
	LibraryCLR* library = new LibraryCLR();
	return library;
}

Task* DriverCLR::CreateTask()
{
	TaskCLR* task = new TaskCLR();
	return task;
}

DriverCLR* DriverCLR::Instance()
{
	DriverCLR* driver = 
		(DriverCLR*)Driver::Find(_vf_runtime_id);
	return driver;
}

Driver* GetDriverInstance()
{
	DriverCLR* driver = new DriverCLR();
	return driver;
}

int DriverCLR::CallBridge(pcstr cmd, pcstr args)
{
	DWORD ret = 0;
	pcwstr cs16_cmd = str::ToStrW(cmd);
	pcwstr cs16_args = str::ToStrW(args);
	HRESULT hr = _RuntimeHost->ExecuteInDefaultAppDomain(
		_BridgePath,
		L"Vapula.Driver.Entry",
		cs16_cmd, cs16_args, &ret);
	if(hr != S_OK)
	{
		ostringstream oss;
		oss << "error occured at [CallBridge]\n";
		oss << "command:" << cmd << "\n";
		oss << "arguments:" << args << "\n";
		oss << "code:" << hr;
		ShowMsgbox(oss.str().c_str(), _vf_clr);
	}
	delete cs16_cmd;
	delete cs16_args;
	return ret;
}