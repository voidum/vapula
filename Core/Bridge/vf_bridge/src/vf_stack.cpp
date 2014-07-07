#include "vf_stack.h"
#include "vf_stack_hub.h"
#include "vf_task.h"
#include "vf_context.h"
#include "vf_dataset.h"
#include "vf_error.h"

namespace vapula
{
	StackHub* Stack::_Hub = null;

	StackHub* Stack::Hub()
	{
		if (_Hub == null)
		{
			Lock* lock = Lock::GetCtorLock();
			lock->Enter();
			if (_Hub == null)
				_Hub = new StackHub();
			lock->Leave();
		}
		return _Hub;
	}

	Stack::Stack() 
	{
		_MethodId = null;
		_HasProtect = false;
		_Context = null;
		_Dataset = null;
		_Error = null;
	}

	Stack::~Stack()
	{
		Clear(_MethodId);
		Clear(_Context);
		Clear(_Dataset);
		Clear(_Error);
	}

	int Stack::CurrentId()
	{
		return GetCurrentThreadId();
	}

	Stack* Stack::Instance()
	{
		StackHub* hub = Stack::Hub();
		return hub->Find(CurrentId());
	}

	int Stack::GetStackId()
	{
		return _StackId;
	}

	pcstr Stack::GetMethodId()
	{
		return _MethodId;
	}

	void Stack::SetMethodId(pcstr id, Task* owner)
	{
		if(owner->GetStack() == this)
			_MethodId = id;
	}

	bool Stack::HasProtect()
	{
		return _HasProtect;
	}

	void Stack::SetProtect(bool protect, Task* owner)
	{
		if(owner->GetStack() == this)
			_HasProtect = protect;
	}

	Context* Stack::GetContext()
	{
		return _Context;
	}

	void Stack::SetContext(Context* context, Task* owner)
	{
		if(owner->GetStack() == this)
			_Context = context;
	}

	Dataset* Stack::GetDataset()
	{
		return _Dataset;
	}

	void Stack::SetDataset(Dataset* dataset, Task* owner)
	{
		if(owner->GetStack() == this)
			_Dataset = dataset;
	}

	Error* Stack::GetError()
	{
		return _Error;
	}

	void Stack::SetError(Error* error)
	{
		Clear(_Error);
		_Error = error;
	}

	void Stack::LinkHub()
	{
		_StackId = CurrentId();
		StackHub* hub = Stack::Hub();
		hub->Link(this);
	}

	void Stack::KickHub()
	{
		StackHub* hub = Stack::Hub();
		hub->Kick(this);
	}
}