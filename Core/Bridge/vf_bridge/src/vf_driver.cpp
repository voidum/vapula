#include "vf_driver.h"
#include "windows.h"

namespace vapula
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

	int DriverHub::GetIndex(cstr id)
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

	Driver* DriverHub::GetDriver(cstr id)
	{
		int index = GetIndex(id);
		if(index < 0)
			return null;
		Driver* driver = _Drivers[index];
		return driver;
	}

	int DriverHub::GetCount()
	{
		return _Drivers.size();
	}

	bool DriverHub::Link(cstr id)
	{
		int index = GetIndex(id);
		if(index >= 0) return true;

		cstrw id_w = MbToWc(id);
		std::wstring path = id_w;
		path += L".vf.driver";
		HMODULE module = LoadLibraryW(path.c_str());
		delete id_w;

		if(module == null) return false;
		typedef Driver* (*Delegate)();
		Delegate d = (Delegate)GetProcAddress(module, "GetDriverInstance");
		Driver* driver = d();
		_Modules.push_back(module);
		_Drivers.push_back(driver);
		return true;
	}

	bool DriverHub::Kick(cstr id)
	{
		int index = GetIndex(id);
		if(index < 0)
			return true;
		Driver* driver = _Drivers[index];
		Clear(driver);
		_Drivers.erase(_Drivers.begin() + index);
		
		HMODULE module = (HMODULE)_Modules[index];
		FreeLibrary(module);
		_Modules.erase(_Modules.begin() + index);
		return true;
	}

	void DriverHub::KickAll()
	{
		typedef vector<Driver*>::iterator iter1;
		typedef vector<object>::iterator iter2;
		for(iter1 i=_Drivers.begin(); i!=_Drivers.end(); i++)
			delete *i;
		_Drivers.clear();
		for(iter2 i=_Modules.begin(); i!=_Modules.end(); i++)
			FreeLibrary((HMODULE)*i);
		_Modules.clear();
	}
}