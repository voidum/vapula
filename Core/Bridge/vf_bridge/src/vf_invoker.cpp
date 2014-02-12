#include "vf_invoker.h"
#include "vf_method.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_envelope.h"
#include "vf_driver.h"
#include "process.h"

namespace vapula
{
	Invoker::Invoker()
	{
		_Stack = null;
		_Thread = null;
		_IsSuspend = false;
	}

	Invoker::~Invoker()
	{
		if(_Thread != null)
			CloseHandle(_Thread);
		Clear(_Stack);
	}

	bool Invoker::Bind(Method* mt)
	{
		_Stack = new Stack();
		_Stack->SetMethodId(str::Copy(mt->GetMethodId()), this);
		_Stack->SetEnvelope(mt->GetEnvelope()->Copy(), this);
		_Stack->SetContext(new Context(), this);
		return true;
	}

	uint32 WINAPI Invoker::Entry(object sender)
	{
		Invoker* inv = (Invoker*)sender;
		Stack* stack = inv->GetStack();
		stack->SetStackId(GetCurrentThreadId(), inv);
		Context* ctx = stack->GetContext();

		StackHub* stack_hub = StackHub::GetInstance();
		stack_hub->Link(stack);
		try {
			inv->OnProcess();
		} catch (Error*) {
			ctx->SetState(VF_STATE_ROLLBACK, inv);
			inv->OnRollback();
		} catch (...) {
			ShowMsgbox("FATAL ERROR");
		}
		stack_hub->Kick(stack);

		ctx->SetState(VF_STATE_IDLE, inv);
		return null;
	}


	Stack* Invoker::GetStack()
	{
		return _Stack;
	}

	bool Invoker::Start()
	{
		if(_Thread != null)
			CloseHandle(_Thread);

		Context* ctx = _Stack->GetContext();
		ctx->SetCtrlCode(VF_CTRL_NULL, this);
		ctx->SetReturnCode(VF_RETURN_NULLTASK);
		ctx->SetState(VF_STATE_BUSY_BACK, this);

		_Thread = (HANDLE)_beginthreadex(null, 0, Entry, this, 0, null);
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
			ctx->SetCtrlCode(VF_CTRL_CANCEL, this);
			DWORD dw = WaitForSingleObject(_Thread, wait);
			if(dw == WAIT_OBJECT_0)
				finish = true;
		}
		if(!finish)
		{
			TerminateThread(_Thread, 1);
			WaitForSingleObject(_Thread, INFINITE);
			ctx->SetReturnCode(VF_RETURN_TERMINATE);
			ctx->SetState(VF_STATE_IDLE, this);
		}
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

			ctx->SetCtrlCode(VF_CTRL_PAUSE, this);
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
			ctx->SetCtrlCode(VF_CTRL_RESUME, this);
		}
	}

	bool Invoker::Restart(uint32 wait)
	{
		Stop(wait);
		return Start();
	}
}