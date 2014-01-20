#pragma once

#include "vf_dev_lib.h"

using namespace vapula;

#define EXPORT __declspec(dllexport)

extern "C" 
{
	EXPORT void Function_Math();
	EXPORT void Function_Out();
	EXPORT void Function_TestArray();
	EXPORT void Function_TestObject();
	EXPORT void Function_TestContext();
}

class TestClassA
{
public:
	int MemberA;
	float MemberB;
public:
	void Inc(){MemberA += 50;MemberB += 20;}
	void Dec(){MemberA -= 50;MemberB -= 20;}
};