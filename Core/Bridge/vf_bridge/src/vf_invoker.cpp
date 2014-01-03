#include "vf_invoker.h"
#include "vf_library.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_envelope.h"
#include "vf_aspect.h"
#include "vf_driver.h"
#include "process.h"

namespace vapula
{
	Invoker::Invoker()
	{
		_FuncId = -1;
		_Stack = null;
		_IsSuspend = false;
	}

	Invoker::~Invoker()
	{
		if(_Thread != null) 
			CloseHandle(_Thread);
		Clear(_Stack);
	}

	uint32 WINAPI Invoker::_ThreadProc()
	{
		return VF_RETURN_NULLTASK;
	}

	bool Invoker::Initialize(Library* lib, int fid)
	{
		_Stack->SetEnvelope(lib->CreateEnvelope(fid));
		_FuncId = fid;
		return true;
	}

	int Invoker::GetFunctionId()
	{
		return _FuncId; 
	}

	Envelope* Invoker::GetEnvelope() 
	{
		return _Stack->GetEnvelope(); 
	}

	Context* Invoker::GetContext()
	{
		return _Stack->GetContext(); 
	}

	Aspect* Invoker::GetAspect(cstr8 id)
	{
		return _Stack->GetAspect(id);
	}

	bool Invoker::Start()
	{
		union 
		{
			uint32 (WINAPI *thread)(PVOID);
			uint32 (WINAPI Invoker::*member)();
		} func_addr;
		func_addr.member = &Invoker::_ThreadProc;

		Context* ctx = new Context();
		ctx->SetState(VF_STATE_BUSY);
		_Stack->SetContext(ctx);
		if(_Thread != null)
			CloseHandle(_Thread);
		_Thread = (HANDLE)_beginthreadex(null, 0, func_addr.thread, this, 0, null);
		//if Assert [_Thread]
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