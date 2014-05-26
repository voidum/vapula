#pragma once

#include "vf_base.h"

namespace vapula
{
	class Driver;
	class Method;
	class Task;

	//library {base}
	class VAPULA_API Library : public Core
	{
	protected:
		pcstr _Id;

		//driver
		Driver* _Driver;

		//library bin path
		pcstr _Path;

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
		//get library id
		pcstr GetLibraryId();

		//get driver
		Driver* GetDriver();

		//get method by id
		Method* GetMethod(pcstr id);

		//create task by id
		Task* CreateTask(pcstr id);

	public:
		//mount library
		virtual bool Mount() = 0;

		//unmount library
		virtual void Unmount() = 0;

	public:
		uint8 GetType();

		pcstr GetCoreId();
	};
}