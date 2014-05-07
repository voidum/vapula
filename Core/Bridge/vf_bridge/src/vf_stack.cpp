#include "vf_stack.h"
#include "vf_runtime.h"
#include "vf_invoker.h"
#include "vf_context.h"
#include "vf_dataset.h"
#include "vf_error.h"

namespace vapula
{
	Stack::Stack() 
	{
		_StackId = 0;
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

	uint32 Stack::CurrentId()
	{
		return GetCurrentThreadId();
	}

	Stack* Stack::Instance()
	{
		Runtime* runtime = Runtime::Instance();
		uint32 id = CurrentId();
		Stack* stack = runtime->GetStack(id);
		return stack;
	}

	uint32 Stack::GetStackId()
	{
		return _StackId;
	}

	void Stack::SetStackId(uint32 id, Invoker* owner)
	{
		if(owner->GetStack() == this)
			_StackId = id;
	}

	pcstr Stack::GetMethodId()
	{
		return _MethodId;
	}

	void Stack::SetMethodId(pcstr id, Invoker* owner)
	{
		if(owner->GetStack() == this)
			_MethodId = id;
	}

	bool Stack::HasProtect()
	{
		return _HasProtect;
	}

	void Stack::SetProtect(bool protect, Invoker* owner)
	{
		if(owner->GetStack() == this)
			_HasProtect = protect;
	}

	Context* Stack::GetContext()
	{
		return _Context;
	}

	void Stack::SetContext(Context* ctx, Invoker* owner)
	{
		if(owner->GetStack() == this)
			_Context = ctx;
	}

	Dataset* Stack::GetDataset()
	{
		return _Dataset;
	}

	void Stack::SetDataset(Dataset* ds, Invoker* owner)
	{
		if(owner->GetStack() == this)
			_Dataset = ds;
	}

	Error* Stack::GetError()
	{
		return _Error;
	}

	void Stack::SetError(Error* err)
	{
		Clear(_Error);
		_Error = err;
	}
}