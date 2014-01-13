#include "vf_invoker.h"
#include "vf_library.h"
#include "vf_stack.h"
#include "vf_token.h"
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
		StackHub* stack_hub = StackHub::GetInstance();
		Token* token_stk = _Stack->GetToken();
		token_stk->Unlock(_StackKey);
		_Stack->SetStackId(GetCurrentThreadId());
		_StackKey = token_stk->Lock();

		stack_hub->Link(_Stack);
		_Entry();
		stack_hub->Kick(_Stack);

		Context* ctx = _Stack->GetContext();
		Token* token_ctx = ctx->GetToken();
		token_ctx->Unlock(_ContextKey);
		ctx->SetState(VF_STATE_IDLE);
		_ContextKey = token_ctx->Lock();
		return 0;
	}

	bool Invoker::Initialize(Library* lib, int id)
	{
		_Stack = new Stack();
		_Stack->SetMethodId(id);
		_Stack->SetEnvelope(lib->GetEnvelope(id)->Copy());

		Context* ctx = new Context();
		_Stack->SetContext(ctx);
		
		Token* token_ctx = ctx->GetToken();
		_ContextKey = token_ctx->Lock();
		
		Token* token_stk = _Stack->GetToken();
		_StackKey = token_stk->Lock();
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
		Token* token = ctx->GetToken();
		token->Unlock(_ContextKey);
		ctx->SetState(VF_STATE_BUSY_BACK);
		ctx->SetCtrlCode(VF_CTRL_NULL);
		_ContextKey = token->Lock();

		ctx->SetReturnCode(VF_RETURN_NULLTASK);

		_Thread = (HANDLE)_beginthreadex(null, 0, func_addr.thread, this, 0, null);
		if(_Thread <= 0)
			return false;
		return true;
	}

	void Invoker::Stop(uint32 wait)
	{
		Context* ctx = _Stack->GetContext();
		Token* token = ctx->GetToken();
		bool finish = false;
		if(wait != 0)
		{
			token->Unlock(_ContextKey);
			ctx->SetCtrlCode(VF_CTRL_CANCEL);
			_ContextKey = token->Lock();

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
		token->Unlock(_ContextKey);
		ctx->SetCtrlCode(VF_CTRL_NULL);
		ctx->SetState(VF_STATE_IDLE);
		_ContextKey = token->Lock();
		
		CloseHandle(_Thread);
		_Thread = null;
	}

	void Invoker::Pause(uint32 wait)
	{
		Context* ctx = _Stack->GetContext();
		Token* token = ctx->GetToken();
		_IsSuspend = false;
		if(wait != 0)
		{
			int times = wait / 25;
			if(wait % 25 != 0) 
				times++;

			token->Unlock(_ContextKey);
			ctx->SetCtrlCode(VF_CTRL_PAUSE);
			_ContextKey = token->Lock();

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
		Token* token = ctx->GetToken();
		if(_IsSuspend)
		{
			_IsSuspend = false;
			ResumeThread(_Thread);
			ctx->SwitchHold();
		}
		else
		{
			token->Unlock(_ContextKey);
			ctx->SetCtrlCode(VF_CTRL_RESUME);
			_ContextKey = token->Lock();
		}
	}

	void Invoker::Restart(uint32 wait)
	{
		//TODO: support ctrl code restart
	}
}