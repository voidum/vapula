#pragma once

#include "vf_dev_invoker.h"

namespace vapula
{
	pcstr const _vf_host = "Vapula Host";
	pcstr const _vf_host_version = "2.0.5.0";
	
	enum HostReturnCode
	{
		VFH_RETURN_NORMAL = 0,
		VFH_RETURN_ERROR = 1
	};
}