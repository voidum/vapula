#pragma once

#include "vf_base.h"
#include <queue>

namespace vapula
{
	class Thread;
	class Task;

	class Worker : Uncopiable
	{
	private:
		Worker();

	public:
		~Worker();

	private:
		static Worker* _Instance;

	public:
		static Worker* Instance();

	private:
		Lock* _Lock;
		uint32 _LoadFactor;
		list<Thread*> _LiveThreads;
		list<Thread*> _TempThreads;
		queue<Task*> _Tasks;
		uint32 _DoneCount;
		uint32 _QueueCount;

	public:
		//online worker
		void Online();

		//offline worker
		void Offline();

	public:
		//get idle thread
		Thread* GetIdleThread();

		//get thread for task
		Thread* GetBusyThread(Task* task);

		//remove thread
		void RemoveThread(Thread* thread);

	public:
		//start task
		void StartTask(Task* task);

		//stop task
		void StopTask(Task* task);

		//pause task
		void PauseTask(Task* task);

		//resume task
		bool ResumeTask(Task* task);

	public:
		//get count of done tasks
		//maybe overflow !!
		uint32 CountDoneTasks();

		//get count of queue tasks
		//maybe overflow !!
		uint32 CountQueueTasks();
	};
}