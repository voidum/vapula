#include "vf_weaver.h"
#include "vf_aspect.h"
#include "vf_invoker.h"
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
		Invoker* inv = aspect->GetInvoker();
		Stack* stk_aspe = inv->GetStack();
		Dataset* ds = stk_aspe->GetDataset();
		Record* rec = (*ds)[1];
		if(rec != null)
		{
			Stack* stk = Stack::Instance();
			rec->WriteAt(stk->GetStackId());
		}
		inv->Start();
	}

	void Weaver::Wait(Aspect* aspect)
	{
		if(aspect->IsAsync())
			return;
		Invoker* inv = aspect->GetInvoker();
		Stack* stk = inv->GetStack();
		Context* ctx = stk->GetContext();
		Setting* setting = Setting::Instance();
		int freq_monitor = setting->IsRealTimeMonitor() ? 5 : 50;
		while(ctx->GetCurrentState() != VF_STATE_IDLE)
			Sleep(freq_monitor);
	}
}