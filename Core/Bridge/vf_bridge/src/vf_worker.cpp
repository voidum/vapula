#include "vf_worker.h"
#include "vf_task.h"
#include "vf_thread.h"
#include "vf_stack.h"
#include "vf_context.h"

namespace vapula
{
	Worker* Worker::_Instance = null;

	Worker* Worker::Instance()
	{
		if (Worker::_Instance == null)
		{
			Lock* lock = Lock::GetCtorLock();
			lock->Enter();
			if (Worker::_Instance == null)
				Worker::_Instance = new Worker();
			lock->Leave();
		}
		return Worker::_Instance;
	}

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
		_Lock->Enter();
		for (uint32 i = 0; i < _LoadFactor; i++)
		{
			uint32 mask = 0x1;
			for (uint32 j = 0; j < cpu_total; j++)
			{
				mask <<= j;
				Thread* thread = new Thread();
				thread->SetTemp(false);
				thread->SetCPUs(mask);
				thread->Start();
				_IdleThreads.push_back(thread);
			}
		}
		_Lock->Leave();
	}

	void Worker::Offline()
	{
		typedef list<Thread*>::iterator iter;
		_Lock->Enter();
		for (iter i = _BusyThreads.begin(); i != _BusyThreads.end(); i++)
		{
			Thread* thread = *i;
			thread->Terminate();
			Clear(thread);
		}
		_BusyThreads.empty();
		for (iter i = _IdleThreads.begin(); i != _IdleThreads.end(); i++)
		{
			Thread* thread = *i;
			if (!thread->IsTemp())
				thread->Terminate();
			Clear(*i);
		}
		_IdleThreads.empty();
		_Lock->Leave();
	}

	Thread* Worker::GetIdleThread()
	{
		if (_IdleThreads.size() > 0)
			return *(_IdleThreads.begin());
		return null;
	}

	Thread* Worker::GetBusyThread(Task* task)
	{
		typedef list<Thread*>::iterator iter;
		for (iter i = _BusyThreads.begin(); i != _BusyThreads.end();)
		{
			Thread* thread = *i;
			Task* cur_task = thread->GetTask();
			if (cur_task == null)
			{
				i = _BusyThreads.erase(i);
				if (!thread->IsTemp())
					_IdleThreads.push_back(thread);
				else
					Clear(thread);
			}
			else if (cur_task == task)
				return thread;
			else
				i++;
		}
		return null;
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
				thread->SetTemp(true);

				Stack* stack = task->GetStack();
				Context* context = stack->GetContext();
				context->SetState(VF_STATE_QUEUE, task);
				thread->SetTask(task);

				thread->Start();
			}
			else
			{
				Stack* stack = task->GetStack();
				Context* context = stack->GetContext();
				context->SetState(VF_STATE_QUEUE, task);
				thread->SetTask(task);

				_IdleThreads.remove(thread);
			}
			_BusyThreads.push_back(thread);
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
				thread2->Start();
				_IdleThreads.push_back(thread2);
				thread->SetTemp(true);
			}
			thread->Terminate();
			Clear(thread);
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
				_IdleThreads.push_back(thread2);
			}
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