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
		list<Thread*> _IdleThreads;
		list<Thread*> _BusyThreads;
		uint32 _DoneCount;

	public:
		//online worker
		void Online();

		//offline worker
		void Offline();

	public:
		//get thread with no task
		Thread* GetIdleThread();

		//get thread with task
		Thread* GetBusyThread(Task* task);

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
	};
}