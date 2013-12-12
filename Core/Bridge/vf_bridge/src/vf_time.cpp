#include "vf_time.h"

namespace vapula
{
	Stopwatch::Stopwatch() { }

	Stopwatch::~Stopwatch() { }

	void Stopwatch::Start()
	{
		QueryPerformanceFrequency(&_F);
		QueryPerformanceCounter(&_T1);
	}

	void Stopwatch::Stop()
	{
		QueryPerformanceCounter(&_T2);
	}

	double Stopwatch::GetElapsedTime()
	{
		double time = 
			(_T2.QuadPart - _T1.QuadPart) * 1000.0 / _F.QuadPart;
		return time;
	}
}