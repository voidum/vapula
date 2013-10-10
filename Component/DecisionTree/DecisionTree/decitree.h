#include "tcm_cdev.h"
#include "NodeClass.h"
#include "NodeJudge.h"
#include "DataSource.h"
#include <fstream>
#include <vector>

using std::vector;

//获取节点
NodeBase* GetNodeById(vector<NodeBase*> nodes,int id);

//设置Lua变量值
void LuaSetData(LPVOID luastate, PCSTR key, double value);

//构建决策树
NodeBase* BuildDeciTree(PCWSTR file,vector<NodeBase*>* tree);

//加载数据源
DataSource* LoadDataSource(PCWSTR file);

//预编译决策树
LPVOID PreCompile(vector<NodeBase*>* tree,DataSource* ds);

//执行决策树
void ExecDeciTree(LPVOID luastate, NodeBase* treeroot,DataSource* dsrc,tcm::Context* ctx);

//输出类别信息
void OutputHeader(vector<NodeBase*>* tree, DataSource* ds);

//清理
void Clear(vector<NodeBase*>* tree);

//原子分类
BYTE Classify(NodeBase* treeroot,LPVOID luastate);

//数据重排
void RecombineData(PDataSet* dsrc,LPVOID luastate,int total,int index);