#include "vf_loop_file.h"

namespace vapula
{
	LoopFile::LoopFile()
	{
	}

	LoopFile::~LoopFile()
	{
	}

	bool LoopFile::Run()
	{
		while (true)
		{
			ShowMsgbox("test");
			Sleep(50);
		}
		return true;
	}
}

/*
using namespace vapula;

Worker_Null::Worker_Null() { }

Worker_Null::~Worker_Null() { }

bool Worker_Null::OnPrepare()
{
	return Worker::OnPrepare();
}

bool Worker_Null::OnProcess()
{
	Setting* setting = Setting::GetInstance();
	int freq_monitor = setting->IsRealTimeMonitor() ? 5 : 50;
	Stack* stack = _Invoker->GetStack();
	Context* ctx = stack->GetContext();

	_Invoker->Start();
	while(ctx->GetCurrentState() != VF_STATE_IDLE)
		Sleep(freq_monitor);
	return true;
}

bool Worker_Null::OnFinish()
{
	return Worker::OnFinish();
}
*/