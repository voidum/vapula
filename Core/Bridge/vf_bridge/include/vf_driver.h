#pragma once

#include "vf_base.h"

namespace vapula
{
	class Library;
	class Aspect;
	class Invoker;
	class DriverDpt;

	//driver {base}
	class VAPULA_API Driver
	{
	public:
		Driver();
		virtual ~Driver();

	public:
		//get runtime id
		virtual cstr8
			GetRuntimeId() = 0;

		//create library
		virtual Library*
			CreateLibrary() = 0;

		//create aspect
		virtual Aspect*
			CreateAspect() = 0;

		//create invoker
		virtual Invoker*
			CreateInvoker() = 0;
	};

	//driver hub
	class VAPULA_API DriverHub
	{
	private:
		DriverHub();
	public:
		~DriverHub();
	private:
		static DriverHub* _Instance;
	public:
		//get instance of DriverHub
		static DriverHub* GetInstance();
	private:
		vector<DriverDpt*> _DriverDpts;
	private:
		DriverDpt* GetDriverDpt(cstr8 id);
	public:
		//link driver by path
		bool Link(cstr8 path);

		//kick out driver by id
		void Kick(cstr8 id);

		//kick out all drivers
		void KickAll();
	public:
		//get driver by id
		Driver* GetDriver(cstr8 id);

		//get count of linked drivers
		int GetCount();
	};
}