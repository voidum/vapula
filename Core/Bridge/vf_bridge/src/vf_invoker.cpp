#include "vf_invoker.h"
#include "vf_method.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_dataset.h"
#include "vf_driver.h"
#include "vf_runtime.h"
#include "vf_worker.h"
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
		_Stack->SetDataset(mt->GetDataset()->Copy(), this);
		_Stack->SetContext(new Context(), this);
		_Stack->SetProtect(mt->HasProtect(), this);
		return true;
	}

	uint32 Invoker::Entry()
	{
		_Stack->SetStackId(GetCurrentThreadId(), this);
		Context* context = _Stack->GetContext();

		Runtime* runtime = Runtime::Instance();
		runtime->Link(_Stack);
		try {
			if (_Stack->HasProtect())
				OnSafeProcess();
			else
				OnProcess();
		} catch (Error*) {
			context->SetState(VF_STATE_ROLLBACK, this);
			if (_Stack->HasProtect())
				OnSafeRollback();
			else
				OnRollback();
			context->SetReturnCode(VF_RETURN_ERROR);
		}
		runtime->Kick(_Stack);

		context->SetState(VF_STATE_IDLE, this);
		return null;
	}

	void Invoker::OnSafeProcess()
	{
		__try {
			OnProcess();
		}
		__except (EXCEPTION_EXECUTE_HANDLER) {
			if (_Stack != null) {
				Context* ctx = _Stack->GetContext();
				if (ctx != null) {
					ctx->SetReturnCode(VF_RETURN_UNHANDLED);
				}
			}
			ShowMsgbox(_vf_fatal, _vf_bridge);
		}
	}

	void Invoker::OnSafeRollback()
	{
		__try {
			OnRollback();
		}
		__except (EXCEPTION_EXECUTE_HANDLER) {
			if (_Stack != null) {
				Context* ctx = _Stack->GetContext();
				if (ctx != null) {
					ctx->SetReturnCode(VF_RETURN_UNHANDLED);
				}
			}
			ShowMsgbox(_vf_fatal, _vf_bridge);
		}
	}

	raw Invoker::GetEntry(Worker* worker)
	{
		//valid worker
		//test if can be use
		//!! MAGIC CODE !!
		union FuncPtr
		{
			uint32(ptr1)();
			raw ptr2;
		}ptr;
		ptr.ptr1 = Invoker::Entry;
		return ptr.ptr2;
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
		if (_Thread <= 0)
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