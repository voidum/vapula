#pragma once

#pragma warning(disable:4251)
#pragma warning(disable:4275)

#include "vf_const.h"
#include "vf_string.h"
#include "vf_uncopiable.hpp"
#include "vf_debug.h"

namespace vapula
{
	//clear target
	template<typename T>
	void Clear(T& target, bool isarr = false)
	{
		typedef char need_complete_type[sizeof(T) ? 1 : -1];
		(void) sizeof(need_complete_type);
		if(target != null)
		{
			if(isarr)
				delete [] target;
			else 
				delete target;
			target = null;
		}
	}

	//spin lock
	class VAPULA_API Lock : Uncopiable
	{
	public:
		Lock();
		~Lock();
	private:
		static Lock* _CtorLock;
	public:
		static Lock* GetCtorLock();
	private:
		uint64* _Core; //TRUE - Lock , FALSE - Unlock
	public:
		//capture lock
		void Enter();

		//release lock
		void Leave();
	};

	//assign only once
	class VAPULA_API Once : Uncopiable
	{
	public:
		Once();
		~Once();
	private:
		object _Value;
		object _Seal;
	public:
		bool CanSet();

		void Set(object value, uint32 size);

		object Get();
	};

	//flag
	class VAPULA_API Flag : Uncopiable
	{
	public:
		Flag();
		~Flag();
	private:
		Lock* _Lock;
		int _Value;
	public:
		void Enable(int flag);
		void Disable(int flag);
		bool Valid(int flag);
	};

	//get type unit
	VAPULA_API uint32 GetTypeUnit(int8 type);

	//generate local unique id
	VAPULA_API cstr8 GetLUID(bool logo = false);

	//get Vapula core version
	VAPULA_API cstr8 GetVersion();
}