#pragma once

#include "vf_dev_lib.h"

using namespace vapula;

#define EXPORT __declspec(dllexport)

extern "C" 
{
	EXPORT void Process_Math();
	EXPORT void Process_Out();
	EXPORT void Process_Array();
	EXPORT void Process_Object();
	EXPORT void Process_Context();
	EXPORT void Process_Context2();
	EXPORT void Rollback_Context2();
	EXPORT void Process_Protect();
	EXPORT void Rollback_Protect();
	EXPORT void Process_Msgbox();
}

class ClassA
{
public:
	int MemberA;
	float MemberB;
public:
	void Inc() { MemberA += 50; MemberB += 20; }
	void Dec() { MemberA -= 50; MemberB -= 20; }
};