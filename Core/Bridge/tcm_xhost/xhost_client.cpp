#include "stdafx.h"
#include "xhost_client.h"

namespace tcm
{
	namespace xhost
	{
		Client::Client()
		{
			_RefCount = 1;
		}

		int Client::AddRef()
		{
			_RefCount++;
			return _RefCount;
		}

		int Client::Release()
		{
			_RefCount--;
			return _RefCount;
		}

		int Client::GetRefCt()
		{
			return _RefCount;
		}
	}
}