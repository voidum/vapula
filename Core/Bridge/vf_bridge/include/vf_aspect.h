#pragma once

#include "vf_utility.h"

namespace vapula
{
	//aspect {base}
	class VAPULA_API Aspect
	{
	public:
		Aspect();
		virtual ~Aspect();
	public:
		//get aspect id
		virtual cstr8
			GetAspectId() = 0;

		//if current aspect is singleton
		virtual bool
			IsSingleton() = 0;
	};

	class VAPULA_API AspectInfo
	{
	};

	//aspect hub
	class VAPULA_API AspectHub
	{
	private:
		static AspectHub* _Instance;
	public:
		static AspectHub* GetInstance();
	private:
		vector<cstr8> _Owners;
		vector<Aspect*> _Stacks;
	public:
		Stack* GetStack(uint32 owner);
	};
}