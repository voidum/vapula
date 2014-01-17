#pragma once

#include "vf_utility.h"

using namespace vapula;

typedef struct {  
	int slen;
	short stype;
	char *s;
} IDL_STRING;

extern "C" VAPULA_API int vfeEntry_IDL(int argc, object argv[]);