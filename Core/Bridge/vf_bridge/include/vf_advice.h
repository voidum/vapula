#pragma once

#include "vf_base.h"

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
		pcstr _Path; 

		//advice id
		pcstr _Id; 

	public:
		//get advice id
		pcstr GetAdviceId();
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
		bool Load(pcstr path);

		//unload advice by id
		void Unload(pcstr id);

		//unload all advices
		void UnloadAll();
	public:
		//get advice by id
		Advice* GetAdvice(pcstr id);

		//get count of loaded advices
		int GetCount();
	};
}