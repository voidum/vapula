#pragma once

#pragma warning(disable:4251)
#pragma warning(disable:4275)

#include "vf_assist.h"
#include "vf_string.h"
#include "vf_debug.h"

namespace vapula
{
	//basic error
	class VAPULA_API Error
	{
	private:
		int _What;
	
	public:
		Error(int what);
		~Error();

	public:
		int What();
	};

	//spin lock
	class VAPULA_API Lock : Uncopiable
	{
	private:
		uint64* _Core; //TRUE - Lock , FALSE - Unlock

	private:
		static Lock* _CtorLock;

	public:
		Lock();
		~Lock();

	public:
		static Lock* GetCtorLock();

	public:
		//capture lock
		void Enter();

		//release lock
		void Leave();
	};

	//can be set only once
	class VAPULA_API Once : Uncopiable
	{
	private:
		object _Data;
		object _Seal;

	public:
		Once();
		~Once();

	public:
		bool CanSet();

		void Set(object data, uint32 size);

		object Get();
	};

	//flag
	class VAPULA_API Flag : Uncopiable
	{
	private:
		Lock* _Lock;
		int _Value;

	public:
		Flag();
		~Flag();

	public:
		void Enable(int flag);
		void Disable(int flag);
		bool Valid(int flag);
	};

	//get type unit
	VAPULA_API uint32 GetTypeUnit(int8 type);

	//generate local unique id
	VAPULA_API pcstr GetLUID(bool logo = false);

	//get Vapula core version
	VAPULA_API pcstr GetVersion();

	//throw error
	VAPULA_API void ThrowError(int what);
}