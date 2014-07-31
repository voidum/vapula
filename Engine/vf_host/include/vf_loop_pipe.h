#pragma once

#include "vf_loop.h"
#include "vf_pipe.h"

namespace vapula
{
	class LoopPipe : public Loop
	{
	public:
		LoopPipe();
		~LoopPipe();

	public:
		bool Run();
	};
}