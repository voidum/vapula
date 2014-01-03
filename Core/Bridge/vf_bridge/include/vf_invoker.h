#pragma once

#include "vf_utility.h"

namespace vapula
{
	class Library;
	class Stack;
	class Context;
	class Envelope;
	class Aspect;

	//invoker
	class VAPULA_API Invoker
	{
	protected:
		Invoker();
	public:
		virtual ~Invoker();
	protected:
		int _FuncId;
		Stack* _Stack;
	protected:
		object _Thread;
		bool _IsSuspend;
	protected:
		//invoke routine
		virtual uint32 WINAPI _ThreadProc();
	public:
		//initialize invoker
		virtual bool Initialize(Library* lib, int fid); 
	public:
		//get function id
		int GetFunctionId();

		//get envelope
		Envelope* GetEnvelope();

		//get context
		Context* GetContext();

		//get aspect
		Aspect* GetAspect(cstr8 id);
	public:
		//start
		bool Start();

		//stop
		void Stop(uint32 wait = 0);

		//pause
		void Pause(uint32 wait = 0);

		//resume
		void Resume();

		//restart
		void Restart(uint32 wait = 0);
	};
}