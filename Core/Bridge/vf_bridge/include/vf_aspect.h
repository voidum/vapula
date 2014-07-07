#pragma once

#include "vf_base.h"

namespace vapula
{
	class Task;
	class AspectHub;
	class Weaver;

	//aspect
	class VAPULA_API Aspect
	{
	friend class Weaver;

	private:
		static AspectHub* _Hub;
		static AspectHub* Hub();

	public:
		static Aspect* Find(pcstr id);
		static int Count();

	private:
		pcstr _AspectId;
		pcstr _Contact;
		pcstr _LibraryId;
		pcstr _MethodId;
		bool _Async;

	public:
		Aspect();
		~Aspect();

	public:
		//load aspect by path
		static Aspect* Load(pcstr path);

	public:
		//get aspect id
		pcstr GetAspectId();

		//get async mode
		bool IsAsync();

		//get key frame regex pattern
		pcstr GetContact();

		//get library id
		pcstr GetLibraryId();

		//get method id
		pcstr GetMethodId();

	public:
		//create task
		Task* CreateTask();

		//test if frame match with contact
		bool TryMatch(pcstr frame);

	public:
		//add aspect into hub
		void LinkHub();

		//remove aspect from hub
		void KickHub();
	};
}