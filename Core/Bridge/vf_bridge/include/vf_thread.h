#pragma once

#include "vf_base.h"

namespace vapula
{
	class Thread
	{
	public:
		Thread();
		~Thread();

	private:
		HANDLE _Handle;
		bool _IsLoop;
		bool _IsBusy;

	public:
		void Suspend();

	};
}