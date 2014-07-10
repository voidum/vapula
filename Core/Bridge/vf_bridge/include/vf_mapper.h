#pragma once

#include "vf_base.h"

namespace vapula
{
	class Record;

	class VAPULA_API Mapper
	{
	public:
		void Map(Record* from, Record* to);
	};
}