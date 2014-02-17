#pragma once

#include "vf_base.h"

namespace vapula
{
	//basic error
	class VAPULA_API Error
	{
	private:
		int _What;

	public:
		Error(int what);
		~Error();

	public:
		int What();

	public:
		static void Throw(int what);
	};
}