#pragma once

#include "vf_dev_lib.h"

using namespace vapula;

extern "C" __declspec(dllexport)
int Run(int function, Envelope* envelope, Context* context);

int Function_Math(Envelope* envelope,Context* context);
int Function_Out(Envelope* envelope,Context* context);
int Function_TestArray(Envelope* envelope,Context* context);
int Function_TestObject(Envelope* envelope,Context* context);
int Function_TestContext(Envelope* envelope,Context* context);

class TestClassA
{
public:
	int MemberA;
	float MemberB;
public:
	void Inc(){MemberA += 50;MemberB += 20;}
	void Dec(){MemberA -= 50;MemberB -= 20;}
};