#include "stdafx.h"
#include "tcm_Invoker.h"
#include "tcm_library.h"
#include "tcm_driver.h"

namespace tcm
{
	Invoker::Invoker()
	{
		_FuncId = -1;
		_Envelope = NULL;
		_Context = NULL;
		_ContextToken = NULL;
		_Thread = NULL;
		_IsSuspend = false;
	}

	Invoker::~Invoker()
	{
		if(_Thread != NULL) CloseHandle(_Thread);
		Clear(_Envelope);
		Clear(_Context);
		Clear(_ContextToken);
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

	Token* Invoker::GetContextToken() { return _ContextToken; }

	UINT WINAPI Invoker::_ThreadProc()
	{
		return TCM_RETURN_NULLTASK;
	}

	bool Invoker::Start()
	{
		union 
		{
			UINT (WINAPI *thread)(PVOID);
			UINT (WINAPI Invoker::*member)();
		} func_addr;
		func_addr.member = &Invoker::_ThreadProc;

		Clear(_Context);
		Clear(_ContextToken);
		_Context = new Context();
		_ContextToken = Token::Stamp(_Context);
		_Context->SetState(_ContextToken, TCM_STATE_BUSY);
		if(_Thread != NULL) CloseHandle(_Thread);
		_Thread = (HANDLE)_beginthreadex(NULL, 0, func_addr.thread, this, 0, NULL);
		//if Assert [_Thread]
		return true;
	}

	void Invoker::Stop(UINT wait)
	{
		if(wait == 0)
		{
			TerminateThread(_Thread, 1);
			WaitForSingleObject(_Thread, INFINITE);
			_Context->SetReturnCode(_ContextToken, TCM_RETURN_CANCELBYFORCE);
		}
		else
		{
			_Context->SetCtrlCode(_ContextToken, TCM_CTRL_CANCEL);
			DWORD dw = WaitForSingleObject(_Thread, wait);
			if(dw != WAIT_OBJECT_0)
			{
				TerminateThread(_Thread, 1);
				WaitForSingleObject(_Thread, INFINITE);
				_Context->SetReturnCode(_ContextToken, TCM_RETURN_CANCELBYFORCE);
			}
			else
			{
				_Context->SetReturnCode(_ContextToken, TCM_RETURN_CANCELBYMSG);
			}
		}
		_Context->SetCtrlCode(_ContextToken, TCM_CTRL_NULL);
		_Context->SetState(_ContextToken, TCM_STATE_IDLE);
		CloseHandle(_Thread);
		_Thread = NULL;
	}

	void Invoker::Pause(UINT wait)
	{
		bool paused = false;
		if(wait != 0)
		{
			int times = wait / 25;
			if(wait % 25 != 0) times++;
			_Context->SetCtrlCode(_ContextToken, TCM_CTRL_PAUSE);
			for(int i=0; i<times; i++)
			{
				if(_Context->GetState() == TCM_STATE_PAUSE)
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
		_Context->SetCtrlCode(_ContextToken, TCM_CTRL_NULL);
		_Context->SetState(_ContextToken, TCM_STATE_PAUSE);
	}

	void Invoker::Resume()
	{
		if(_IsSuspend)
		{
			_IsSuspend = false;
			ResumeThread(_Thread);
			_Context->SetState(_ContextToken, TCM_STATE_BUSY);
		}
		else
		{
			_Context->SetCtrlCode(_ContextToken, TCM_CTRL_RESUME);
		}
	}

	void Invoker::Restart(UINT wait)
	{
		//TODO: support ctrl code restart
	}
}