#pragma once

#include "vf_assist.h"

namespace vapula
{
	Uncopiable::Uncopiable() { }

	Uncopiable::~Uncopiable() { }

	Handle::Handle(object ptr, bool isarr)
	{
		_Ptr = ptr;
		_IsArr = isarr;
	}

	Handle::~Handle()
	{
		Clear(_Ptr, _IsArr);
	}

	object Handle::Get()
	{
		return _Ptr;
	}

	bool Handle::IsNull()
	{
		return _Ptr == null;
	}

	void Handle::Ref(object ptr, bool isarr)
	{
		_Ptr = ptr;
		_IsArr = isarr;
	}

	void Handle::DeRef()
	{
		_Ptr = null;
	}
}