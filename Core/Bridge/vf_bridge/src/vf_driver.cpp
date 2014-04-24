#include "vf_driver.h"

namespace vapula
{
	Driver::Driver() { }

	Driver::~Driver() { }

	Driver* Driver::Load(pcstr path)
	{
		pcwstr cs16_path = str::ToStrW(path);
		Scoped autop1((raw)cs16_path);
		HMODULE module = LoadLibraryW(cs16_path);
		if(module == null) 
			return null;

		typedef Driver* (*Delegate)();
		Delegate d = (Delegate)GetProcAddress(module, "GetDriverInstance");
		if(d == null)
		{
			FreeLibrary(module);
			return null;
		}

		Driver* drv = d();
		drv->_Module = module;
		return drv;
	}


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
		typedef list<Driver*>::iterator iter;
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

	void DriverHub::Link(Driver* driver)
	{
		Driver* drv = GetDriver(driver->GetRuntimeId());
		if(drv == null)
			_Drivers.push_back(driver);
	}

	bool DriverHub::Link(pcstr id)
	{
		Driver* drv = GetDriver(id);
		if(drv != null)
			return true;
		ostringstream oss;
		pcstr cs8_dir = GetRuntimeDir();
		oss<<cs8_dir<<id<<".driver";
		pcstr cs8_path = str::Copy(oss.str().c_str());
		Scoped autop((raw)cs8_path);
		drv = Driver::Load(cs8_path);
		if(drv == null)
			return false;
		_Drivers.push_back(drv);
		return true;
	}

	void DriverHub::Kick(pcstr id)
	{
		typedef list<Driver*>::iterator iter;
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
		typedef list<Driver*>::iterator iter;
		for(iter i = _Drivers.begin(); i != _Drivers.end(); i++)
			Clear(*i);
		_Drivers.clear();
	}
}