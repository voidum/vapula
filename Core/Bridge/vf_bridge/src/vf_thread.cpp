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
				if (thread->_IsTemp)
					break;
				Sleep(25);
				continue;
			}
			thread->_Task->Invoke();
			thread->_Task = null;
		}
		return 0;
	}

	int Thread::GetThreadId()
	{
		return ::GetThreadId(_Handle);
	}

	Task* Thread::GetTask()
	{
		return _Task;
	}

	void Thread::SetTask(Task* task)
	{
		_Task = task;
	}

	bool Thread::IsTemp()
	{
		return _IsTemp;
	}

	void Thread::SetTemp(bool temp)
	{
		_IsTemp = temp;
	}

	uint32 Thread::GetCPUs()
	{
		return _CPUMask;
	}
	
	void Thread::SetCPUs(uint32 mask)
	{
		_CPUMask = mask;
		if (_Handle != null)
			SetThreadAffinityMask(_Handle, _CPUMask);
	}

	bool Thread::IsSuspend()
	{
		return _IsSuspend;
	}

	void Thread::Start()
	{
		if (_Handle == null)
		{
			_Handle = (HANDLE)_beginthreadex(null, 0, Thread::Handler, this, 0, 0);
			SetCPUs(_CPUMask);
		}
	}

	void Thread::Terminate()
	{
		if (TerminateThread(_Handle, 0) != 0)
		{
			WaitForSingleObject(_Handle, INFINITE);
			CloseHandle(_Handle);
		}
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