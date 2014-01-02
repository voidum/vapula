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

	//aspect hub
	class VAPULA_API AspectHub
	{
	private:
		static AspectHub* _Instance;
	public:
		static AspectHub* GetInstance();
	private:
		vector<AspectDpt*> _AspectDpts;
	private:
		AspectDpt* GetAspectDpt(cstr8 id);
	public:
		//link aspect by path
		bool Link(cstr8 path);

		//kick out aspect by id
		void Kick(cstr8 id);

		//kick out all aspects
		void KickAll();
	public:
		//get aspect by id
		Aspect* GetAspect(cstr8 id);

		//get count of linked aspects
		int GetCount();
	};
}