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
		queue<Thread*> _HeadThreads;
		queue<Thread*> _BackThreads;
		queue<Task*> _Tasks;
		uint32 _DoneCount;
		uint32 _QueueCount;

	public:
		//online worker
		void Online();

		//offline worker
		void Offline();

	public:
		//start task
		bool Start(Task* task, uint32 wait);

		//stop task
		void Stop(Task* task, uint32 wait);

		//pause task
		void Pause(Task* task, uint32 wait);

		//resume task
		void Resume(Task* task);

		//restart task
		bool Restart(Task* task, uint32 wait);

	public:
		//get count of done tasks
		//maybe overflow !!
		uint32 CountDoneTasks();

		//get count of queue tasks
		//maybe overflow !!
		uint32 CountQueueTasks();
	};
}