#include "vf_worker.h"
#include "vf_invoker.h"

namespace vapula
{
	Worker::Worker()
	{
		_Lock = new Lock();
		_Threads = null;
		_ThreadCount = 0;
	}

	Worker::~Worker()
	{
		Stop();
		Clear();
	}

	uint32 Worker::Entry(raw sender)
	{
		Worker* worker = (Worker*)sender;
		union FuncPtr
		{
			uint32(ptr1)();
			raw ptr2;
		};
		FuncPtr ptr;

		ptr.ptr2 = worker->_Tasks.front()->GetEntry(worker);
		ptr.ptr1();
	}

	bool Worker::Start()
	{
		SYSTEM_INFO system;
		GetSystemInfo(&system);
		for (uint32 i = 0; i < system.dwNumberOfProcessors; i++)
		{
		}
	}
}