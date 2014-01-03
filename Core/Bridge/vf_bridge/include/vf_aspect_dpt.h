#pragma once

#include "vf_base.h"

namespace vapula
{
	class Aspect;

	//aspect descriptor
	class AspectDpt
	{
	public:
		AspectDpt();
		~AspectDpt();
	public:
		HMODULE _Handle;
		Aspect* _Aspect;
	};
}