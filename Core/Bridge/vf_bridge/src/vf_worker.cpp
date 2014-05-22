#include "vf_worker.h"
#include "vf_task.h"
#include "vf_thread.h"
#include "vf_stack.h"
#include "vf_context.h"

namespace vapula
{
	Worker::Worker()
	{
		_Lock = new Lock();
		_LoadFactor = 1;
	}

	Worker::~Worker()
	{
		Offline();
		Clear(_Lock);
	}

	void Worker::Online()
	{
		SYSTEM_INFO system;
		GetSystemInfo(&system);
		uint32 cpu_total = system.dwNumberOfProcessors;
		for (uint32 i = 0; i < _LoadFactor; i++)
		{
			uint32 mask = 0x1;
			for (uint32 j = 0; j < cpu_total; j++)
			{
				mask <<= j;
				Thread* thread = new Thread();
				thread->SetTemp(false);
				thread->SetCPUs(mask);
				_LiveThreads.push_back(thread);
			}
		}
	}

	void Worker::Offline()
	{
		typedef list<Thread*>::iterator iter;
		for (iter i = _LiveThreads.begin(); i != _LiveThreads.end(); i++)
			Clear(*i);
		_LiveThreads.empty();
		for (iter i = _TempThreads.begin(); i != _TempThreads.end(); i++)
			Clear(*i);
		_TempThreads.empty();
		_Tasks.empty();
	}

	void Worker::StartTask(Task* task)
	{
		_Lock->Enter();
		Thread* thread = GetBusyThread(task);
		if (thread == null)
		{
			thread = GetIdleThread();
			if (thread == null)
			{
				thread = new Thread();
				_TempThreads.push_back(thread);
			}
			thread->SetTask(task);
		}
		_Lock->Leave();
	}

	void Worker::StopTask(Task* task)
	{
		_Lock->Enter();
		Thread* thread = GetBusyThread(task);
		if (thread != null)
		{
			if (!thread->IsTemp())
			{
				Thread* thread2 = new Thread();
				thread2->SetTemp(false);
				thread2->SetCPUs(thread->GetCPUs());
				_LiveThreads.push_back(thread2);
			}
			RemoveThread(thread);
			thread->Terminate();
			Stack* stack = task->GetStack();
			Context* context = stack->GetContext();
			context->SetReturnCode(VF_RETURN_TERMINATE);
			context->SetState(VF_STATE_IDLE, task);
		}
		_Lock->Leave();
	}

	void Worker::PauseTask(Task* task)
	{
		_Lock->Enter();
		Thread* thread = GetBusyThread(task);
		if (thread != null)
		{
			if (!thread->IsTemp())
			{
				Thread* thread2 = new Thread();
				thread2->SetTemp(false);
				thread2->SetCPUs(thread->GetCPUs());
				_LiveThreads.push_back(thread2);
			}
			RemoveThread(thread);
			thread->Suspend();
			Stack* stack = task->GetStack();
			Context* context = stack->GetContext();
			context->SetState(VF_STATE_PAUSE, task);
			context->SetControlCode(VF_CTRL_NULL, task);
		}
		_Lock->Leave();
	}

	bool Worker::ResumeTask(Task* task)
	{
		bool done = false;
		_Lock->Enter();
		Thread* thread = GetBusyThread(task);
		if (thread != null)
			done = thread->Resume();
		_Lock->Leave();
		return done;
	}
}