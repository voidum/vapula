#pragma once

#include "vf_worker.h"
#include "vf_pipe.h"

using vapula::Pipe;
using vapula::Worker;

class WorkerPipe : public Worker
{
private:
	Pipe* _Pipe;

public:
	WorkerPipe();
	~WorkerPipe();

public:
	bool OnPrepare();
	bool OnProcess();
	bool OnFinish();
};