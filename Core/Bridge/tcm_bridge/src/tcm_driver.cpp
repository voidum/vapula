#include "stdafx.h"
#include "tcm_driver.h"

namespace tcm
{
	Driver::Driver() { }
	Driver::~Driver() { }

	DriverHub* DriverHub::_Instance = new DriverHub();

	DriverHub* DriverHub::GetInstance()
	{
		return DriverHub::_Instance;
	}

	DriverHub::DriverHub() { }
	DriverHub::~DriverHub()
	{
		KickAll();
	}

	int DriverHub::GetIndex(PCSTR id)
	{
		typedef vector<Driver*>::iterator iter;
		int x = 0;
		for(iter i=_Drivers.begin(); i!=_Drivers.end(); i++)
		{
			int same = strcmp(id, (*i)->GetRuntimeId());
			if(same == 0) return x;
			x++;
		}
		return -1;
	}

	Driver* DriverHub::GetDriver(PCSTR id)
	{
		int index = GetIndex(id);
		if(index < 0) return NULL;
		Driver* driver = _Drivers[index];
		return driver;
	}

	int DriverHub::GetCount()
	{
		return _Drivers.size();
	}

	bool DriverHub::Link(PCSTR id)
	{
		int index = GetIndex(id);
		if(index >= 0) return false;
		PCWSTR id_w = MbToWc(id);
		std::wstring strw = id_w;
		strw += L".tcm.driver";
		HMODULE module = LoadLibraryW(strw.c_str());
		delete id_w;
		if(module == NULL) return false;
		typedef Driver* (*Delegate)();
		Delegate d = (Delegate)GetProcAddress(module, "GetDriverInstance");
		Driver* driver = d();
		_Modules.push_back(module);
		_Drivers.push_back(driver);
		return true;
	}

	bool DriverHub::Kick(PCSTR id)
	{
		int index = GetIndex(id);
		if(index < 0) return false;
		Driver* driver = _Drivers[index];
		Clear(driver);
		_Drivers.erase(_Drivers.begin() + index);
		
		HMODULE module = _Modules[index];
		FreeLibrary(module);
		_Modules.erase(_Modules.begin() + index);
		return true;
	}

	void DriverHub::KickAll()
	{
		typedef vector<Driver*>::iterator iter1;
		typedef vector<HMODULE>::iterator iter2;
		for(iter1 i=_Drivers.begin(); i!=_Drivers.end(); i++)
			delete *i;
		_Drivers.clear();
		for(iter2 i=_Modules.begin(); i!=_Modules.end(); i++)
			FreeLibrary(*i);
		_Modules.clear();
	}
}