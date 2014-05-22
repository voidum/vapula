#pragma once

#include "vf_base.h"

namespace vapula
{
	class Stack;
	class Method;
	class Worker;

	//task
	class VAPULA_API Task
	{
	friend class Worker;
	friend class Thread;

	protected:
		Stack* _Stack;

	protected:
		Task();

	public:
		virtual ~Task();

	public:
		//bind task with method
		virtual bool Bind(Method* mt);

	protected:
		//invoke task
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
		//get stack for task
		Stack* GetStack();

		//start
		void Start();

		//stop
		void Stop(uint32 wait = 0);

		//pause
		void Pause(uint32 wait = 0);

		//resume
		void Resume();
	};
}