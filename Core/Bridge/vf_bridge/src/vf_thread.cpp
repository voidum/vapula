#include "vf_thread.h"
#include "vf_worker.h"
#include "vf_task.h"
#include <process.h>

namespace vapula
{
	Thread::Thread()
	{
		_Handle = null;
		_IsSuspend = false;
		_IsTemp = true;
		_Task = null;
		_CPUMask = 0;
	}

	Thread::~Thread()
	{
	}

	uint32 Thread::Handler(raw sender)
	{
		Thread* thread = (Thread*)sender;
		for (;;)
		{
			if (thread->_Task == null)
			{
				Sleep(25);
				continue;
			}
			thread->_Task->Invoke();
			thread->_Task = null;
		}
	}

	bool Thread::IsTemp()
	{
		return _IsTemp;
	}

	bool Thread::IsSuspend()
	{
		return _IsSuspend;
	}

	uint32 Thread::GetCPUs()
	{
		return _CPUMask;
	}

	Task* Thread::GetTask()
	{
		return _Task;
	}

	void Thread::SetTemp(bool temp)
	{
		_IsTemp = temp;
	}

	void Thread::SetCPUs(uint32 mask)
	{
		_CPUMask = mask;
		SetThreadAffinityMask(_Handle, mask);
	}

	void Thread::SetTask(Task* task)
	{
		_Task = task;
	}

	void Thread::Start()
	{
		_Handle = (HANDLE)_beginthreadex(null, 0, Thread::Handler, this, 0, 0);
	}

	void Thread::Terminate()
	{
		TerminateThread(_Handle, 0);
		WaitForSingleObject(_Handle, INFINITE);
		CloseHandle(_Handle);
	}

	void Thread::Suspend()
	{
		SuspendThread(_Handle);
		_IsSuspend = true;
	}

	bool Thread::Resume()
	{
		if (_IsSuspend)
		{
			ResumeThread(_Handle);
			_IsSuspend = false;
			return true;
		}
		return false;
	}
}