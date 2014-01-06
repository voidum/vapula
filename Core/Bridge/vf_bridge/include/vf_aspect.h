#pragma once

#include "vf_utility.h"

namespace vapula
{
	class Driver;

	//aspect {base}
	class VAPULA_API Aspect
	{
	public:
		Aspect();
		virtual ~Aspect();
	protected:
		//driver
		Driver* _Driver;

		//aspect path
		cstr8 _Path; 

		//aspect id
		cstr8 _Id; 

		//entry symbol
		cstr8 _EntrySym;

	public:
		//get driver
		Driver* GetDriver();

		//get runtime id
		cstr8 GetRuntimeId();

		//get aspect id
		cstr8 GetAspectId();

	public:
		//mount aspect
		virtual bool Mount() = 0;

		//unmount aspect
		virtual void Unmount() = 0;
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