#pragma once

#include "vf_base.h"

namespace vapula
{
	class Task;

	//aspect
	class VAPULA_API Aspect
	{
	private:
		pcstr _Id;
		pcstr _Contact;
		pcstr _LibraryId;
		pcstr _MethodId;
		bool _Async;

	private:
		Task* _Task;

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

		//get invoker
		Task* GetTask();

		//test if frame match with contact
		bool TryMatch(pcstr frame);
	};
}