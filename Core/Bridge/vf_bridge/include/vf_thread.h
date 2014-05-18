#pragma once

#include "vf_base.h"

namespace vapula
{
	class Task;

	enum ThreadRole
	{
		VF_THREAD_HEAD = 0,
		VF_THREAD_BACK = 1,
		VF_THREAD_DEAD = 2
	};

	class Thread
	{
	public:
		Thread();
		~Thread();

	private:
		HANDLE _Handle;
		bool _IsSuspend;
		uint8 _Role;
		Task* _Task;

	private:
		static uint32 WINAPI Entry();

	public:
		uint8 GetRole();
		void SetRole(uint8 role);

	public:
		void Suspend();
	};
}