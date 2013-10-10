#pragma once

#include "worker_null.h"
#include "worker_pipe.h"
#include "worker_scp_lua.h"

//TCM Host控制注入模式
enum CtrlInjectMode
{
	TCM_HOST_CJ_NULL = 0,
	TCM_HOST_CJ_PIPE = 1,
	TCM_HOST_CJ_SCP_LUA = 2
};