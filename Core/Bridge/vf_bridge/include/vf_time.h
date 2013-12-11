#pragma once

#include "vf_candy.h"

namespace vf
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