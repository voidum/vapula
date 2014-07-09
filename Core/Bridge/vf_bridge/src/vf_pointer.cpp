#include "vf_pointer.h"

namespace vapula
{
	Pointer::Pointer()
	{
		_Data = null;
		_Size = 0;
	}

	Pointer::~Pointer()
	{
		//do nothing
	}

	void Pointer::Capture(raw data, uint32 size)
	{
		_Data = data;
		_Size = size;
	}

	void Pointer::Release()
	{
		_Data = null;
		_Size = 0;
	}
}