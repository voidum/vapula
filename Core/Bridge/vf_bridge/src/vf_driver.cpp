#include "vf_driver.h"
#include "vf_driver_dpt.h"

namespace vapula
{
	Driver::Driver() { }
	Driver::~Driver() { }

	DriverHub* DriverHub::_Instance = null;

	DriverHub* DriverHub::GetInstance()
	{
		Lock* lock = Lock::GetCtorLock();
		if(lock->Enter())
		{
			if(DriverHub::_Instance == null)
				DriverHub::_Instance = new DriverHub();
			lock->Leave();
		}
		return DriverHub::_Instance;
	}

	DriverHub::DriverHub() { }
	DriverHub::~DriverHub()
	{
		KickAll();
	}

	DriverDpt* DriverHub::GetDriverDpt(cstr8 id)
	{
		typedef vector<DriverDpt*>::iterator iter;
		for(iter i = _DriverDpts.begin(); i != _DriverDpts.end(); i++)
		{
			DriverDpt* dpt = *i;
			Driver* driver = dpt->_Driver;
			if(strcmp(driver->GetRuntimeId(), id) == 0)
				return dpt;
		}
		return null;
	}

	Driver* DriverHub::GetDriver(cstr8 id)
	{
		DriverDpt* dpt = GetDriverDpt(id);
		if(dpt == null)
			return null;
		else
			return dpt->_Driver;
	}

	int DriverHub::GetCount()
	{
		return _DriverDpts.size();
	}

	bool DriverHub::Link(cstr8 id)
	{
		DriverDpt* dpt = GetDriverDpt(id);
		if(dpt != null)
			return true;

		astr8 dir(GetRuntimeDir());
		ostringstream oss;
		oss<<dir.get()<<id<<".driver";
		astr16 path16(str::ToCh16(oss.str().c_str()));
		HMODULE module = LoadLibraryW(path16.get());

		if(module == null) 
			return false;

		typedef Driver* (*Delegate)();
		Delegate d = (Delegate)GetProcAddress(module, "GetDriverInstance");
		if(d == null)
		{
			FreeLibrary(module);
			return false;
		}

		Driver* driver = d();
		dpt = new DriverDpt();
		dpt->_Driver = driver;
		dpt->_Handle = module;
		_DriverDpts.push_back(dpt);
		return true;
	}

	void DriverHub::Kick(cstr8 id)
	{
		typedef vector<DriverDpt*>::iterator iter;
		for(iter i = _DriverDpts.begin(); i != _DriverDpts.end(); i++)
		{
			DriverDpt* dpt = *i;
			Driver* driver = dpt->_Driver;
			if(strcmp(driver->GetRuntimeId(), id) == 0)
			{
				_DriverDpts.erase(i);
				Clear(dpt);
				break;
			}
		}
	}

	void DriverHub::KickAll()
	{
		typedef vector<DriverDpt*>::iterator iter;
		for(iter i = _DriverDpts.begin(); i != _DriverDpts.end(); i++)
			Clear(*i);
		_DriverDpts.clear();
	}
}