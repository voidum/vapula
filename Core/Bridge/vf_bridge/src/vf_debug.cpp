#include "vf_debug.h"

namespace vapula
{
	DbgMemleak::DbgMemleak()
	{
		_S1 = new _CrtMemState();
		_S2 = new _CrtMemState();
		_Diff = new _CrtMemState();
	}

	DbgMemleak::~DbgMemleak()
	{
		delete _S1;
		delete _S2;
		delete _Diff;
	}

	void DbgMemleak::Begin()
	{
		_CrtMemCheckpoint(_S1);
	}

	void DbgMemleak::End()
	{
		_CrtMemCheckpoint(_S2);
		if(_CrtMemDifference(_Diff, _S1, _S2))
			_CrtMemDumpStatistics(_Diff);
		else
			std::cout<<"no mem leak"<<std::endl;
	}

	DbgTimer::DbgTimer()
	{
		_Freq = new LARGE_INTEGER();
		_T1 = new LARGE_INTEGER();
		_T2 = new LARGE_INTEGER();
	}

	DbgTimer::~DbgTimer()
	{
		delete _Freq;
		delete _T1;
		delete _T2;
	}

	void DbgTimer::Begin()
	{
		QueryPerformanceFrequency(_Freq);
		QueryPerformanceCounter(_T1);
	}

	void DbgTimer::End()
	{
		QueryPerformanceCounter(_T2);
		double ms = (_T2->QuadPart - _T1->QuadPart) * 1000.0 / _Freq->QuadPart;
		std::cout<<"dbg-timer:"<<ms<<std::endl;
	}
}