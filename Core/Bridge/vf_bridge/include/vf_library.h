#pragma once

#include "vf_base.h"

namespace vapula
{
	class Driver;
	class Method;
	class Task;
	class LibraryHub;

	//library {base}
	class VAPULA_API Library
	{
	private:
		static LibraryHub* _Hub;
		static LibraryHub* Hub();

	public:
		static Library* Find(pcstr id);
		static int Count();

	protected:
		//library id
		pcstr _LibraryId;

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

	public:
		//link library into hub
		void LinkHub();

		//kick out library from hub
		void KickHub();
	};
}