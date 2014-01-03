#pragma once

#include "vf_utility.h"

namespace vapula
{
	//aspect {base}
	class VAPULA_API AspectDpt
	{
	public:
		AspectDpt();
		virtual ~AspectDpt();
	protected:
		//driver
		Driver* _Driver;

		//aspect directory
		cstr8 _Dir; 

		//aspect id
		cstr8 _Id; 

		//entry descriptor
		cstr8 _EntryDpt;

	public:
		//get driver
		Driver* GetDriver();

		//get runtime id
		cstr8 GetRuntimeId();

		//get binary extension
		cstr8 GetBinExt();

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
		vector<AspectDpt*> _AspectDpts;
	private:
		AspectDpt* GetAspectDpt(cstr8 id);
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