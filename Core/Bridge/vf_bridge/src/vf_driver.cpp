#include "vf_driver.h"
#include "vf_driver_hub.h"

namespace vapula
{
	DriverHub* Driver::_Hub = null;

	DriverHub* Driver::Hub()
	{
		if (_Hub == null)
		{
			Lock* lock = Lock::GetCtorLock();
			lock->Enter();
			if (_Hub == null)
				_Hub = new DriverHub();
			lock->Leave();
		}
		return _Hub;
	}

	Driver* Driver::Find(pcstr id)
	{
		DriverHub* hub = Driver::Hub();
		return hub->Find(id);
	}

	int Driver::Count()
	{
		DriverHub* hub = Driver::Hub();
		return hub->Count();
	}

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

	void Driver::LinkHub()
	{
		DriverHub* hub = Driver::Hub();
		hub->Link(this);
	}

	void Driver::KickHub()
	{
		DriverHub* hub = Driver::Hub();
		hub->Kick(GetRuntimeId());
	}
}