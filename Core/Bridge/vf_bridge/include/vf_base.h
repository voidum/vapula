#pragma once

#pragma warning(disable:4251)
#pragma warning(disable:4275)

#include "vf_assist.h"
#include "vf_core.h"
#include "vf_string.h"

namespace vapula
{
	//spin lock
	class VAPULA_API Lock : Uncopiable
	{
	private:
		//TRUE - Lock , FALSE - Unlock
		uint64* _Core;

	public:
		Lock();
		~Lock();

	public:
		//capture lock
		void Enter();

		//release lock
		void Leave();

	private:
		static Lock* _CtorLock;

	public:
		//get lock for ctor
		static Lock* GetCtorLock();
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
		//enable flag
		void Enable(int flag);

		//disable flag
		void Disable(int flag);

		//valid flag
		bool Valid(int flag);
	};

	//show value by simple message box
	template<typename T>
	VAPULA_API void ShowMsgbox(T value)
	{
		ShowMsgbox(str::Value(value), _vf_bridge);
	}

	//show string by simple message box
	VAPULA_API void ShowMsgbox(pcstr value, pcstr caption);

	//get path directory
	VAPULA_API pcstr GetPathDir(pcstr path, bool file = false);

	//test if file can be opened as read
	VAPULA_API bool TryOpenRead(pcstr file);

	//wait for a moment by ms
	VAPULA_API void WaitSpan(uint32 wait);
}