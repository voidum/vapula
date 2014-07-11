#include "vf_setting.h"

namespace vapula
{
	Setting* Setting::_Instance = null;

	Setting::Setting()
	{
		_Flag = new Flag();
		_Flag->Disable(VF_SETTING_SILENT);
		_Flag->Disable(VF_SETTING_REALTIME);
		_Flag->Disable(VF_SETTING_LOG);
	}

	Setting::~Setting()
	{
		Clear(_Flag);
	}

	Setting* Setting::Instance()
	{
		if (Setting::_Instance == null)
		{
			Lock* lock = Lock::GetCtorLock();
			lock->Enter();
			if (Setting::_Instance == null)
				Setting::_Instance = new Setting();
			lock->Leave();
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

	bool Setting::IsRealTime()
	{
		return _Flag->Valid(VF_SETTING_REALTIME);
	}

	bool Setting::HasLog()
	{
		return _Flag->Valid(VF_SETTING_LOG);
	}
}