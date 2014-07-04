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
}