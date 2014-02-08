#pragma once

#include "vf_worker.h"
#include "vf_pipe.h"

using vapula::Pipe;
using vapula::Worker;

class Worker_Pipe : public Worker
{
public:
	Worker_Pipe();
	~Worker_Pipe();
private:
	Pipe* _Pipe;
public:
	bool RunStageA();
	bool RunStageB();
	bool RunStageC();
};