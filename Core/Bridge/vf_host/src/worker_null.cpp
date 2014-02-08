#include "worker_null.h"
#include "vf_task.h"
#include "vf_invoker.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_setting.h"

using namespace vapula;

Worker_Null::Worker_Null() { }

Worker_Null::~Worker_Null() { }

bool Worker_Null::RunStageA()
{
	return true;
}

bool Worker_Null::RunStageB()
{
	Setting* setting = Setting::GetInstance();
	int freq_monitor = setting->IsRealTimeMonitor() ? 5 : 50;

	Invoker* inv = _Task->GetInvoker();
	Stack* stack = inv->GetStack();
	inv->Start();
	Context* ctx = stack->GetContext();
	while(ctx->GetCurrentState() != VF_STATE_IDLE)
		Sleep(freq_monitor);
	return true;
}

bool Worker_Null::RunStageC()
{
	return true;
}