#include "vf_driver_hub.h"
#include "vf_driver.h"

namespace vapula
{
	DriverHub::DriverHub()
	{
		_Lock = new Lock();
	}

	DriverHub::~DriverHub()
	{
		RemoveAll();
		Clear(_Lock);
	}

	list<Driver*>& DriverHub::GetInnerData()
	{
		return _Drivers;
	}

	int DriverHub::Count()
	{
		return _Drivers.size();
	}

	Driver* DriverHub::Find(pcstr id)
	{
		typedef list<Driver*>::iterator iter;
		_Lock->Enter();
		Driver* driver = null;
		for (iter i = _Drivers.begin(); i != _Drivers.end(); i++)
		{
			Driver* tmp = *i;
			if (strcmp(tmp->GetRuntimeId(), id) == 0)
			{
				driver = tmp;
				break;
			}
		}
		_Lock->Leave();
		return driver;
	}

	void DriverHub::Add(Driver* driver)
	{
		typedef list<Driver*>::iterator iter;
		_Lock->Enter();
		pcstr id = driver->GetRuntimeId();
		for (iter i = _Drivers.begin(); i != _Drivers.end(); i++)
		{
			Driver* tmp = *i;
			if (strcmp(tmp->GetRuntimeId(), id) == 0)
			{
				_Lock->Leave();
				return;
			}
		}
		_Drivers.push_back(driver);
		_Lock->Leave();
	}

	void DriverHub::Remove(Driver* driver)
	{
		typedef list<Driver*>::iterator iter;
		_Lock->Enter();
		for (iter i = _Drivers.begin(); i != _Drivers.end(); i++)
		{
			Driver* tmp = *i;
			if (driver == tmp)
			{
				Clear(tmp);
				_Drivers.erase(i);
				break;
			}
		}
		_Lock->Leave();
	}

	void DriverHub::RemoveAll()
	{
		typedef list<Driver*>::iterator iter;
		_Lock->Enter();
		for (iter i = _Drivers.begin(); i != _Drivers.end(); i++)
			Clear(*i);
		_Drivers.clear();
		_Lock->Leave();
	}
}