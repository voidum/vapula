#include "stdafx.h"
#include "xhost_app.h"

namespace tcm
{
	namespace xhost
	{
		App::App()
		{
			_RefCount = 1;
		}

		int App::AddRef()
		{
			_RefCount++;
			return _RefCount;
		}

		int App::Release()
		{
			_RefCount--;
			return _RefCount;
		}

		int App::GetRefCt()
		{
			return _RefCount;
		}
	}
}