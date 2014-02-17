#pragma once

#include "vf_library.h"
#include "vf_invoker.h"

using namespace vapula;

//control mode
enum CtrlMode
{
	VFH_CTRL_NULL = 0,
	VFH_CTRL_PIPE = 1
};

//host return code
enum HostReturnCode
{
	VFH_RETURN_NORMAL = 0,
	VFH_RETURN_INVALIDCMD = 1,
	VFH_RETURN_INVALIDTASK = 2,
	VFH_RETURN_FAILEXEC = 3
};

pcstr const _vf_host = "Vapula Host";
pcstr const _vf_host_version = "2.0.4.1";