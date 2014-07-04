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
		//get OS thread id
		int GetThreadId();

	public:
		Task* GetTask();
		void SetTask(Task* task);

	public:
		uint32 GetCPUs();
		void SetCPUs(uint32 mask);

	public:
		bool IsTemp();
		void SetTemp(bool temp);

	public:
		bool IsSuspend();

	public:
		void Start();
		void Terminate();
		void Suspend();
		bool Resume();
	};
}