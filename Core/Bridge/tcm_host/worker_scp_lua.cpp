#include "stdafx.h"
#include "worker_scp_lua.h"

#include "tcm_executor.h"
#include "tcm_xml.h"
#include "tcm_config.h"

extern "C"
{
#include "lua.h"
#include "lauxlib.h"  
#include "lualib.h"
};

using namespace rapidxml;

Worker_SCP_LUA::Worker_SCP_LUA()
{
	_L = luaL_newstate();
}

Worker_SCP_LUA::~Worker_SCP_LUA()
{
	lua_close(_L);
}

void Worker_SCP_LUA::LuaSetData(PCSTR tbname, PCSTR key, PCSTR value)
{
	lua_getglobal(_L, tbname);
	lua_pushstring(_L, key);
	lua_pushstring(_L, value);
	lua_settable(_L, -3);
}

bool Worker_SCP_LUA::RunStageA()
{
	Config* config = Config::GetInstance();
	Flag* flag = config->GetFlag();

	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Executor* exec = task->GetExecutor();

	xml_node<>* xe_ctrl = (xml_node<>*)xml::Parse(task->GetCtrlConfig());
	PCSTR path = xml::ValueA(xe_ctrl->first_node("path"));

	luaL_openlibs(_L);
	if(luaL_loadfile(_L, path))
	{
		if(!flag->Valid(TCM_CONFIG_SILENT))
		{
			ShowMsgbox(L"Fail to load script file.", L"TCM Host");
			return false;
		}
	}
	if(lua_pcall(_L, 0, 0, 0))
	{
		if(!flag->Valid(TCM_CONFIG_SILENT))
		{
			ShowMsgbox(L"Fail to compile script file.", L"TCM Host");
			return false;
		}
	}

	lua_newtable(_L);
	lua_setglobal(_L, "tcm_param");
	lua_newtable(_L);
	lua_setglobal(_L, "tcm_tag");
	lua_newtable(_L);
	lua_setglobal(_L, "tcm_debug");

	Envelope* env = exec->GetEnvelope();
	Dictionary* tags = task->GetTags();
	for(int i=0;i<tags->GetCount();i++)
	{
		PCSTR key = WcToMb(tags->GetKey(i));
		PCSTR value = WcToMb(tags->GetValue(i));
		LuaSetData("tcm_tag",key,value);
		delete key;
		delete value;
	}
	xml_node<>* paramexp = (xml_node<>*)xml::Path(xe_ctrl, 2, "export", "param");
	while(paramexp)
	{
		PCSTR key = xml::ValueA(paramexp->first_attribute("key"));
		PCSTR value = env->CastReadA(xml::ValueInt(paramexp->first_attribute("id")));
		LuaSetData("tcm_param", key, value);
		paramexp = paramexp->next_sibling();
		delete key;
		delete value;
	}
	LuaSetData("tcm_debug","exception","");

	lua_getglobal(_L, "TcmBeforeRun");
	lua_pcall(_L, 0, 1, 0);
	int cancel = lua_tonumber(_L, -1);
	if(cancel != 0) return false;
	return true;
}

bool Worker_SCP_LUA::RunStageB()
{
	Config* config = Config::GetInstance();
	Flag* flag = config->GetFlag();

	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Executor* exec = task->GetExecutor();

	int freq_monitor = flag->Valid(TCM_CONFIG_RTMON) ? 5 : 50;
	exec->Start();
	Context* ctx = exec->GetContext();
	while(ctx->GetState() != TCM_STATE_IDLE)
	{
		lua_getglobal(_L, "TcmRunning");
		lua_pushnumber(_L, ctx->GetState());
		lua_pushnumber(_L, ctx->GetProgress());
		lua_pcall(_L, 2, 1, 0);
		int ctrl = lua_tonumber(_L, -1);
		switch(ctrl)
		{
		case TCM_CTRL_CANCEL:
			exec->Stop(30000);
			break;
		case TCM_CTRL_PAUSE:
			exec->Pause(30000);
		case TCM_CTRL_RESUME:
			exec->Resume();
			break;
		}
		Sleep(freq_monitor);
	}
	return true;
}

bool Worker_SCP_LUA::RunStageC()
{
	Config* config = Config::GetInstance();
	Flag* flag = config->GetFlag();

	TaskEx* task = dynamic_cast<TaskEx*>(_Task);
	Executor* exec = task->GetExecutor();
	Context* ctx = exec->GetContext();

	xml_node<>* xe_ctrl = (xml_node<>*)xml::Parse(task->GetCtrlConfig());
	PCSTR path = xml::ValueA(xe_ctrl->first_node("path"));

	xml_node<>* paramexp = (xml_node<>*)xml::Path(xe_ctrl, 2, "export", "param");
	Envelope* env = exec->GetEnvelope();
	while(paramexp)
	{
		PCSTR key = xml::ValueA(paramexp->first_attribute("key"));
		PCSTR value = env->CastReadA(xml::ValueInt(paramexp->first_attribute("id")));
		LuaSetData("tcm_param", key, value);
		paramexp = paramexp->next_sibling();
		delete key;
		delete value;
	}

	//PCSTR exception = "";
	//LuaSetData("tcm_debug","exception",exception == NULL ? "" : exception);

	lua_getglobal(_L, "TcmAfterRun");
	int retcode = ctx->GetReturnCode();
	lua_pushnumber(_L, retcode);
	lua_pcall(_L, 1, 0, 0);
	return true;
}