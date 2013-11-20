#pragma once

#include "include/cef_app.h"

namespace tcm
{
	namespace xhost
	{
		class App : 
			public CefApp
		{
		private:
			int _RefCount;
		public:
			App();

			virtual int AddRef();

			virtual int Release();

			virtual int GetRefCt();
		};
	}
}