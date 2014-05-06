#pragma once

#include "vf_base.h"

namespace vapula
{
	class Context;
	class Dataset;
	class Invoker;
	class Error;

	//stack
	class VAPULA_API Stack
	{
	public:
		Stack();
		~Stack();

	private:
		uint32 _StackId;
		pcstr _MethodId;
		bool _HasProtect;
		Context* _Context;
		Dataset* _Dataset;
		Error* _Error;

	public:
		static Stack* GetInstance();

	public:
		//set stack id
		void SetStackId(uint32 id, Invoker* owner);

		//set method id
		void SetMethodId(pcstr id, Invoker* owner);

		//set protect flag
		void SetProtect(bool protect, Invoker* owner);

		//set context
		void SetContext(Context* ctx, Invoker* owner);

		//set dataset
		void SetDataset(Dataset* ds, Invoker* owner);

		//set error
		void SetError(Error* err);

	public:
		//get stack id
		uint32 GetStackId();

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

	//stack hub
	class VAPULA_API StackHub
	{
	private:
		StackHub();
	public:
		~StackHub();

	private:
		static StackHub* _Instance;
	public:
		static StackHub* GetInstance();

	private:
		Lock* _Lock;
		list<Stack*> _Stacks;
	public:
		//get stack by id
		Stack* GetStack(uint32 id);

	public:
		//link stack
		void Link(Stack* stack);

		//kick stack
		void Kick(Stack* stack);

		//kick all stacks
		void KickAll();

		//get count of linked stack
		int GetCount();
	};
}