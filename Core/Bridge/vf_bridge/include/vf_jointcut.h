#pragma once

#include "vf_utility.h"

namespace vapula
{
	//joint point in target code
	class VAPULA_API Joint
	{
	public:
		Joint();
		~Joint();
	private:
		void _Reach(cstr8 function, cstr8 symbol);
	public:
		void Reach(cstr8 function, cstr8 data = null);
		void Reach(cstr8 function, int line, cstr8 data = null);
		void Reach(cstr8 function, cstr8 label, cstr8 data = null);
	};

	//pointcut
	class VAPULA_API Pointcut
	{
	public:
		Pointcut();
		~Pointcut();
	};
}