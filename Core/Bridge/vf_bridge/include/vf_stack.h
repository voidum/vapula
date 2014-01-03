#pragma once

#include "vf_utility.h"

namespace vapula
{
	class Context;
	class Envelope;
	class Aspect;

	//stack
	class VAPULA_API Stack
	{
	private:
		Stack();
	public:
		~Stack();
	public:
		static Stack* GetInstance();
	private:
		uint32 _Owner;
		Context* _Context;
		Envelope* _Envelope;
		vector<Aspect*> _Aspects;
	public:
		//get owner
		uint32 GetOwner();

		//get context
		Context* GetContext();

		//set context
		void SetContext(Context* ctx);

		//get envelope
		Envelope* GetEnvelope();

		//set envelope
		void SetEnvelope(Envelope* env);

		//get aspect by id
		Aspect* GetAspect(cstr8 id);
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
		Stack* GetStack();
	};
}