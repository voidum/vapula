#include "worker_null.h"
#include "vf_task.h"
#include "vf_invoker.h"
#include "vf_config.h"

using namespace vapula;

Worker_NULL::Worker_NULL() { }

Worker_NULL::~Worker_NULL() { }

bool Worker_NULL::RunStageA()
{
	return true;
}

bool Worker_NULL::RunStageB()
{
	Config* config = Config::GetInstance();
	int freq_monitor = config->IsRealTimeMonitor() ? 5 : 50;

	Invoker* inv = _Task->GetInvoker();
	inv->Start();
	Context* ctx = inv->GetContext();
	while(ctx->GetState() != VF_STATE_IDLE)
		Sleep(freq_monitor);
	return true;
}

bool Worker_NULL::RunStageC()
{
	return true;
}