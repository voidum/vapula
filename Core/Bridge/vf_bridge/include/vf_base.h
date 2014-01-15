#pragma once

#pragma warning(disable:4251)
#pragma warning(disable:4275)

#include "vf_const.h"
#include "vf_string.h"

namespace vapula
{
	//restrict copy
	class Uncopiable
	{
	protected:
		Uncopiable();
		~Uncopiable();
	private:
		Uncopiable(const Uncopiable&);
		Uncopiable& operator=(const Uncopiable&);
	};

	//spin lock
	class VAPULA_API Lock : Uncopiable
	{
	public:
		Lock(uint16 a = 10, uint16 b = 400, uint16 c = 150);
		~Lock();
	private:
		static Lock* _CtorLock;
	public:
		static Lock* GetCtorLock();
	private:
		uint64* _Core; //TRUE - Lock , FALSE - Unlock
		uint16 _A; //1T retry times
		uint16 _B; //max blank times
		uint16 _C; //max blank time
	public:
		//capture lock
		bool Enter();

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

	//clear target
	template<typename T>
	void Clear(T& target, bool isarr = false)
	{
		if(target != null)
		{
			if(isarr) 
				delete [] target;
			else 
				delete target;
			target = null;
		}
	}

	//generate local unique id
	VAPULA_API cstr8 GetLUID(bool logo = false);

	//get Vapula core version
	VAPULA_API cstr8 GetVersion();
}