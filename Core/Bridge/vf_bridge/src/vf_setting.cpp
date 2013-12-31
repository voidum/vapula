#include "vf_setting.h"

namespace vapula
{
	Settings* Settings::_Instance = null;

	Settings::Settings()
	{
		_Flag = new Flag();
	}

	Settings::~Settings()
	{
		Clear(_Flag);
	}

	Settings* Settings::GetInstance()
	{
		if(Settings::_Instance == null)
		{
			Lock* lock = Lock::GetCtorLock();
			if(lock->Enter())
			{
				Settings::_Instance = new Settings();
				lock->Leave();
			}
		}
		return Settings::_Instance;
	}

	bool Settings::IsSilent()
	{
		return _Flag->Valid(VF_SETTING_SILENT);
	}

	bool Settings::IsRealTimeMonitor()
	{
		return _Flag->Valid(VF_SETTING_RTMON);
	}
}