#include "vf_stack.h"
#include "vf_runtime.h"
#include "vf_task.h"
#include "vf_context.h"
#include "vf_dataset.h"
#include "vf_error.h"

namespace vapula
{
	Stack::Stack() 
	{
		_StackId = null;
		_MethodId = null;
		_HasProtect = false;
		_Context = null;
		_Dataset = null;
		_Error = null;
	}

	Stack::~Stack()
	{
		Clear(_StackId);
		Clear(_MethodId);
		Clear(_Context);
		Clear(_Dataset);
		Clear(_Error);
	}

	pcstr Stack::CurrentId()
	{
		return str::Value(GetCurrentThreadId());
	}

	Stack* Stack::Instance()
	{
		Runtime* runtime = Runtime::Instance();
		pcstr id = CurrentId();
		Stack* stack = (Stack*)runtime->SelectObject(VF_CORE_STACK, id);
		return stack;
	}

	pcstr Stack::GetStackId()
	{
		return _StackId;
	}

	void Stack::SetStackId(pcstr id, Task* owner)
	{
		if (owner->GetStack() == this)
		{
			Clear(_StackId);
			_StackId = id;
		}
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

	uint8 Stack::GetType()
	{
		return VF_CORE_STACK;
	}

	pcstr Stack::GetCoreId()
	{
		return GetStackId();
	}
}