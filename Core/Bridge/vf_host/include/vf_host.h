#pragma once

#include "vf_base.h"
#include "vf_library.h"
#include "vf_invoker.h"

#include "rapidxml/rapidxml.hpp"

using namespace rapidxml;
using namespace vapula;
using std::ostringstream;

//��������ע��ģʽ
enum CtrlInjectMode
{
	VF_HOST_CJ_NULL = 0,
	VF_HOST_CJ_PIPE = 1
};

//��������ֵ
enum HostReturnCode
{
	VF_HOST_RETURN_NORMAL = 0,
	VF_HOST_RETURN_INVALIDCMD = 1,
	VF_HOST_RETURN_INVALIDTASK = 2,
	VF_HOST_RETURN_FAILEXEC = 3
};

str const _vf_host_appname = "Vapula Host";
str const _vf_host_err_0 = "invalid invoke";