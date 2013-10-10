#pragma once

#include "tcm_cdev.h"

using namespace tcm;

extern "C" __declspec(dllexport)
UINT Run(int function, Envelope* envelope, Context* context);