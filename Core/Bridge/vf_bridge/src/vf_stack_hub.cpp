#include "vf_stack_hub.h"
#include "vf_stack.h"

namespace vapula
{
	StackHub::StackHub()
	{
		_Lock = new Lock();
	}

	StackHub::~StackHub()
	{
		KickAll();
		Clear(_Lock);
	}

	list<Stack*>& StackHub::GetInnerData()
	{
		return _Stacks;
	}

	int StackHub::Count()
	{
		return _Stacks.size();
	}

	Stack* StackHub::Find(int id)
	{
		typedef list<Stack*>::iterator iter;
		_Lock->Enter();
		Stack* stack = null;
		for (iter i = _Stacks.begin(); i != _Stacks.end(); i++)
		{
			Stack* tmp = *i;
			if (tmp->GetStackId() == id)
			{
				stack = tmp;
				break;
			}
		}
		_Lock->Leave();
		return stack;
	}

	void StackHub::Link(Stack* stack)
	{
		typedef list<Stack*>::iterator iter;
		_Lock->Enter();
		int id = stack->GetStackId();
		for (iter i = _Stacks.begin(); i != _Stacks.end(); i++)
		{
			Stack* tmp = *i;
			if (tmp->GetStackId() == id)
			{
				_Lock->Leave();
				return;
			}
		}
		_Stacks.push_back(stack);
		_Lock->Leave();
	}

	void StackHub::Kick(Stack* stack)
	{
		typedef list<Stack*>::iterator iter;
		_Lock->Enter();
		for (iter i = _Stacks.begin(); i != _Stacks.end(); i++)
		{
			Stack* tmp = *i;
			if (tmp == stack)
			{
				_Stacks.erase(i);
				break;
			}
		}
		_Lock->Leave();
	}

	void StackHub::KickAll()
	{
		typedef list<Stack*>::iterator iter;
		_Lock->Enter();
		for (iter i = _Stacks.begin(); i != _Stacks.end(); i++)
			Clear(*i);
		_Stacks.clear();
		_Lock->Leave();
	}
}