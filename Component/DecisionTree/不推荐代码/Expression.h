#pragma once

#include "tcm_base.h"
#include "tcm_xml.h"

class DataSource;
class ExprBase;
class RelExpr;
class LgcExpr;
class CplxExpr;

typedef ExprBase* PExprBase;
typedef RelExpr* PRelExpr;

using std::vector;

const double Accuracy = 0.0001;

enum ExprType
{
	ET_Rel = 0,
	ET_Lgc = 1,
	ET_Cplx = 2
};

enum OperRelation
{
	OpRel_Equal = 0,
	OpRel_NotEqual = 1,
	OpRel_Greater = 2,
	OpRel_GreaterEqual = 3,
	OpRel_Less = 4,
	OpRel_LessEqual = 5
};

enum OperLogic
{
	OpLgc_And = 0,
	OpLgc_Or = 1,
	OpLgc_Xor = 2
};

class ExprBase
{
public:
	ExprBase(){}
	~ExprBase(){}
public:
	int _Type;
	int _Operation;
	bool _Result;
public:
	virtual bool Compile(LPVOID param) = 0;
	virtual bool Test(LPVOID param) = 0;
	virtual bool IfPushed() = 0;
};

class LgcExpr : public ExprBase
{
public:
	LgcExpr();
	~LgcExpr();
public:
	ExprBase* _LVar;
	ExprBase* _RVar;
	bool _Pushed;
private:
	bool Compile(LPVOID param);
	bool Test(LPVOID param);
	inline bool IfPushed() { return _Pushed; }
};

class CplxExpr : public ExprBase
{
public:
	CplxExpr();
	~CplxExpr();
public:
	PCSTR _ExprString;
	vector<RelExpr*> _AtomExprs;
	PRelExpr* _AtomExprArr;
	ExprBase* _ExprRoot;
private:
	PExprBase* _ExprStack;
private:
	bool _ValidExpr();
	bool _TransExpr();
	void _ClearCache(vector<ExprBase*>& lst);
public:
	RelExpr* GetAtomExpr(int id);
public:
	bool Compile(LPVOID param);
	bool Test(LPVOID param);
	inline bool IfPushed() { return true; }
public:
	static CplxExpr* Parse(rapidxml::xml_node<>* xml);
};

class RelExpr : public ExprBase
{
public:
	RelExpr();
	~RelExpr();
public:
	int _Id;
	PCSTR _LVarStr;
	PCSTR _RVarStr;
private:
	int _LVarId;
	int _RVarId;
	double _LVar;
	double _RVar;
public:
	bool Compile(LPVOID param);
	bool Test(LPVOID param);
	inline bool IfPushed() { return true; }
public:
	static RelExpr* Parse(rapidxml::xml_node<>* xml);
};