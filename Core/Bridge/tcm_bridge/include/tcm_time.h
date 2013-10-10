#pragma once

#include "tcm_candy.h"

namespace tcm
{
	//¼ÆÊ±Æ÷
	class TCM_BRIDGE_API Stopwatch : Uncopiable
	{
	public:
		Stopwatch();
		~Stopwatch();
	private:
		LARGE_INTEGER _F, _T1, _T2;
	public:
		void Start();
		void Stop();
		double GetElapsedTime();
	};
}