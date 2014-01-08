#pragma once

#include "vf_utility.h"

namespace vapula
{
	class Library;
	class Stack;
	class Context;
	class Envelope;
	class Token;

	//invoker
	class VAPULA_API Invoker
	{
	protected:
		Invoker();
	public:
		virtual ~Invoker();

	protected:
		HANDLE _Thread;
		bool _IsSuspend;
		Stack* _Stack;

	protected:
		//invoke routine
		uint32 WINAPI Entry();

		//invoke custom core routine
		virtual void _Entry() = 0;

	public:
		//initialize invoker
		virtual bool Initialize(Library* lib, int fid); 

	public:
		//get stack of task by invoker
		Stack* GetStack();

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