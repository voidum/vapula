#pragma once

#include "tcm_candy.h"

namespace tcm
{
	//工作者任务返回值
	enum TaskReturnCode
	{
		TCM_TASK_RETURN_NORMAL = 0,
		TCM_TASK_RETURN_ERROR_A = 1,
		TCM_TASK_RETURN_ERROR_B = 2,
		TCM_TASK_RETURN_ERROR_C = 3
	};

	class Library;
	class Invoker;
	class Worker;

	//任务基类
	class TCM_BRIDGE_API Task
	{
	protected:
		Task();
	public:
		~Task();
	public:
		//从指定XML文件解析任务
		static Task* Parse(strw path);
	protected:
		Library* _Lib;
		int _FuncId;
		Invoker* _Invoker;
	protected	:
		float* _StageTime;
	public:
		//设置阶段耗时，以秒为单位
		void SetStageTime(int stage, float time);

		//获取阶段耗时，以秒为单位
		float GetStageTime(int stage);

		//获取任务的指定功能库
		Library* GetLibrary();

		//获取任务的指定功能
		int GetFunctionId();

		//获取调用器
		Invoker* GetInvoker();
	public:
		//指定工作者运行任务
		virtual bool RunAs(Worker* worker) = 0;
	};

	//工作者基类
	class TCM_BRIDGE_API Worker
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