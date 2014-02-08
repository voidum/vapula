#pragma once

#include "vf_worker.h"

using vapula::Worker;

class Worker_Null : public Worker
{
public:
	Worker_Null();
	~Worker_Null();
public:
	bool RunStageA();
	bool RunStageB();
	bool RunStageC();
};