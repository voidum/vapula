#pragma once

#include "vf_base.h"

namespace vapula
{
	class Aspect;

	//weaver (hub for aspect)
	class VAPULA_API Weaver
	{
	private:
		list<Aspect*> _Aspects;

	private:
		static Weaver* _Instance;

	public:
		//get instance of weaver
		static Weaver* GetInstance();

	private:
		Weaver();
	public:
		~Weaver();

	public:
		//link aspect
		void Link(Aspect* aspect);

		//kick out aspect by id
		void Kick(pcstr id);

		//kick out all aspects
		void KickAll();

	public:
		//get aspect by id
		Aspect* GetAspect(pcstr id);

		//get count of linked aspects
		int GetCount();

	public:
		//reach frame
		void Reach(pcstr frame);

	private:
		//invoke aspect
		void Invoke(Aspect* aspect);

		//wait for aspect
		void Wait(Aspect* aspect);
	};
}