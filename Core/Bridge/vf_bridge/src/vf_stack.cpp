#include "vf_stack.h"
#include "vf_context.h"
#include "vf_envelope.h"

namespace vapula
{
	Stack::Stack() 
	{
		_StackId = 0;
	}

	Stack::~Stack()
	{
		Clear(_Context);
		Clear(_Envelope);
	}

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
		_StackId = id;
	}

	pcstr Stack::GetFunctionId()
	{
		return _FunctionId;
	}

	void Stack::SetFunctionId(pcstr id)
	{
		_FunctionId = id;
	}

	Context* Stack::GetContext()
	{
		return _Context;
	}

	void Stack::SetContext(Context* ctx)
	{
		_Context = ctx;
	}

	Envelope* Stack::GetEnvelope()
	{
		return _Envelope;
	}

	void Stack::SetEnvelope(Envelope* env)
	{
		_Envelope = env;
	}

	StackHub* StackHub::_Instance = null;

	StackHub* StackHub::GetInstance()
	{
		Lock* lock = Lock::GetCtorLock();
		lock->Enter();
		if(StackHub::_Instance == null)
			StackHub::_Instance = new StackHub();
		lock->Leave();
		return StackHub::_Instance;
	}

	StackHub::StackHub()
	{
		_Lock = new Lock();
	}

	StackHub::~StackHub()
	{
		KickAll();
		delete _Lock;
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

	void StackHub::Link(Stack* stack)
	{
		typedef vector<Stack*>::iterator iter;
		_Lock->Enter();
		iter i = _Stacks.begin();
		while(i != _Stacks.end())
		{
			if(*i == stack)
				break;
			i++;
		}
		if(i == _Stacks.end())
			_Stacks.push_back(stack);
		_Lock->Leave();
	}

	void StackHub::Kick(Stack* stack)
	{
		typedef vector<Stack*>::iterator iter;
		_Lock->Enter();
		for(iter i = _Stacks.begin(); i != _Stacks.end(); i++)
		{
			if(*i == stack)
			{
				_Stacks.erase(i);
				break;
			}
		}
		_Lock->Leave();
	}

	void StackHub::KickAll()
	{
		_Lock->Enter();
		_Stacks.clear();
		_Lock->Leave();
	}
}