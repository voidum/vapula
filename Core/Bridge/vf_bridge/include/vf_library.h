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
		static Library* Load(pcstr path);
	public:
		virtual ~Library();

	protected:
		//driver
		Driver* _Driver;

		//library bin path
		pcstr _Path;

		//library id
		pcstr _Id; 

		//functions
		vector<Function*> _Functions;

	protected:
		void ClearAll();

	public:
		//get driver
		Driver* GetDriver();

		//get library id
		pcstr GetLibraryId();

		//get function by id
		Function* GetFunction(pcstr id);

		//create invoker by id
		Invoker* CreateInvoker(pcstr id);

	public:
		//mount library
		virtual bool Mount() = 0;

		//unmount library
		virtual void Unmount() = 0;
	};
}