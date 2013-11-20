#pragma once

#include "tcm_base.h"

namespace tcm
{
	namespace xhost
	{
		using tcm::strw;

		class Config
		{
		public:
			Config();
			~Config();
		private:
			int _Port;
			strw _AppId;
		private:
			strw _Title;
			bool _LockSize;
			int _Width;
			int _Height;
			int _X;
			int _Y;
		public:
			bool Parse();
		public:
			int Port(int value = -1);
			strw AppId(strw app_id = null);
			strw Title();
			int Width();
			int Height();
			int X();
			int Y();
		};
	}
}