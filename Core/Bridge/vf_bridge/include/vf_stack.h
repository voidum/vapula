#pragma once

#include "vf_candy.h"

namespace vapula
{
	class Context;
	class Envelope;

	//∂‘œÛ’ª
	class VAPULA_API Stack
	{
	private:
		Stack();
	public:
		~Stack();
	private:
		static Stack* _Instance;
	public:
		static Stack* GetInstance();
	private:
		vector<cstr8> _Ids;
		vector<object> _Contexts;
		vector<object> _Envelopes;
	public:
		Context* GetContext();
		Envelope* GetEnvelope();
	};
}