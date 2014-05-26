#pragma once 

#include "vf_base.h"

namespace vapula
{
	class VAPULA_API Core
	{
	public:
		virtual uint8 GetType() = 0;
		virtual pcstr GetCoreId() = 0;
	};
}