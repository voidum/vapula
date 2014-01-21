#pragma once

#include "vf_base.h"

namespace vapula
{
	class Invoker;
	class Driver;

	class Function;

	//library {base}
	class VAPULA_API Library
	{
	protected:
		Library();
	public:
		static Library* Load(cstr8 path);
	public:
		virtual ~Library();

	protected:
		//driver
		Driver* _Driver;

		//library bin path
		cstr8 _Path;

		//library id
		cstr8 _Id; 

		//functions
		vector<Function*> _Functions;

	protected:
		void ClearAll();

	public:
		//get driver
		Driver* GetDriver();

		//get library id
		cstr8 GetLibraryId();

		//get function by id
		Function* GetFunction(cstr8 id);

		//create invoker by id
		Invoker* CreateInvoker(cstr8 id);

	public:
		//mount library
		virtual bool Mount() = 0;

		//unmount library
		virtual void Unmount() = 0;
	};
}