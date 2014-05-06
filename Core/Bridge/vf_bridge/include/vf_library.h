#pragma once

#include "vf_base.h"

namespace vapula
{
	class Driver;
	class Method;
	class Invoker;

	//library {base}
	class VAPULA_API Library
	{
	protected:
		//driver
		Driver* _Driver;

		//library bin path
		pcstr _Path;

		//library id
		pcstr _Id; 

		//methods
		list<Method*> _Methods;

	protected:
		Library();
	public:
		virtual ~Library();

	public:
		//load library by path
		static Library* Load(pcstr path);

	protected:
		void ClearAll();

	public:
		//get driver
		Driver* GetDriver();

		//get library id
		pcstr GetLibraryId();

		//get method by id
		Method* GetMethod(pcstr id);

		//create invoker by id
		Invoker* CreateInvoker(pcstr id);

	public:
		//mount library
		virtual bool Mount() = 0;

		//unmount library
		virtual void Unmount() = 0;
	};
}