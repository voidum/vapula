#include "vf_driver_dpt.h"
#include "vf_driver.h"

namespace vapula
{
	DriverDpt::DriverDpt()
	{
		_Handle = null;
		_Driver = null;
	}

	DriverDpt::~DriverDpt()
	{
		delete _Driver;
		FreeLibrary(_Handle);
	}
}