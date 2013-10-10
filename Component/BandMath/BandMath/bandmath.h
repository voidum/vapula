#pragma once

#include "tcm_cdev.h"
#include "datavar.h"

using namespace tcm;

void ExecBandMath(PCSTR expr,DataDef* datadef,Context* ctx);