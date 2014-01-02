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
		vector<Aspect> _Aspects;
	public:
		//get owner of stack
		uint32 GetOwner();

		//get context in stack
		Context* GetContext();

		//get envelope in stack
		Envelope* GetEnvelope();

		//get aspect in stack by key
		Aspect* GetAspect(cstr8 key);
	};

	//stack hub
	class VAPULA_API StackHub
	{
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