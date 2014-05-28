#pragma once

#include "vf_host.h"

namespace vapula
{
	class Worker;

	//task
	class Plan
	{
	private:
		pcstr _Path;
		Library* _Library;
		pcstr _MethodId;
		int _CtrlMode;
		pcstr _CtrlSetting;

	private:
		Plan();
	public:
		~Plan();

	public:
		//load task from XML file
		static Task* Load(pcstr path);

	public:
		//get path of task
		pcstr GetTaskPath();

		//get path of task data
		pcstr GetDataPath();

		//get library for task
		Library* GetLibrary();

		//get method for task
		pcstr GetMethodId();

	public:
		//get control mode
		int GetCtrlMode();

		//get control setting
		pcstr GetCtrlSetting();

	public:
		//run as specified worker
		bool RunAs(Worker* worker);
	};
}