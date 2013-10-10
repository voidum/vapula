#pragma once

#include "task.h"

using namespace tcm;

struct lua_State;

class Worker_SCP_LUA : public Worker
{
public:
	Worker_SCP_LUA();
	~Worker_SCP_LUA();
private:
	lua_State* _L;
private:
	void LuaSetData(PCSTR tbname, PCSTR key, PCSTR value);
public:
	bool RunStageA();
	bool RunStageB();
	bool RunStageC();
};