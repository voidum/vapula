#pragma once

#include "vf_base.h"

namespace vapula
{
	class Driver;
	class Method;
	class Task;

	//library {base}
	class VAPULA_API Library
	{
	protected:
		//library id
		pcstr _Id;

		//library bin path
		pcstr _Path;

		//methods
		list<Method*> _Methods;

		//driver
		Driver* _Driver;

	protected:
		Library();
	public:
		virtual ~Library();

	public:
		//load library by path
		static Library* Load(pcstr path);

	public:
		//get library id
		pcstr GetLibraryId();

		//get method by id
		Method* GetMethod(pcstr id);

		//get driver
		Driver* GetDriver();

	public:
		//create task by method id
		Task* CreateTask(pcstr id);

	public:
		//mount library
		virtual bool Mount() = 0;

		//unmount library
		virtual void Unmount() = 0;
	};
}