#pragma once

#include "vf_base.h"

namespace vapula
{
	enum SettingItem
	{
		VF_SETTING_SILENT = 1,
		VF_SETTING_RTMON = 2
	};

	//setting for Vapula
	class VAPULA_API Setting : Uncopiable
	{
	private:
		Setting();
	public:
		~Setting();
	private:
		static Setting* _Instance;
	public:
		static Setting* GetInstance();
	private:
		Flag* _Flag;
	public:
		Flag* GetFlag();
	public:
		bool IsSilent();
		bool IsRealTimeMonitor();
	};
}