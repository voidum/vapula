#pragma once

#include "vf_worker.h"
#include "vf_pipe.h"

using vapula::Pipe;
using vapula::Worker;

class Worker_PIPE : public Worker
{
public:
	Worker_PIPE();
	~Worker_PIPE();
private:
	Pipe* _Pipe;
public:
	bool RunStageA();
	bool RunStageB();
	bool RunStageC();
};