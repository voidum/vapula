#include "vf_weaver.h"
#include "vf_aspect.h"
#include "vf_task.h"
#include "vf_stack.h"
#include "vf_dataset.h"
#include "vf_context.h"
#include "vf_setting.h"

namespace vapula
{
	Weaver* Weaver::_Instance = null;

	Weaver* Weaver::Instance()
	{
		return Weaver::_Instance;
	}

	Weaver::Weaver() { }

	Weaver::~Weaver()
	{
	}

	void Weaver::Invoke(Aspect* aspect)
	{
		Task* task = aspect->GetTask();
		//Stack* stack = task->GetStack();
		//Dataset* dataset = stack->GetDataset();
		//init record
		//Record* record = (*dataset)[1];
		task->Start();
	}

	void Weaver::Join(Aspect* aspect)
	{
		Task* task = aspect->GetTask();
		Stack* stack = task->GetStack();
		Context* context = stack->GetContext();
		Setting* setting = Setting::Instance();
		int freq_monitor = setting->IsRealTimeMonitor() ? 5 : 50;
		while(context->GetCurrentState() != VF_STATE_IDLE)
			Sleep(freq_monitor);
	}
}