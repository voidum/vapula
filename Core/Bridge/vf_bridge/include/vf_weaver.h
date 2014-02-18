#pragma once

#include "vf_base.h"

namespace vapula
{
	class Aspect;

	class VAPULA_API Weaver
	{
	private:
		list<Aspect*> _Aspects;

	private:
		static Weaver* _Instance;
	public:
		static Weaver* GetInstance();

	public:
		void Link(Aspect* aspect);
		void Kick(Aspect* aspect);
	
	public:
		void Reach(pcstr frame, int mode);
	};
}