#include "worker_null.h"
#include "vf_task.h"
#include "vf_invoker.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_setting.h"

using namespace vapula;

Worker_NULL::Worker_NULL() { }

Worker_NULL::~Worker_NULL() { }

bool Worker_NULL::RunStageA()
{
	return true;
}

bool Worker_NULL::RunStageB()
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

bool Worker_NULL::RunStageC()
{
	return true;
}