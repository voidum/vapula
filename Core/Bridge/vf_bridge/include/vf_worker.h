#pragma once

#include "vf_base.h"
#include <queue>

namespace vapula
{
	class Thread;
	class Task;

	using std::queue;

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
		list<Thread*> _HeadThreads;
		list<Thread*> _RearThreads;
		queue<Task*> _Tasks;
		uint32 _DoneCount;
		uint32 _QueueCount;

	protected:
		static uint32 WINAPI Entry(raw sender);

	public:
		//online worker
		bool Online();

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
		uint32 CountQueueTasks();
	};
}