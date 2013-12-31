#pragma once

#include "vf_context.h"
#include "vf_envelope.h"

namespace vapula
{
	class Library;

	//invoker
	class VAPULA_API Invoker
	{
	protected:
		Invoker();
	public:
		virtual ~Invoker();
	protected:
		int _FuncId;
		Envelope* _Envelope;
		Context* _Context;
	protected:
		object _Thread;
		bool _IsSuspend;
		virtual uint32 WINAPI _ThreadProc();
	public:
		//get function id
		int GetFunctionId();

		//get envelope
		Envelope* GetEnvelope();

		//get context
		Context* GetContext();
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
	public:
		//initialize invoker
		virtual bool Initialize(Library* lib, int fid); 
	};
}