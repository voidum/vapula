#include "stdafx.h"
#include "worker_null.h"

#include "tcm_Invoker.h"
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
	Flag* flag = config->GetFlag();

	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Invoker* inv = task->GetInvoker();
	inv->Start();
	int freq_monitor = flag->Valid(TCM_CONFIG_RTMON) ? 5 : 50;
	Context* ctx = inv->GetContext();
	while(ctx->GetState() != TCM_STATE_IDLE) Sleep(freq_monitor);
	return true;
}

bool Worker_NULL::RunStageC()
{
	return true;
}