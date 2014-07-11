#pragma once

#include "vf_base.h"

namespace vapula
{
	enum Settings
	{
		VF_SETTING_SILENT = 1,
		VF_SETTING_REALTIME = 2,
		VF_SETTING_LOG = 4
	};

	//runtime setting
	class VAPULA_API Setting : Uncopiable
	{
	private:
		Setting();
	public:
		~Setting();
	private:
		static Setting* _Instance;
	public:
		static Setting* Instance();
	private:
		Flag* _Flag;
	public:
		Flag* GetFlag();
	public:
		bool IsSilent();
		bool IsRealTime();
		bool HasLog();
	};
}