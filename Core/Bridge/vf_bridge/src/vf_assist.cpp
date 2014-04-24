#pragma once

#include "vf_assist.h"

namespace vapula
{
	Uncopiable::Uncopiable() { }

	Uncopiable::~Uncopiable() { }

	Scoped::Scoped(raw ptr, bool isarr)
	{
		_Ptr = ptr;
		_IsArr = isarr;
	}

	Scoped::~Scoped()
	{
		Clear(_Ptr, _IsArr);
	}

	raw Scoped::Get()
	{
		return _Ptr;
	}

	bool Scoped::IsNull()
	{
		return _Ptr == null;
	}

	void Scoped::Ref(raw ptr, bool isarr)
	{
		_Ptr = ptr;
		_IsArr = isarr;
	}

	void Scoped::DeRef()
	{
		_Ptr = null;
	}
}