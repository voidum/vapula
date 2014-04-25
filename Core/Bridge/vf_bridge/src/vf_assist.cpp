#pragma once

#include "vf_assist.h"

namespace vapula
{
	Uncopiable::Uncopiable() { }

	Uncopiable::~Uncopiable() { }

	Scoped::Scoped(raw ptr)
	{
		_Ptr = ptr;
	}

	Scoped::~Scoped()
	{
		Clear(_Ptr);
	}

	raw Scoped::Get()
	{
		return _Ptr;
	}

	bool Scoped::IsNull()
	{
		return _Ptr == null;
	}

	void Scoped::Ref(raw ptr)
	{
		_Ptr = ptr;
	}

	void Scoped::DeRef()
	{
		_Ptr = null;
	}
}