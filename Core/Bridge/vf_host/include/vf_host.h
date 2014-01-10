#pragma once

#include "vf_utility.h"
#include "vf_library.h"
#include "vf_invoker.h"

#include "rapidxml/rapidxml.hpp"

using namespace rapidxml;
using namespace vapula;
using std::ostringstream;

//control injection mode
enum CtrlInjectMode
{
	VF_HOST_CJ_NULL = 0,
	VF_HOST_CJ_PIPE = 1
};

//host return code
enum HostReturnCode
{
	VF_HOST_RETURN_NORMAL = 0,
	VF_HOST_RETURN_INVALIDCMD = 1,
	VF_HOST_RETURN_INVALIDTASK = 2,
	VF_HOST_RETURN_FAILEXEC = 3
};

cstr8 const _vf_host = "Vapula Host";
cstr8 const _vf_host_err_0 = "invalid invoke";