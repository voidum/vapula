#pragma once

#include "vf_base.h"

namespace vapula
{
	class Context;
	class Dataset;
	class Task;
	class Error;
	class StackHub;

	//stack
	class VAPULA_API Stack
	{
	private:
		static StackHub* _Hub;
		static StackHub* Hub();

	public:
		Stack();
		~Stack();

	private:
		int _StackId;
		pcstr _MethodId;
		bool _HasProtect;
		Context* _Context;
		Dataset* _Dataset;
		Error* _Error;

	public:
		//get current id
		static int CurrentId();

		//get current stack
		static Stack* Instance();

	public:
		//get stack id
		int GetStackId();

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

	public:
		//link stack into hub
		void LinkHub();

		//kick out stack from hub
		void KickHub();
	};
}