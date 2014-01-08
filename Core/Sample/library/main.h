#pragma once

#include "vf_dev_lib.h"

using namespace vapula;

extern "C" __declspec(dllexport) int Run();

int Function_Math();
int Function_Out();
int Function_TestArray();
int Function_TestObject();
int Function_TestContext();

class TestClassA
{
public:
	int MemberA;
	float MemberB;
public:
	void Inc(){MemberA += 50;MemberB += 20;}
	void Dec(){MemberA -= 50;MemberB -= 20;}
};