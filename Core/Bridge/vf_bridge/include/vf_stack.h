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
		int32 _FunctionId;
		Context* _Context;
		Envelope* _Envelope;
	public:
		static Stack* GetInstance();
	public:
		//get stack id
		uint32 GetStackId();

		//{TI2} set stack id
		void SetStackId(uint32 id);

		//get function id
		int32 GetFunctionId();

		//{TI2} set function id
		void SetFunctionId(int32 id);

		//get context
		Context* GetContext();

		//{TI2} set context
		void SetContext(Context* ctx);

		//get envelope
		Envelope* GetEnvelope();

		//{TI2} set envelope
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
		Lock* _Lock;
		vector<Stack*> _Stacks;
	public:
		//get stack by id
		Stack* GetStack(uint32 id);
	public:
		//link stack
		bool Link(Stack* stack);

		//kick stack
		void Kick(Stack* stack);

		//kick all stacks
		void KickAll();

		//get count of linked stack
		int GetCount();
	};
}