#pragma once

#include "task.h"
#include "tcm_pipe.h"

using namespace tcm;

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