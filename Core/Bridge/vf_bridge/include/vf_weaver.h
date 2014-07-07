#pragma once

#include "vf_base.h"

namespace vapula
{
	class Aspect;
	class Task;
	
	//weaver
	class Weaver
	{
	private:
		static Weaver* _Instance;

	public:
		//get instance of weaver
		static Weaver* Instance();

	private:
		Weaver();
	public:
		~Weaver();

	private:
		//invoke aspect
		Task* Invoke(Aspect* aspect);

	public:
		//raise reach frame event
		void OnReachFrame(pcstr frame);
	};
}