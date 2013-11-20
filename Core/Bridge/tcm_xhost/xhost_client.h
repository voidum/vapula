#pragma once

#include "include/cef_client.h"

namespace tcm
{
	namespace xhost
	{
		class Client : 
			public CefClient
		{
		private:
			int _RefCount;
		public:
			Client();

			virtual int AddRef();

			virtual int Release();

			virtual int GetRefCt();
		};
	}
}