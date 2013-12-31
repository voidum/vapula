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
		Driver();
		virtual ~Driver();
	public:
		//get runtime id
		virtual cstr8
			GetRuntimeId() = 0;

		//create library
		virtual Library*
			CreateLibrary() = 0;

		//create invoker
		virtual Invoker*
			CreateInvoker() = 0;
	};

	//info agent for driver
	class VAPULA_API DriverInfo
	{
	private:
		cstr8 _Id;
		HMODULE _Handle;
	private:
		Driver* _Driver;
	};

	//hub for driver
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
		vector<DriverInfo*> _DriverInfos;
	public:
		//link driver by id
		bool Link(cstr8 id);

		//kick out driver by id
		bool Kick(cstr8 id);

		//kick out all drivers
		void KickAll();
	public:
		//get driver by id
		Driver* GetDriver(cstr8 id);

		//get count of linked drivers
		int GetCount();
	};
}