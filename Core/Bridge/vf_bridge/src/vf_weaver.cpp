#include "vf_weaver.h"
#include "vf_aspect.h"
#include "vf_invoker.h"
#include "vf_stack.h"
#include "vf_envelope.h"
#include "vf_context.h"
#include "vf_setting.h"

namespace vapula
{
	Weaver* Weaver::_Instance = null;

	Weaver* Weaver::GetInstance()
	{
		Lock* lock = Lock::GetCtorLock();
		lock->Enter();
		if(Weaver::_Instance == null)
			Weaver::_Instance = new Weaver();
		lock->Leave();
		return Weaver::_Instance;
	}

	Weaver::Weaver() { }

	Weaver::~Weaver()
	{
		KickAll();
	}

	Aspect* Weaver::GetAspect(pcstr id)
	{
		typedef list<Aspect*>::iterator iter;
		for(iter i = _Aspects.begin(); i != _Aspects.end(); i++)
		{
			Aspect* aspe = *i;
			if(strcmp(aspe->GetAspectId(), id) == 0)
				return aspe;
		}
		return null;
	}

	int Weaver::GetCount()
	{
		return _Aspects.size();
	}

	void Weaver::Link(Aspect* aspect)
	{
		Aspect* aspe = GetAspect(aspect->GetAspectId());
		if(aspe == null)
			_Aspects.push_back(aspe);
	}

	void Weaver::Kick(pcstr id)
	{
		typedef list<Aspect*>::iterator iter;
		for(iter i = _Aspects.begin(); i != _Aspects.end(); i++)
		{
			Aspect* aspe = *i;
			if(strcmp(aspe->GetAspectId(), id) == 0)
			{
				_Aspects.erase(i);
				Clear(aspe);
				break;
			}
		}
	}

	void Weaver::KickAll()
	{
		typedef list<Aspect*>::iterator iter;
		for(iter i = _Aspects.begin(); i != _Aspects.end(); i++)
			Clear(*i);
		_Aspects.clear();
	}

	void Weaver::Reach(pcstr frame) 
	{
		typedef list<Aspect*>::iterator iter;
		list<Aspect*> reqs;
		for(iter i = _Aspects.begin(); i != _Aspects.end(); i++)
		{
			Aspect* aspe = *i;
			if(aspe->TryMatch(frame))
			{
				reqs.push_back(aspe);
				Invoke(aspe);
			}
		}
		for(iter i = reqs.begin(); i != reqs.end(); i++)
			Wait(*i);
	}

	void Weaver::Invoke(Aspect* aspect)
	{
		Invoker* inv = aspect->GetInvoker();
		Stack* stk_aspe = inv->GetStack();
		Envelope* env = stk_aspe->GetEnvelope();
		Stack* stk = Stack::GetInstance();
		env->WriteValue(1, stk->GetStackId());
		inv->Start();
	}

	void Weaver::Wait(Aspect* aspect)
	{
		if(aspect->GetMode() == VF_ASPECT_ASYNC)
			return;
		Invoker* inv = aspect->GetInvoker();
		Stack* stk = inv->GetStack();
		Context* ctx = stk->GetContext();
		Setting* setting = Setting::GetInstance();
		int freq_monitor = setting->IsRealTimeMonitor() ? 5 : 50;
		while(ctx->GetCurrentState() != VF_STATE_IDLE)
			Sleep(freq_monitor);
	}
}