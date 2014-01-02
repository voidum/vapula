#pragma once

#include "vf_base.h"

namespace vapula
{
	class Driver;

	//driver descriptor
	class DriverDpt
	{
	public:
		DriverDpt();
		~DriverDpt();
	public:
		HMODULE _Handle;
		Driver* _Driver;
	};
}