#pragma once

#include "vf_base.h"

namespace vapula
{
	class Task;

	class Thread
	{
	public:
		Thread();
		~Thread();

	private:
		HANDLE _Handle;
		Task* _Task;
		uint32 _CPUMask;
		bool _IsTemp;
		bool _IsSuspend;

	private:
		static uint32 WINAPI Handler(raw sender);

	public:
		bool IsTemp();
		bool IsSuspend();
		uint32 GetCPUs();

	public:
		void SetTemp(bool temp);
		void SetCPUs(uint32 mask);
		void SetTask(Task* task);

	public:
		void Suspend();
		void Terminate();
		bool Resume();
	};
}