#pragma once

#include "vf_utility.h"

namespace vapula
{
	//advice
	class VAPULA_API Advice
	{
	public:
		Advice();
		virtual ~Advice();
	protected:
		//advice path
		cstr8 _Path; 

		//advice id
		cstr8 _Id; 

	public:
		//get advice id
		cstr8 GetAdviceId();
	};

	//advice hub
	class VAPULA_API AdviceHub
	{
	private:
		static AdviceHub* _Instance;
	public:
		static AdviceHub* GetInstance();
	private:
		vector<Advice*> _Advices;
	public:
		//load advice by path
		bool Load(cstr8 path);

		//unload advice by id
		void Unload(cstr8 id);

		//unload all advices
		void UnloadAll();
	public:
		//get advice by id
		Advice* GetAdvice(cstr8 id);

		//get count of loaded advices
		int GetCount();
	};
}