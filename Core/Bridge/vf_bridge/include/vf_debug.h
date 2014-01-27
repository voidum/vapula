#pragma once

#include "vf_const.h"

#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <crtdbg.h>

namespace vapula
{
	class VAPULA_API DbgMemleak
	{
	public:
		DbgMemleak();
		~DbgMemleak();
	private:
		_CrtMemState* _S1;
		_CrtMemState* _S2;
		_CrtMemState* _Diff;
	public:
		void Begin();
		void End();
	};

	class VAPULA_API DbgTimer
	{
	public:
		DbgTimer();
		~DbgTimer();
	private:
		LARGE_INTEGER* _Freq;
		LARGE_INTEGER* _T1;
		LARGE_INTEGER* _T2;
	public:
		void Begin();
		void End();
	};
}