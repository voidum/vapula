#include "vf_thread.h"
#include "vf_worker.h"
#include "vf_task.h"

namespace vapula
{
	uint32 Thread::Handler(raw sender)
	{
		Thread* thread = (Thread*)sender;
		for (;;)
		{
			if (thread->_Task == null)
			{
				Sleep(50);
				continue;
			}
			thread->_Task->Invoke();
			thread->_Task = null;
		}
		return 0;
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
}