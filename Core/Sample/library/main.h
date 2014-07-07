#pragma once

#include "vf_dev_library.h"

using namespace vapula;

#define EXPORT __declspec(dllexport)

extern "C" 
{
	EXPORT void Process_Math();
	EXPORT void Process_Out();
	EXPORT void Process_Context();
	EXPORT void Process_Context2();
	EXPORT void Rollback_Context2();
	EXPORT void Process_Protect();
	EXPORT void Rollback_Protect();
	EXPORT void Process_AOP();
}