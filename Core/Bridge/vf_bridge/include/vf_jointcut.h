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
		void _Reach(cstr8 method, cstr8 symbol);
	public:
		void Reach(cstr8 method);
		void Reach(cstr8 method, int line);
		void Reach(cstr8 method, cstr8 label);
	};

	//pointcut
	class VAPULA_API Pointcut
	{
	public:
		Pointcut();
		~Pointcut();
	};
}