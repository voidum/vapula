#pragma once

#include "vf_worker.h"

using vapula::Worker;

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