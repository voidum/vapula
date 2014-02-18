#pragma once

#include "vf_base.h"

namespace vapula
{
	class Library;

	//aspect
	class VAPULA_API Aspect
	{
	private:
		pcstr _Pattern;
		Library* _Library;
	public:
		Aspect();
		~Aspect();
	public:
		static Aspect* Load(pcstr path);
	public:
		Library* GetLibrary();
		pcstr GetPattern();
	};
}