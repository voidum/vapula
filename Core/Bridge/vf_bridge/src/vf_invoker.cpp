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
		StackHub* stack_hub = StackHub::GetInstance();
		_Stack->SetStackId(GetCurrentThreadId());

		stack_hub->Link(_Stack);
		_Entry();
		stack_hub->Kick(_Stack);;

		Context* ctx = _Stack->GetContext();
		ctx->SetState(VF_STATE_IDLE);
		return 0;
	}

	bool Invoker::Initialize(Library* lib, int fid)
	{
		_Stack = new Stack();
		_Stack->SetEnvelope(lib->CreateEnvelope(fid));
		_Stack->SetContext(new Context());
		_Stack->SetFunctionId(fid);
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
		ctx->SetState(VF_STATE_BUSY);
		ctx->SetCtrlCode(VF_CTRL_NULL);
		ctx->SetReturnCode(VF_RETURN_NULLTASK);
		_Thread = (HANDLE)_beginthreadex(null, 0, func_addr.thread, this, 0, null);
		if(_Thread <= 0)
			return false;
		return true;
	}

	void Invoker::Stop(uint32 wait)
	{
		Context* ctx = _Stack->GetContext();
		if(wait == 0)
		{
			TerminateThread(_Thread, 1);
			WaitForSingleObject(_Thread, INFINITE);
			ctx->SetReturnCode(VF_RETURN_CANCELBYFORCE);
		}
		else
		{
			ctx->SetCtrlCode(VF_CTRL_CANCEL);
			DWORD dw = WaitForSingleObject(_Thread, wait);
			if(dw != WAIT_OBJECT_0)
			{
				TerminateThread(_Thread, 1);
				WaitForSingleObject(_Thread, INFINITE);
				ctx->SetReturnCode(VF_RETURN_CANCELBYFORCE);
			}
			else
			{
				ctx->SetReturnCode(VF_RETURN_CANCELBYMSG);
			}
		}
		ctx->SetCtrlCode(VF_CTRL_NULL);
		ctx->SetState(VF_STATE_IDLE);
		CloseHandle(_Thread);
		_Thread = null;
	}

	void Invoker::Pause(uint32 wait)
	{
		Context* ctx = _Stack->GetContext();
		bool paused = false;
		if(wait != 0)
		{
			int times = wait / 25;
			if(wait % 25 != 0) 
				times++;
			ctx->SetCtrlCode(VF_CTRL_PAUSE);
			for(int i=0; i<times; i++)
			{
				if(ctx->GetState() == VF_STATE_PAUSE)
				{
					paused = true;
					break;
				}
				Sleep(25);
			}
		}
		if(!paused)
		{
			_IsSuspend = true;
			SuspendThread(_Thread);
		}
		ctx->SetCtrlCode(VF_CTRL_NULL);
		ctx->SetState(VF_STATE_PAUSE);
	}

	void Invoker::Resume()
	{
		Context* ctx = _Stack->GetContext();
		if(_IsSuspend)
		{
			_IsSuspend = false;
			ResumeThread(_Thread);
			ctx->SetState(VF_STATE_BUSY);
		}
		else
		{
			ctx->SetCtrlCode(VF_CTRL_RESUME);
		}
	}

	void Invoker::Restart(uint32 wait)
	{
		//TODO: support ctrl code restart
	}
}