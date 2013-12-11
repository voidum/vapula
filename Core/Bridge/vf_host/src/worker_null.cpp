#include "stdafx.h"
#include "worker_null.h"

#include "tcm_invoker.h"
#include "tcm_config.h"

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

	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Invoker* inv = task->GetInvoker();
	inv->Start();
	Context* ctx = inv->GetContext();
	while(ctx->GetState() != TCM_STATE_IDLE)
		Sleep(freq_monitor);
	return true;
}

bool Worker_NULL::RunStageC()
{
	return true;
}