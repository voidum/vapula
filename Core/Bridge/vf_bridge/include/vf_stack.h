#pragma once

#include "vf_base.h"

namespace vapula
{
	class Context;
	class Dataset;
	class Task;
	class Error;

	//stack
	class VAPULA_API Stack
	{
	public:
		Stack();
		~Stack();

	private:
		pcstr _MethodId;
		bool _HasProtect;
		Context* _Context;
		Dataset* _Dataset;
		Error* _Error;

	public:
		//get current stack
		static Stack* Instance();
	
	public:
		//set stack id
		void SetStackId(pcstr id, Task* owner);

		//set method id
		void SetMethodId(pcstr id, Task* owner);

		//set protect flag
		void SetProtect(bool protect, Task* owner);

		//set context
		void SetContext(Context* context, Task* owner);

		//set dataset
		void SetDataset(Dataset* dataset, Task* owner);

		//set error
		void SetError(Error* error);

	public:
		//get stack id
		pcstr GetStackId();

		//get method id
		pcstr GetMethodId();

		//get protect flag
		bool HasProtect();

		//get context
		Context* GetContext();

		//get dataset
		Dataset* GetDataset();

		//get error
		Error* GetError();
	};
}