#pragma once

#include "vf_host.h"

namespace vapula
{
	class Worker;

	//任务
	class Task
	{
	private:
		Task();
	public:
		~Task();

	private:
		Library* _Lib;
		int _FuncId;
		Invoker* _Invoker;
		float* _StageTime;

	private:
		Dict* _Tags;
		int _CtrlMode;
		cstr8 _CtrlConfig;

	public:
		//从指定XML文件解析任务
		static Task* Parse(cstr8 path);

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
		//获取扩展标签
		Dict* GetTags();

		//获取控制模式
		int GetCtrlMode();

		//获取控制配置
		cstr8 GetCtrlConfig();

	public:
		//指定工作者运行任务
		bool RunAs(Worker* worker);
	};

}