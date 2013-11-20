#include "stdafx.h"
#include "xhost_config.h"

namespace tcm
{
	namespace xhost
	{
		using namespace tcm;

		Config::Config()
		{
			_Port = 80;
			_AppId = null;
			_Title = null;
			_LockSize = false;
			_Width = 600;
			_Height = 400;
			_X = -1;
			_Y = -1;
		}

		Config::~Config()
		{
			Clear(_AppId);
			Clear(_Title);
		}

		bool Config::Parse()
		{
			return true;
		}

		int Config::Port(int value)
		{
			if(value >= 0)
				_Port = value;
			return _Port;
		}
	
		strw Config::AppId(strw app_id)
		{
			if(app_id != null)
			{
				Clear(_AppId);
				_AppId = CopyStrW(app_id);
			}
			return _AppId;
		}

		strw Config::Title()
		{
			return _Title;
		}

		int Config::Width()
		{
			return _Width;
		}

		int Config::Height()
		{
			return _Height;
		}

		int Config::X()
		{
			return _X;
		}

		int Config::Y()
		{
			return _Y;
		}
	}
}