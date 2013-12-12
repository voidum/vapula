#pragma once

#include "vf_candy.h"

namespace vapula
{
	enum ConfigItem
	{
		VF_CONFIG_SILENT = 1,
		VF_CONFIG_RTMON = 2
	};

	class VAPULA_API Config : Uncopiable
	{
	private:
		Config();
	public:
		~Config();
	private:
		static Config* _Instance;
	public:
		static Config* GetInstance();
	private:
		Flag* _Flag;
		int _test;
	public:
		Flag* GetFlag();
		int GetTest() { return _test; }
	public:
		bool IsSilent();
		bool IsRealTimeMonitor();
	};
}