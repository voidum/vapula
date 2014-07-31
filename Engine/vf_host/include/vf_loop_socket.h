#pragma once

#include "vf_loop.h"

namespace vapula
{
	class LoopSocket : public Loop
	{
	public:
		LoopSocket();
		~LoopSocket();

	public:
		bool Run();
	};
}