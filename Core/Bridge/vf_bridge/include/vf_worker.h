#pragma once

#include "vf_base.h"
#include <queue>

namespace vapula
{
	class Invoker;

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
		HANDLE* _Threads;
		uint32 _ThreadCount;
		queue<Invoker*> _Tasks;

	protected:
		static uint32 WINAPI Entry(raw sender);

	public:
		//start worker
		bool Start();

		//stop worker
		void Stop();

	public:
		//assign task
		void Assign(Invoker* task);

		//cancel task
		void Cancel(Invoker* task);

		//
		void HasTask();

		//clear tasks
		void Clear();
	};
}