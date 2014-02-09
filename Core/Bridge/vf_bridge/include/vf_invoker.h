#pragma once

#include "vf_utility.h"

namespace vapula
{
	class Stack;
	class Function;

	//invoker
	class VAPULA_API Invoker
	{
	protected:
		bool _IsSuspend;
		HANDLE _Thread;
		Stack* _Stack;

	protected:
		Invoker();
	public:
		virtual ~Invoker();

	public:
		//bind invoker with function
		virtual bool Bind(Function* func);

	protected:
		//invoke routine
		uint32 WINAPI Entry();

		//invoke custom process
		virtual void OnProcess() = 0;

		//invoke custom rollback
		virtual void OnRollback() = 0;

	public:
		//get stack for invoker
		Stack* GetStack();

		//start
		bool Start();

		//stop
		void Stop(uint32 wait = 0);

		//pause
		void Pause(uint32 wait = 0);

		//resume
		void Resume();

		//restart
		bool Restart(uint32 wait = 0);
	};
}