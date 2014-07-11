#pragma once

#include "vf_loop.h"

namespace vapula
{
	class LoopFile : public Loop
	{
	public:
		LoopFile();
		~LoopFile();

	public:
		bool Run();
	};
}