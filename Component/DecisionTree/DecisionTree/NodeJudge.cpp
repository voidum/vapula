#include "stdafx.h"
#include "NodeJudge.h"

extern "C"
{
#include "lua.h"
#include "lauxlib.h"  
#include "lualib.h"
};

using std::string;
using namespace tcm;

NodeJudge::NodeJudge()
{
	_Type = NT_Judge;
}

NodeJudge::~NodeJudge()
{
	if(_Condition != NULL) delete _Condition;
}

PCSTR NodeJudge::Compile()
{
	string tmps = "func";
	tmps += ValueToStrA(_Id);
	_LuaFuncName = CopyStrA(tmps.c_str());
	tmps = "function ";
	tmps += _LuaFuncName;
	tmps += "()\n if ";
	tmps += _Condition;
	tmps += " then\n return 1\n else\n return -1\n end\nend\n\n";
	return CopyStrA(tmps.c_str());
}

int NodeJudge::Test(LPVOID luastate)
{
	lua_State* L = (lua_State*)luastate;
	lua_getglobal(L, _LuaFuncName);
	lua_pcall(L, 0, 1, 0);
	int ret = (int)lua_tonumber(L, -1);
	int ntop = lua_gettop(L);
	for(int i=0;i<ntop;i++) lua_pop(L , 1);
	return ret;
}