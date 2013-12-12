#pragma once

namespace vapula
{
	class Task;

	//工作者基类
	class Worker
	{
	public:
		Worker();
		~Worker();
	protected:
		Task* _Task;
	public:
		//运行任务
		int Run(Task* task);

		//获取任务
		Task* GetTask();
	protected:
		//运行任务阶段A
		virtual bool RunStageA() = 0;

		//运行任务阶段B
		virtual bool RunStageB() = 0;

		//运行任务阶段C
		virtual bool RunStageC() = 0;
	};
}