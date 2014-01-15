#include "vf_invoker.h"
#include "vf_library.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_envelope.h"
#include "vf_driver.h"
#include "process.h"

namespace vapula
{
	Invoker::Invoker()
	{
		_Thread = null;
		_Stack = null;
		_IsSuspend = false;
	}

	Invoker::~Invoker()
	{
		if(_Thread != null) 
			CloseHandle(_Thread);
		Clear(_Stack);
	}

	Stack* Invoker::GetStack()
	{
		return _Stack;
	}

	uint32 WINAPI Invoker::Entry()
	{
		_Stack->TokenOn(_StackKey);
		_Stack->SetStackId(GetCurrentThreadId());
		_Stack->TokenOff(_StackKey);

		StackHub* stack_hub = StackHub::GetInstance();
		stack_hub->Link(_Stack);
		_Entry();
		stack_hub->Kick(_Stack);

		Context* ctx = _Stack->GetContext();
		ctx->TokenOn(_ContextKey);
		ctx->SetState(VF_STATE_IDLE);
		ctx->TokenOff(_ContextKey);
		return 0;
	}

	bool Invoker::Initialize(Library* lib, int id)
	{
		_Stack = new Stack();
		_Stack->SetMethodId(id);
		_Stack->SetEnvelope(lib->GetEnvelope(id)->Copy());

		Context* ctx = new Context();
		_Stack->SetContext(ctx);
		ctx->TokenOff(_ContextKey);

		_Stack->TokenOff(_StackKey);
		return true;
	}

	bool Invoker::Start()
	{
		union 
		{
			uint32 (WINAPI *thread)(PVOID);
			uint32 (WINAPI Invoker::*member)();
		} func_addr;
		func_addr.member = &Invoker::Entry;

		if(_Thread != null)
			CloseHandle(_Thread);

		Context* ctx = _Stack->GetContext();
		ctx->TokenOn(_ContextKey);
		ctx->SetState(VF_STATE_BUSY_BACK);
		ctx->SetCtrlCode(VF_CTRL_NULL);
		ctx->TokenOff(_ContextKey);

		ctx->SetReturnCode(VF_RETURN_NULLTASK);

		_Thread = (HANDLE)_beginthreadex(null, 0, func_addr.thread, this, 0, null);
		if(_Thread <= 0)
			return false;
		return true;
	}

	void Invoker::Stop(uint32 wait)
	{
		Context* ctx = _Stack->GetContext();
		bool finish = false;
		if(wait != 0)
		{
			ctx->TokenOn(_ContextKey);
			ctx->SetCtrlCode(VF_CTRL_CANCEL);
			ctx->TokenOff(_ContextKey);

			DWORD dw = WaitForSingleObject(_Thread, wait);
			if(dw == WAIT_OBJECT_0)
				finish = true;
		}
		if(!finish)
		{
			TerminateThread(_Thread, 1);
			WaitForSingleObject(_Thread, INFINITE);
			ctx->SetReturnCode(VF_RETURN_TERMINATE);
		}
		ctx->TokenOn(_ContextKey);
		ctx->SetCtrlCode(VF_CTRL_NULL);
		ctx->SetState(VF_STATE_IDLE);
		ctx->TokenOff(_ContextKey);
		
		CloseHandle(_Thread);
		_Thread = null;
	}

	void Invoker::Pause(uint32 wait)
	{
		Context* ctx = _Stack->GetContext();
		_IsSuspend = false;
		if(wait != 0)
		{
			int times = wait / 25;
			if(wait % 25 != 0) 
				times++;

			ctx->TokenOn(_ContextKey);
			ctx->SetCtrlCode(VF_CTRL_PAUSE);
			ctx->TokenOff(_ContextKey);

			for(int i=0; i<times; i++)
			{
				byte state = ctx->GetCurrentState();
				if(state == VF_STATE_PAUSE)
					return;
				Sleep(25);
			}
		}
		_IsSuspend = true;
		SuspendThread(_Thread);
		ctx->SwitchHold();
	}

	void Invoker::Resume()
	{
		Context* ctx = _Stack->GetContext();
		if(_IsSuspend)
		{
			_IsSuspend = false;
			ResumeThread(_Thread);
			ctx->SwitchHold();
		}
		else
		{
			ctx->TokenOn(_ContextKey);
			ctx->SetCtrlCode(VF_CTRL_RESUME);
			ctx->TokenOff(_ContextKey);
		}
	}

	void Invoker::Restart(uint32 wait)
	{
		//TODO: support ctrl code restart
	}
}