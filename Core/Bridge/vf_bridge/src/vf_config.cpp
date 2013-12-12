#include "vf_config.h"

namespace vapula
{
	Config* Config::_Instance = null;

	Config::Config()
	{
		_Flag = new Flag();
	}

	Config::~Config()
	{
		Clear(_Flag);
	}

	Config* Config::GetInstance()
	{
		if(Config::_Instance == null)
			Config::_Instance = new Config();
		return Config::_Instance;
	}

	Flag* Config::GetFlag()
	{
		return _Flag;
	}

	bool Config::IsSilent()
	{
		return _Flag->Valid(VF_CONFIG_SILENT);
	}

	bool Config::IsRealTimeMonitor()
	{
		return _Flag->Valid(VF_CONFIG_RTMON);
	}
}