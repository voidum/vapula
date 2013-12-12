#include "vf_worker.h"
#include "vf_task.h"

namespace vapula
{
	Worker::Worker() { }

	Worker::~Worker() { }

	Task* Worker::GetTask()
	{
		return _Task;
	}

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
		if(ret_stage) 
			return VF_HOST_RETURN_NORMAL;
		else return i + 1;
	}
}