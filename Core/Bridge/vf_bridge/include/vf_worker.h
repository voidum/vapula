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
		list<HANDLE> _FrontThreads;
		list<HANDLE> _RearThreads;
		queue<Invoker*> _Tasks;

	protected:
		static uint32 WINAPI Entry(raw sender);

	public:
		//online worker
		bool Online();

		//offline worker
		void Offline();

	public:
		//start invoker
		bool Start(Invoker* task, uint32 wait);

		//stop invoker
		void Stop(Invoker* task, uint32 wait);

		//pause invoker
		void Pause(Invoker* task, uint32 wait);

		//resume invoker
		void Resume(Invoker* task);

		//restart invoker
		bool Restart(Invoker* task, uint32 wait);

		//clear tasks
		void Clear();
	};
}