#pragma once

#include "vf_base.h"

namespace vapula
{
	class Library;
	class Invoker;

	//driver {base}
	class VAPULA_API Driver
	{
	public:
		HMODULE _Module;

	public:
		Driver();
		virtual ~Driver();

	public:
		//get runtime id
		virtual pcstr
			GetRuntimeId() = 0;

		//get bin extension
		//e.g. "dll"
		virtual pcstr
			GetBinExt() = 0;

		//create library
		virtual Library*
			CreateLibrary() = 0;

		//create invoker
		virtual Invoker*
			CreateInvoker() = 0;
	};

	//driver hub
	class VAPULA_API DriverHub
	{
	private:
		list<Driver*> _Drivers;

	private:
		static DriverHub* _Instance;

	public:
		//get instance of DriverHub
		static DriverHub* GetInstance();

	private:
		DriverHub();
	public:
		~DriverHub();

	public:
		//link driver by id
		bool Link(pcstr id);

		//kick out driver by id
		void Kick(pcstr id);

		//kick out all drivers
		void KickAll();

	public:
		//get driver by id
		Driver* GetDriver(pcstr id);

		//get count of linked drivers
		int GetCount();
	};
}