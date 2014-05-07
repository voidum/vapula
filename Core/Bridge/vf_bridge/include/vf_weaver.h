#pragma once

#include "vf_base.h"

namespace vapula
{
	class Aspect;

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

	public:
		//invoke aspect
		void Invoke(Aspect* aspect);

		//wait for aspect
		void Wait(Aspect* aspect);
	};
}