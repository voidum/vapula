#include "stdafx.h"
#include "tcmmain.h"
#include "decitree.h"

extern "C"
{
#include "lua.h"
#include "lauxlib.h"  
#include "lualib.h"
};

UINT Run(int function, Envelope* envelope, Context* context)
{
	if(function != 0) return TCM_RETURNCODE_NOFUNCTION;

	PCWSTR file_decitree = envelope->Read<PCWSTR>(0);
	PCWSTR file_datasrc = envelope->Read<PCWSTR>(1);
	
	vector<NodeBase*>* tree = new vector<NodeBase*>();

	NodeBase* treeroot = BuildDeciTree(file_decitree, tree);
	DataSource* ds = LoadDataSource(file_datasrc);
	lua_State* L = (lua_State*)PreCompile(tree,ds);
	if(L == NULL) return TCM_RETURNCODE_ERROR;

	ds->BeginAction();
	ExecDeciTree(L, treeroot, ds, context);
	OutputHeader(tree, ds);
	ds->EndAction();
	Clear(tree);
	
	return TCM_RETURNCODE_NORMAL;
}