#pragma once

#include "vf_base.h"

namespace vapula
{
	enum SettingItem
	{
		VF_SETTING_SILENT = 1,
		VF_SETTING_RTMON = 2
	};

	//settings for Vapula
	class VAPULA_API Settings : Uncopiable
	{
	private:
		Settings();
	public:
		~Settings();
	private:
		static Settings* _Instance;
	public:
		static Settings* GetInstance();
	private:
		Flag* _Flag;
	public:
		bool IsSilent();
		bool IsRealTimeMonitor();
	};
}