#pragma once

#include "task.h"

using namespace tcm;

class Worker_NULL : public Worker
{
public:
	Worker_NULL();
	~Worker_NULL();
public:
	bool RunStageA();
	bool RunStageB();
	bool RunStageC();
};