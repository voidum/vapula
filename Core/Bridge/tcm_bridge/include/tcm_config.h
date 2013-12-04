#pragma once

#include "tcm_candy.h"

namespace tcm
{
	enum ConfigItem
	{
		TCM_CONFIG_SILENT = 1,
		TCM_CONFIG_RTMON = 2
	};

	class TCM_BRIDGE_API Config : Uncopiable
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