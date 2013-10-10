#include "stdafx.h"
#include "Expression.h"
#include "DataSource.h"
#include <stack>

using std::cout;
using std::endl;
using std::string;
using std::stack;
using namespace tcm;
using rapidxml::xml_node;

CplxExpr::CplxExpr()
{
	_Type = ET_Cplx;
	_ExprRoot = NULL;
}

CplxExpr::~CplxExpr()
{
	if(_ExprString != NULL) delete _ExprString;
	for(vector<RelExpr*>::iterator iter=_AtomExprs.begin();iter!=_AtomExprs.end();iter++)
	{
		RelExpr* expr = *iter;
		if(expr != NULL) delete expr;
	}
	_AtomExprs.clear();
	//clear the tree
	ExprBase* expr = _ExprRoot;
	stack<ExprBase*> stk;
	stk.push(expr);
	while (!stk.empty())
	{
		expr = stk.top();
		stk.pop();
		if (expr->IfPushed())
			delete expr;
		else
		{
			stk.push(expr);
			if(expr->_Type == ET_Lgc)
			{
				LgcExpr* tmpexpr = (LgcExpr*)expr;
				tmpexpr->_Pushed = false;
				if(tmpexpr->_LVar != NULL) stk.push(tmpexpr->_LVar);
				if(tmpexpr->_RVar != NULL) stk.push(tmpexpr->_RVar);
			}
		}
	}

	if(_AtomExprArr != NULL) delete [] _AtomExprArr;
	if(_ExprStack != NULL) delete [] _ExprStack;
}

CplxExpr* CplxExpr::Parse(xml_node<>* xml)
{
	CplxExpr* expr = new CplxExpr();
	expr->_ExprString = GetXBaseValueA(xml->first_node("cplx"));
	xml_node<>* xnode = (xml_node<>*)GetXElemByPath(xml,2,"atoms","atom");
	while(xnode)
	{
		RelExpr* atomexpr = RelExpr::Parse(xnode);
		expr->_AtomExprs.push_back(atomexpr);
		xnode = xnode->next_sibling();
	}
	return expr;
}

bool CplxExpr::_ValidExpr()
{
	int count = strlen(_ExprString) + 1;
	int state = -1; // -1[null],0[0-9],1[^&|],2[(],3[)]
	for(int i=0; i<count; i++)
	{
		char tmp = _ExprString[i];
		if(tmp == '&' || tmp == '^' || tmp == '|')
		{
			if(state == -1 || state == 1 || state == 2) return false;
			else state = 1;
		}
		else if(tmp == '(')
		{
			if(state == 0 || state == 3) return false;
			else state = 2;
		}
		else if(tmp == ')')
		{
			if(state == -1 || state == 1 || state == 2) return false;
			else state = 3;
		}
		else if(tmp >= '0' && tmp <= '9')
		{
			if(state == 3) return false;
			else state = 0;
		}
		else if(tmp == 0)
		{
			if(state == 1 || state == 2) return false;
			else state = -1;
		}
		else return false;
	}
	return true;
}
bool CplxExpr::_TransExpr()
{
	int count = strlen(_ExprString) + 1;
	int state = -1; // -1[null],0[0-9],1[^&|],2[(],3[)]
	int estate = 0; // 0[wait for lvar],1[wait for rvar],2[wait for oper]
	int brk = 0;

	string var;
	ExprBase* expr = NULL;
	stack<ExprBase*> stk;
	vector<ExprBase*> lst;

	for(int i=0; i<count; i++)
	{
		char tmp = _ExprString[i];
		if(tmp == '&' || tmp == '^' || tmp == '|')
		{
			if(state == 0) //变量完成，出现运算符
			{
				if(expr != NULL) //存在表达式
				{
					//填充右值
					LgcExpr* expr1 = dynamic_cast<LgcExpr*>(expr); 
					int id = atoi(var.c_str());
					expr1->_RVar = GetAtomExpr(id);
					//构造表达式
				}
				else //查询左值
				{
					int id = atoi(var.c_str());
					expr = GetAtomExpr(id);
				}
				LgcExpr* expr1 = new LgcExpr();
				expr1->_LVar = expr;
				switch(tmp)
				{
					case '&':expr1->_Operation = OpLgc_And;break;
					case '|':expr1->_Operation = OpLgc_Or;break;
					case '^':expr1->_Operation = OpLgc_Xor;break;
				}
				//更新状态
				expr = expr1;
			}
			else if(state == 3) //右括号结束，出现运算符
			{
				//构造逻辑式
				LgcExpr* expr1 = new LgcExpr();
				expr1->_LVar = expr;
				switch(tmp)
				{
					case '&':expr1->_Operation = OpLgc_And;break;
					case '|':expr1->_Operation = OpLgc_Or;break;
					case '^':expr1->_Operation = OpLgc_Xor;break;
				}

				//更新状态
				expr = expr1;
			}
			state = 1;
			continue;
		}
		else if(tmp == '(')
		{
			if(state == -1) //由左括号起始
			{
			}
			if(state == 1) //运算符完成，出现左括号
			{
			}
			if(state == 2) //左括号完成，出现左括号
			{
			}
			brk++;
			state = 2;
			continue;
		}
		else if(tmp == ')')
		{
			if(brk > 0) brk--;
			else { _ClearCache(lst);return false; }
			if(state == 0) //变量完成，出现右括号
			{
				int id = atoi(var.c_str());
				if(expr != NULL) //存在表达式
				{
					//填充右值
					LgcExpr* expr1 = dynamic_cast<LgcExpr*>(expr); 
					expr1->_RVar = GetAtomExpr(id);
				}
				else
				{
					//构造比较式
					expr = GetAtomExpr(id);
				}
				state = 3;
			}
			if(state == 3) //右括号完成，出现右括号
			{
			}
			continue;
		}
		else if(tmp >= '0' && tmp <= '9') //ok
		{
			//由变量开始、运算符完成或左括号完成
			if(state == -1 || state == 1 || state == 2) 
			{
				var = tmp;
				state = 0;
			}
			//变量完成，出现变量，追加
			else if(state == 0) { var += tmp; } 
			continue;
		}
		else if(tmp == 0)
		{
			if(state == 0) //变量完成，终止
			{
				int id = atoi(var.c_str());
				if(expr != NULL) //存在表达式
				{
					//填充右值
					LgcExpr* expr1 = dynamic_cast<LgcExpr*>(expr); 
					expr1->_RVar = GetAtomExpr(id);
				}
				else
				{
					expr = GetAtomExpr(id);
				}
			}
			if(state == 3) //右括号完成，终止
			{
			}
			state = -1;
		}
	}
	_ExprRoot = expr;
	return true;
}

void CplxExpr::_ClearCache(vector<ExprBase*>& lst)
{
	for (vector<ExprBase*>::iterator iter=lst.begin();iter!=lst.end();iter++)
	{
		ExprBase* expr = *iter;
		if(expr != NULL) delete expr;
	}
	lst.clear();
}

bool CplxExpr::Compile(LPVOID param)
{
	bool ret = _ValidExpr();
	cout<<" - {Compile} : filter = "<<(ret ? "ok" : "failed")<<endl;

	ret = _TransExpr();
	cout<<" - {Compile} : translator = "<<(ret ? "ok" : "failed")<<endl;

	_AtomExprArr = new PRelExpr[_AtomExprs.size()];
	int i = 0;
	for (vector<RelExpr*>::iterator iter=_AtomExprs.begin();iter!=_AtomExprs.end();iter++)
	{
		RelExpr* expr = *iter;
		expr->Compile(param);
		_AtomExprArr[i++] = expr;
	}
	cout<<" - {Compile} : optimizer (convert vector)"<<endl;

	ExprBase* expr = _ExprRoot;
	int stk_lvlmax = 0;
	stack<ExprBase*> stk;
	stk.push(expr);
	while (!stk.empty())
	{
		expr = stk.top();
		stk.pop();
		if (expr->IfPushed())
		{
			if(expr->_Type == ET_Lgc)
				((LgcExpr*)expr)->_Pushed = false;
		}
		else
		{
			stk.push(expr);
			if(expr->_Type == ET_Lgc)
			{
				LgcExpr* tmpexpr = (LgcExpr*)expr;
				tmpexpr->_Pushed = true;
				if(tmpexpr->_LVar != NULL) stk.push(tmpexpr->_LVar);
				if(tmpexpr->_RVar != NULL) stk.push(tmpexpr->_RVar);
			}
		}
		if(stk.size() > stk_lvlmax) stk_lvlmax = stk.size();
	}
	_ExprStack = new PExprBase[stk_lvlmax];
	memset(_ExprStack, NULL, stk_lvlmax*sizeof(LPVOID));
	cout<<" - {Compile} : optimizer (convert tree-stack)"<<endl;

	return ret;
}

bool CplxExpr::Test(LPVOID param)
{
	for (int i=0;i<_AtomExprs.size();i++) _AtomExprArr[i]->Test(param);

	ExprBase* expr = _ExprRoot;
	int pos = -1;
	_ExprStack[++pos] = expr;
	while (pos > -1)
	{
		expr = _ExprStack[pos];
		pos--;
		if (expr->IfPushed())
		{
			switch(expr->_Type)
			{
			case ET_Rel:
				_Result = expr->_Result;
				break;
			case ET_Lgc:
				_Result = expr->Test(NULL);
				((LgcExpr*)expr)->_Pushed = false; //refresh stack state
				break;
			}
		}
		else
		{
			_ExprStack[++pos] = expr;
			if(expr->_Type == ET_Lgc)
			{
				LgcExpr* tmpexpr = (LgcExpr*)expr;
				tmpexpr->_Pushed = true;
				if(tmpexpr->_LVar != NULL) _ExprStack[++pos] = tmpexpr->_LVar;
				if(tmpexpr->_RVar != NULL) _ExprStack[++pos] = tmpexpr->_RVar;
			}
		}
	}
	return _Result;
}

RelExpr* CplxExpr::GetAtomExpr(int id)
{
	for (vector<RelExpr*>::iterator iter=_AtomExprs.begin();iter!=_AtomExprs.end();iter++)
	{
		RelExpr* expr = *iter;
		if(expr->_Id == id) return expr;
	}
	return NULL;
}

LgcExpr::LgcExpr()
{
	_Type = ET_Lgc;
	_Pushed = false;
	_LVar = NULL;
	_RVar = NULL;
}

LgcExpr::~LgcExpr()
{
}

bool LgcExpr::Compile(LPVOID param)
{
	return false;
}

bool LgcExpr::Test(LPVOID param)
{
	switch(_Operation)
	{
	case OpLgc_And:
		_Result = _LVar->_Result & _RVar->_Result;
		break;
	case OpLgc_Or:
		_Result = _LVar->_Result | _RVar->_Result;
		break;
	case OpLgc_Xor:
		_Result = _LVar->_Result ^ _RVar->_Result;
		break;
	default:
		_Result = false;
		break;
	}
	return _Result;
}

RelExpr::RelExpr()
{
	_Type = ET_Rel;
	_LVarId = -1;
	_RVarId = -1;
}

RelExpr::~RelExpr()
{
	if(_LVar != NULL) delete _LVarStr;
	if(_LVar != NULL) delete _RVarStr;
}

RelExpr* RelExpr::Parse(xml_node<>* xml)
{
	RelExpr* expr = new RelExpr();

	PCSTR tmpa = GetXBaseValueA(xml->first_attribute("id"));
	int tmpv = atoi(tmpa);
	delete tmpa;
	expr->_Id = tmpv;

	tmpa = GetXBaseValueA(xml->first_attribute("logic"));
	tmpv = atoi(tmpa);
	delete tmpa;
	expr->_Operation = tmpv;

	expr->_LVarStr = GetXBaseValueA(xml->first_attribute("lvar"));
	expr->_RVarStr = GetXBaseValueA(xml->first_attribute("rvar"));
	return expr;
}

bool RelExpr::Compile(LPVOID param)
{
	DataSource* dsrc = (DataSource*)param;
	char tmp = _LVarStr[0];
	if(tmp >= '0' && tmp <= '9')
	{
		double tmpv = atof(_LVarStr);
		_LVar = tmpv;
		_LVarId = -1;
	}
	else
	{
		DataSet* ds = dsrc->GetDataSet(_LVarStr);
		_LVarId = ds->_Id;
	}
	tmp = _RVarStr[0];
	if(tmp >= '0' && tmp <= '9')
	{
		double tmpv = atof(_RVarStr);
		_RVar = tmpv;
		_RVarId = -1;
	}
	else
	{
		DataSet* ds = dsrc->GetDataSet(_RVarStr);
		_RVarId = ds->_Id;
	}
	return true;
}

bool RelExpr::Test(LPVOID param)
{
	double* data = (double*)param;
	double lvar = _LVarId >= 0 ? data[_LVarId] : _LVar;
	double rvar = _RVarId >= 0 ? data[_RVarId] : _RVar;
	switch(_Operation)
	{
	case OpRel_Equal:
		_Result = (abs(lvar-rvar) <= Accuracy);
		break;
	case OpRel_NotEqual:
		_Result = (abs(lvar-rvar) > Accuracy);
		break;
	case OpRel_Greater:
		_Result = (lvar > rvar);
		break;
	case OpRel_GreaterEqual:
		_Result = (lvar > rvar || abs(lvar-rvar) <= Accuracy);
		break;
	case OpRel_Less:
		_Result = (lvar < rvar);
		break;
	case OpRel_LessEqual:
		_Result = (lvar < rvar || abs(lvar-rvar) <= Accuracy);
		break;
	default:
		_Result = false;
		break;
	}
	return _Result;
}