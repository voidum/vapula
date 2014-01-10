#pragma once

#include "vf_host.h"

namespace vapula
{
	class Worker;

	//task
	class Task
	{
	private:
		Task();
	public:
		~Task();

	private:
		Library* _Library;
		int _FunctionId;
		Invoker* _Invoker;
		float* _StageTime;

	private:
		int _CtrlMode;
		cstr8 _CtrlSetting;

	public:
		//parse task from XML file
		static Task* Parse(cstr8 path);

	public:
		//set stage elapsed time (s)
		void SetStageTime(int stage, float time);

		//get stage elapsed time (s)
		float GetStageTime(int stage);

		//get library for task
		Library* GetLibrary();

		//get function for task
		int GetFunctionId();

		//get invoker
		Invoker* GetInvoker();

	public:
		//get control mode
		int GetCtrlMode();

		//get control setting
		cstr8 GetCtrlSetting();

	public:
		//run as specified worker
		bool RunAs(Worker* worker);
	};
}