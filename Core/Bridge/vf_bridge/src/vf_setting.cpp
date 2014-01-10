#include "vf_setting.h"

namespace vapula
{
	Setting* Setting::_Instance = null;

	Setting::Setting()
	{
		_Flag = new Flag();
	}

	Setting::~Setting()
	{
		Clear(_Flag);
	}

	Setting* Setting::GetInstance()
	{
		if(Setting::_Instance == null)
		{
			Lock* lock = Lock::GetCtorLock();
			if(lock->Enter())
			{
				Setting::_Instance = new Setting();
				lock->Leave();
			}
		}
		return Setting::_Instance;
	}

	Flag* Setting::GetFlag()
	{
		return _Flag;
	}

	bool Setting::IsSilent()
	{
		return _Flag->Valid(VF_SETTING_SILENT);
	}

	bool Setting::IsRealTimeMonitor()
	{
		return _Flag->Valid(VF_SETTING_RTMON);
	}
}