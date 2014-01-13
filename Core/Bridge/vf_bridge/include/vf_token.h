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
		object _A;
		uint8 _B;
	public:
		//check if token is lock
		bool IsLock();

		//lock token & get new key
		uint8 Lock();

		//unlock token by key
		void Unlock(uint8 key);
	};
}