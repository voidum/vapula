#include "vf_stack.h"
#include "vf_context.h"
#include "vf_envelope.h"

namespace vapula
{
	Stack::Stack() { }

	Stack::~Stack() { }

	Stack* Stack::GetInstance()
	{
		StackHub* stackhub = StackHub::GetInstance();
		uint32 id = GetCurrentThreadId();
		Stack* stack = stackhub->GetStack(id);
		return stack;
	}

	uint32 Stack::GetStackId()
	{
		return _StackId;
	}

	void Stack::SetStackId(uint32 id)
	{
		if(AssertOffTI())
			return;
		_StackId = id;
	}

	int32 Stack::GetMethodId()
	{
		return _MethodId;
	}

	void Stack::SetMethodId(int32 id)
	{
		if(AssertOffTI())
			return;
		_MethodId = id;
	}

	Context* Stack::GetContext()
	{
		return _Context;
	}

	void Stack::SetContext(Context* ctx)
	{
		if(AssertOffTI())
			return;
		_Context = ctx;
	}

	Envelope* Stack::GetEnvelope()
	{
		return _Envelope;
	}

	void Stack::SetEnvelope(Envelope* env)
	{
		if(AssertOffTI())
			return;
		_Envelope = env;
	}

	StackHub* StackHub::_Instance = null;

	StackHub* StackHub::GetInstance()
	{
		Lock* lock = Lock::GetCtorLock();
		if(lock->Enter())
		{
			if(StackHub::_Instance == null)
				StackHub::_Instance = new StackHub();
			lock->Leave();
		}
		return StackHub::_Instance;
	}

	StackHub::StackHub() { }
	StackHub::~StackHub()
	{
		KickAll();
	}

	Stack* StackHub::GetStack(uint32 id)
	{
		typedef vector<Stack*>::iterator iter;
		for(iter i = _Stacks.begin(); i != _Stacks.end(); i++)
		{
			Stack* stack = *i;
			if(stack->GetStackId() == id)
				return stack;
		}
		return null;
	}

	int StackHub::GetCount()
	{
		return _Stacks.size();
	}

	bool StackHub::Link(Stack* stack)
	{
		typedef vector<Stack*>::iterator iter;
		for(iter i = _Stacks.begin(); i != _Stacks.end(); i++)
		{
			Stack* stack_old = *i;
			if(stack_old == stack)
				return false;
		}
		_Stacks.push_back(stack);
		return true;
	}

	void StackHub::Kick(Stack* stack)
	{
		typedef vector<Stack*>::iterator iter;
		for(iter i = _Stacks.begin(); i != _Stacks.end(); i++)
		{
			Stack* stack_old = *i;
			if(stack_old == stack)
			{
				_Stacks.erase(i);
				break;
			}
		}
	}

	void StackHub::KickAll()
	{
		_Stacks.clear();
	}
}