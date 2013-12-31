#pragma once

#include "vf_utility.h"

namespace vapula
{
	using std::map;

	class Context;
	class Envelope;

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
		Context* _Context;
		Envelope* _Envelope;
		vector<Aspect> _Aspects;
	public:
		Context* GetContext();
		Envelope* GetEnvelope();
	public:
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
		vector<uint32> _Owners;
		vector<Stack*> _Stacks;
	public:
		Stack* GetStack(uint32 owner);
	};
}