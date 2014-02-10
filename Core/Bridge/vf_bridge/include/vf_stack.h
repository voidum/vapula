#pragma once

#include "vf_utility.h"

namespace vapula
{
	class Context;
	class Envelope;

	//stack
	class VAPULA_API Stack
	{
	public:
		Stack();
		~Stack();

	private:
		uint32 _StackId;
		pcstr _FunctionId;
		Context* _Context;
		Envelope* _Envelope;
		Error* _Error;

	public:
		static Stack* GetInstance();

	public:
		//set stack id
		void SetStackId(uint32 id);

		//set function id
		void SetFunctionId(pcstr id);

		//set context
		void SetContext(Context* ctx);

		//set envelope
		void SetEnvelope(Envelope* env);

		//set error
		void SetError(Error* err);

	public:
		//get stack id
		uint32 GetStackId();

		//get function id
		pcstr GetFunctionId();

		//get context
		Context* GetContext();

		//get envelope
		Envelope* GetEnvelope();

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
		vector<Stack*> _Stacks;
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