#include "stdafx.h"
#include "tcm_task.h"
#include "tcm_library.h"
#include "tcm_executor.h"

namespace tcm
{
	Worker::Worker() { }

	Worker::~Worker() { }

	int Worker::Run(Task* task)
	{
		_Task = task;
		LARGE_INTEGER freq,t1,t2;
		QueryPerformanceFrequency(&freq);
		bool ret_stage = false;
		int i=0;
		for(i=0; i<3; i++)
		{
			QueryPerformanceCounter(&t1);
			switch(i)
			{
				case 0: ret_stage = RunStageA(); break;
				case 1: ret_stage = RunStageB(); break;
				case 2: ret_stage = RunStageC(); break;
			}
			QueryPerformanceCounter(&t2);
			task->SetStageTime(i, (t2.QuadPart-t1.QuadPart)/(float)freq.QuadPart);
			if(!ret_stage) break;
		}
		if(ret_stage) return TCM_TASK_RETURN_NORMAL;
		else return i + 1;
	}

	Task::Task()
	{
		_Lib = NULL;
		_FuncId = -1;
		_StageTime = new float[3];
		for(int i=0; i<3; i++) _StageTime[i] = 0;
	}

	Task::~Task()
	{
		Clear(_Lib);
		Clear(_StageTime, true);
	}

	Task* Task::Parse(PCWSTR path)
	{
		return NULL;
	}

	void Task::SetStageTime(int stage, float time)
	{
		_StageTime[stage] = time;
	}

	float Task::GetStageTime(int stage)
	{
		return _StageTime[stage];
	}

	Library* Task::GetLibrary()
	{
		return _Lib;
	}

	int Task::GetFunctionId()
	{
		return _FuncId;
	}

	Executor* Task::GetExecutor()
	{
		return _Executor;
	}
}