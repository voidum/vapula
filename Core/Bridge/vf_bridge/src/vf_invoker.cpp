#include "vf_invoker.h"
#include "vf_library.h"
#include "vf_driver.h"
#include "process.h"

namespace vapula
{
	Invoker::Invoker()
	{
		_FuncId = -1;
		_Envelope = null;
		_Context = null;
		_Thread = null;
		_IsSuspend = false;
	}

	Invoker::~Invoker()
	{
		if(_Thread != null) 
			CloseHandle(_Thread);
		Clear(_Envelope);
		Clear(_Context);
	}

	bool Invoker::Initialize(Library* lib, int fid)
	{
		_Envelope = lib->CreateEnvelope(fid);
		_FuncId = fid;
		return true;
	}

	int Invoker::GetFunctionId() { return _FuncId; }

	Envelope* Invoker::GetEnvelope() { return _Envelope; }

	Context* Invoker::GetContext() { return _Context; }

	uint32 WINAPI Invoker::_ThreadProc()
	{
		return VF_RETURN_NULLTASK;
	}

	bool Invoker::Start()
	{
		union 
		{
			uint32 (WINAPI *thread)(PVOID);
			uint32 (WINAPI Invoker::*member)();
		} func_addr;
		func_addr.member = &Invoker::_ThreadProc;

		Clear(_Context);
		_Context = new Context();
		_Context->SetState(VF_STATE_BUSY);
		if(_Thread != null)
			CloseHandle(_Thread);
		_Thread = (HANDLE)_beginthreadex(null, 0, func_addr.thread, this, 0, null);
		//if Assert [_Thread]
		return true;
	}

	void Invoker::Stop(uint32 wait)
	{
		if(wait == 0)
		{
			TerminateThread(_Thread, 1);
			WaitForSingleObject(_Thread, INFINITE);
			_Context->SetReturnCode(VF_RETURN_CANCELBYFORCE);
		}
		else
		{
			_Context->SetCtrlCode(VF_CTRL_CANCEL);
			DWORD dw = WaitForSingleObject(_Thread, wait);
			if(dw != WAIT_OBJECT_0)
			{
				TerminateThread(_Thread, 1);
				WaitForSingleObject(_Thread, INFINITE);
				_Context->SetReturnCode(VF_RETURN_CANCELBYFORCE);
			}
			else
			{
				_Context->SetReturnCode(VF_RETURN_CANCELBYMSG);
			}
		}
		_Context->SetCtrlCode(VF_CTRL_NULL);
		_Context->SetState(VF_STATE_IDLE);
		CloseHandle(_Thread);
		_Thread = null;
	}

	void Invoker::Pause(uint32 wait)
	{
		bool paused = false;
		if(wait != 0)
		{
			int times = wait / 25;
			if(wait % 25 != 0) 
				times++;
			_Context->SetCtrlCode(VF_CTRL_PAUSE);
			for(int i=0; i<times; i++)
			{
				if(_Context->GetState() == VF_STATE_PAUSE)
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
		_Context->SetCtrlCode(VF_CTRL_NULL);
		_Context->SetState(VF_STATE_PAUSE);
	}

	void Invoker::Resume()
	{
		if(_IsSuspend)
		{
			_IsSuspend = false;
			ResumeThread(_Thread);
			_Context->SetState(VF_STATE_BUSY);
		}
		else
		{
			_Context->SetCtrlCode(VF_CTRL_RESUME);
		}
	}

	void Invoker::Restart(uint32 wait)
	{
		//TODO: support ctrl code restart
	}
}