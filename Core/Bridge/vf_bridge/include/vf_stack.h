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
		int _FunctionId;
		Context* _Context;
		Envelope* _Envelope;
	public:
		//get function id
		int GetFunctionId();

		//set function id
		void SetFunctionId(int fid);

		//get context
		Context* GetContext();

		//set context
		void SetContext(Context* ctx);

		//get envelope
		Envelope* GetEnvelope();

		//set envelope
		void SetEnvelope(Envelope* env);
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
		vector<Stack*> _Stacks;
	public:
		//link stack
		bool Link(Stack* stack);

		//kick stack by id
		void Kick(Stack* stack);

		//kick all stacks
		void KickAll();

		//get count of allocated stack
		int GetCount();
	};
}