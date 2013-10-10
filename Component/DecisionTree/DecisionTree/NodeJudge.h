#include "NodeBase.h"

class NodeJudge : public NodeBase
{
public:
	NodeJudge();
	~NodeJudge();
public:
	PCSTR _Condition;
	NodeBase* _NodeYes;
	NodeBase* _NodeNo;
private:
	PCSTR _LuaFuncName;
public:
	PCSTR Compile();
	int Test(LPVOID luastate);
};