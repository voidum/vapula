#pragma once

#include "vf_host.h"

namespace vapula
{
	class Plan;
	class Task;
	class Stack;

	//loop {base}
	class Loop
	{
	public:
		Loop();
		virtual ~Loop();

	public:
		//load loop by path
		static Loop* Load(pcstr path);

	public:
		//run loop
		virtual bool Run() = 0;
	};
}