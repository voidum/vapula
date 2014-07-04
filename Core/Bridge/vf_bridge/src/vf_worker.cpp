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

	Thread* Worker::GetThreadById(int id)
	{
		typedef list<Thread*>::iterator iter;
		Thread* thread = null;
		_Lock->Enter();
		for (iter i = _BusyThreads.begin(); i != _BusyThreads.end();)
		{
			Thread* temp_thread = *i;
			if (temp_thread->GetThreadId() == id)
			{
				thread = temp_thread;
				break;
			}
		}
		_Lock->Leave();
		return thread;
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
		Thread* thread = null;
		for (iter i = _BusyThreads.begin(); i != _BusyThreads.end();)
		{
			Thread* temp_thread = *i;
			Task* temp_task = temp_thread->GetTask();
			if (temp_task == null)
			{
				i = _BusyThreads.erase(i);
				if (!temp_thread->IsTemp())
					_IdleThreads.push_back(temp_thread);
				else
					Clear(temp_thread);
			}
			else 
			{
				if (temp_task == task)
					thread = temp_thread;
				i++;
			}
		}
		return thread;
	}

	void Worker::StartTask(Task* task)
	{
		_Lock->Enter();
		Thread* thread = GetBusyThread(task);
		if (thread == null)
		{
			Stack* stack = task->GetStack();
			Context* context = stack->GetContext();
			context->SetState(VF_STATE_QUEUE, task);
			thread = GetIdleThread();
			if (thread == null)
			{
				thread = new Thread();
				thread->SetTemp(true);
			}
			else
			{
				_IdleThreads.remove(thread);
			}
			thread->SetTask(task);
			thread->Start();
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
				Thread* thread_new = new Thread();
				thread_new->SetTemp(false);
				thread_new->SetCPUs(thread->GetCPUs());
				thread_new->Start();
				_IdleThreads.push_back(thread_new);
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
				Thread* thread_new = new Thread();
				thread_new->SetTemp(false);
				thread_new->SetCPUs(thread->GetCPUs());
				thread_new->Start();
				_IdleThreads.push_back(thread_new);
				thread->SetTemp(true);
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