#pragma once

namespace vapula
{
	enum TaskStage
	{
		VF_STAGE_PREPARE = 0,
		VF_STAGE_PROCESS = 1,
		VF_STAGE_FINISH = 2,
		VF_STAGE_ROLLBACK = 3
	};

	class Task;

	//worker {base}
	class Worker
	{
	public:
		Worker();
		~Worker();
	protected:
		Task* _Task;
	public:
		//run task
		int Run(Task* task);

		//get task
		Task* GetTask();
	protected:
		//stage: prepare
		virtual bool OnPrepare() = 0;

		//stage: process
		virtual bool OnProcess() = 0;

		//stage: finish
		virtual bool OnFinish() = 0;

		//stage: rollback
		virtual bool OnRollback() = 0;
	};
}