#pragma once

#include "vf_base.h"

namespace vapula
{
	//operation token
	class VAPULA_API Token : Uncopiable
	{
	public:
		Token();
		~Token();
	private:
		Lock* _Lock;
		object _A;
		uint8 _B;
	public:
		//check if token is on
		bool IsOff();

		//turn off token & get new key
		void Off(uint8& key);

		//turn on token by key
		void On(uint8 key);
	};
}