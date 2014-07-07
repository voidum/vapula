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

		void Add(Driver* driver);

		void Remove(Driver* driver);

		void RemoveAll();
	};
}