#pragma once

namespace vapula
{
	#define STAGE_MAX 4

	enum WorkerStage
	{
		VFH_WORKER_NORMAL = 0,
		VFH_WORKER_PREPARE = 1,
		VFH_WORKER_PROCESS = 2,
		VFH_WORKER_FINISH = 3
	};

	class Task;
	class Invoker;
	class Stack;

	//worker {base}
	class Worker
	{
	protected:
		Task* _Task;
		float* _StageTime;
		Invoker* _Invoker;

	public:
		Worker();
		~Worker();

	public:
		//get task
		Task* GetTask();

		//get stack
		Stack* GetStack();

		//set stage elapsed time (s)
		void SetStageTime(int stage, float time);

		//get stage elapsed time (s)
		float GetStageTime(int stage);

		//run task
		int Run(Task* task);

	protected:
		//stage: prepare
		virtual bool OnPrepare();

		//stage: process
		virtual bool OnProcess() = 0;

		//stage: finish
		virtual bool OnFinish();
	};
}