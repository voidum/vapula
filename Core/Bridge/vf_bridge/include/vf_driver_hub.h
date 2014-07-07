#pragma once

#include "vf_base.h"

namespace vapula
{
	class Driver;

	class DriverHub : Uncopiable
	{
	public:
		DriverHub();
		~DriverHub();

	private:
		Lock* _Lock;
		list<Driver*> _Drivers;

	public:
		list<Driver*>& GetInnerData();

	public:
		int Count();

		Driver* Find(pcstr id);

		void Link(Driver* driver);

		void Kick(pcstr id);

		void KickAll();
	};
}