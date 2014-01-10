#pragma once

#include "vf_dev_lib.h"

using namespace vapula;

extern "C" __declspec(dllexport) void Run();

void Function_Math();
void Function_Out();
void Function_TestArray();
void Function_TestObject();
void Function_TestContext();

class TestClassA
{
public:
	int MemberA;
	float MemberB;
public:
	void Inc(){MemberA += 50;MemberB += 20;}
	void Dec(){MemberA -= 50;MemberB -= 20;}
};