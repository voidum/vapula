#pragma once

namespace vapula
{
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
		//run stage A
		virtual bool RunStageA() = 0;

		//run stage B
		virtual bool RunStageB() = 0;

		//run stage C
		virtual bool RunStageC() = 0;
	};
}