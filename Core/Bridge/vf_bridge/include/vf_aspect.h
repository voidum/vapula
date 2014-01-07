#pragma once

#include "vf_utility.h"

namespace vapula
{
	//aspect
	class VAPULA_API Aspect
	{
	public:
		Aspect();
		virtual ~Aspect();
	protected:
		//aspect path
		cstr8 _Path; 

		//aspect id
		cstr8 _Id; 

		//pointcut symbol
		cstr8 _PointcutSym;

	public:
		//get aspect id
		cstr8 GetAspectId();
	};

	//aspect hub
	class VAPULA_API AspectHub
	{
	private:
		static AspectHub* _Instance;
	public:
		static AspectHub* GetInstance();
	private:
		vector<Aspect*> _Aspects;
	public:
		//load aspect by path
		bool Load(cstr8 path);

		//unload aspect by id
		void Unload(cstr8 id);

		//unload all aspects
		void UnloadAll();
	public:
		//get aspect by id
		Aspect* GetAspect(cstr8 id);

		//get count of loaded aspects
		int GetCount();
	};
}