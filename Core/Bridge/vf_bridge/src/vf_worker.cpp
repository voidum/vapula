#include "vf_worker.h"
#include "vf_invoker.h"

namespace vapula
{
	Worker::Worker()
	{
		_Lock = new Lock();
		_Threads = null;
		_ThreadCount = 0;
	}

	Worker::~Worker()
	{
		Stop();
		Clear();
	}

	uint32 Worker::Entry(raw sender)
	{
		Worker* worker = (Worker*)sender;

	}

	bool Worker::Start(Invoker* task, uint32 wait)
	{
		SYSTEM_INFO system;
		GetSystemInfo(&system);
		for (uint32 i = 0; i < system.dwNumberOfProcessors; i++)
		{
		}
	}

	void Worker::Stop(Invoker* task, uint32 wait)
	{
		Context* ctx = _Stack->GetContext();
		bool finish = false;
		if (wait != 0)
		{
			ctx->SetCtrlCode(VF_CTRL_CANCEL, this);
			DWORD dw = WaitForSingleObject(_Thread, wait);
			if (dw == WAIT_OBJECT_0)
				finish = true;
		}
		if (!finish)
		{
			TerminateThread(_Thread, 1);
			WaitForSingleObject(_Thread, INFINITE);
			ctx->SetReturnCode(VF_RETURN_TERMINATE);
			ctx->SetState(VF_STATE_IDLE, this);
		}
		CloseHandle(_Thread);
		_Thread = null;
	}

	void Worker::Pause(Invoker* task, uint32 wait)
	{
		Context* ctx = _Stack->GetContext();
		_IsSuspend = false;
		if (wait != 0)
		{
			int times = wait / 25;
			if (wait % 25 != 0)
				times++;

			ctx->SetCtrlCode(VF_CTRL_PAUSE, this);
			for (int i = 0; i<times; i++)
			{
				byte state = ctx->GetCurrentState();
				if (state == VF_STATE_PAUSE)
					return;
				Sleep(25);
			}
		}
		_IsSuspend = true;
		SuspendThread(_Thread);
		ctx->SwitchHold();
	}

	void Worker::Resume(Invoker* task)
	{
		Context* ctx = _Stack->GetContext();
		if (_IsSuspend)
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

	bool Worker::Restart(Invoker* task, uint32 wait)
	{
	}
}