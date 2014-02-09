#include "vf_driver.h"

namespace vapula
{
	Driver::Driver() { }
	Driver::~Driver() { }

	DriverHub* DriverHub::_Instance = null;

	DriverHub* DriverHub::GetInstance()
	{
		Lock* lock = Lock::GetCtorLock();
		lock->Enter();
		if(DriverHub::_Instance == null)
			DriverHub::_Instance = new DriverHub();
		lock->Leave();
		return DriverHub::_Instance;
	}

	DriverHub::DriverHub() { }
	DriverHub::~DriverHub()
	{
		KickAll();
	}

	Driver* DriverHub::GetDriver(pcstr id)
	{
		typedef vector<Driver*>::iterator iter;
		for(iter i = _Drivers.begin(); i != _Drivers.end(); i++)
		{
			Driver* drv = *i;
			if(strcmp(drv->GetRuntimeId(), id) == 0)
				return drv;
		}
		return null;
	}

	int DriverHub::GetCount()
	{
		return _Drivers.size();
	}

	bool DriverHub::Link(pcstr id)
	{
		Driver* drv = GetDriver(id);
		if(drv != null)
			return true;

		ostringstream oss;
		pcstr cs8_dir = GetRuntimeDir();
		oss<<cs8_dir<<id<<".driver";
		pcwstr cs16_path = str::ToStrW(oss.str().c_str());
		delete cs8_dir;
		Handle autop1((object)cs16_path);
		HMODULE module = LoadLibraryW(cs16_path);

		if(module == null) 
			return false;

		typedef Driver* (*Delegate)();
		Delegate d = (Delegate)GetProcAddress(module, "GetDriverInstance");
		if(d == null)
		{
			FreeLibrary(module);
			return false;
		}

		drv = d();
		drv->_Module = module;
		_Drivers.push_back(drv);
		return true;
	}

	void DriverHub::Kick(pcstr id)
	{
		typedef vector<Driver*>::iterator iter;
		for(iter i = _Drivers.begin(); i != _Drivers.end(); i++)
		{
			Driver* drv = *i;
			if(strcmp(drv->GetRuntimeId(), id) == 0)
			{
				_Drivers.erase(i);
				Clear(drv);
				break;
			}
		}
	}

	void DriverHub::KickAll()
	{
		typedef vector<Driver*>::iterator iter;
		for(iter i = _Drivers.begin(); i != _Drivers.end(); i++)
			Clear(*i);
		_Drivers.clear();
	}
}