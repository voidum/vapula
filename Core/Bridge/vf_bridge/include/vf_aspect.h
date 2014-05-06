#pragma once

#include "vf_base.h"

namespace vapula
{
	class Invoker;

	//aspect
	class VAPULA_API Aspect
	{
	private:
		pcstr _Id;
		pcstr _Contact;
		bool _Async;
		Library* _Library;
		Invoker* _Invoker;
	
	private:
		Invoker* _Invoker;

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

		//get library
		pcstr GetLibrary();

		//get method
		pcstr GetMethod();

		//get invoker
		Invoker* GetInvoker();

		//test if frame match with contact
		bool TryMatch(pcstr frame);
	};
}