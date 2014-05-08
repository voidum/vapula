#pragma once

#include "vf_base.h"

namespace vapula
{
	class Stack;
	class Method;
	class Worker;

	//invoker
	class VAPULA_API Invoker
	{
	friend class Worker;

	protected:
		bool _IsSuspend;
		Stack* _Stack;

	protected:
		Invoker();

	public:
		virtual ~Invoker();

	public:
		//bind invoker with method
		virtual bool Bind(Method* mt);

	protected:
		//invoke routine
		void Invoke();

		//invoke custom process
		virtual void OnProcess() = 0;

		//invoke custom rollback
		virtual void OnRollback() = 0;

		//invoke custom process safely
		void OnSafeProcess();

		//invoke custom process safely
		void OnSafeRollback();

	public:
		//get stack for invoker
		Stack* GetStack();

		//start
		bool Start(uint32 wait = 0);

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