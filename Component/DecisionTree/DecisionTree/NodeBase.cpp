#include "stdafx.h"
#include "NodeClass.h"
#include "NodeJudge.h"

using namespace tcm;
using rapidxml::xml_node;

NodeBase::~NodeBase()
{
	if(_Name != NULL) delete _Name;
}

NodeBase* NodeBase::Parse(xml_node<>* xml)
{
	NodeBase* node = NULL;
	NodeJudge* nj = NULL;
	NodeClass* nc = NULL;
	PCSTR tmpa = GetXBaseValueA(xml->first_attribute("type"));
	int tmpv = atoi(tmpa);
	delete tmpa;
	switch(tmpv)
	{
	case NT_Class:
		node = new NodeClass();
		nc = dynamic_cast<NodeClass*>(node);
		tmpa = GetXBaseValueA(xml->first_node("color"));
		nc->SetColor(tmpa);
		delete tmpa;
		break;
	case NT_Judge:
		node = new NodeJudge();
		nj = dynamic_cast<NodeJudge*>(node);
		nj->_Condition = GetXBaseValueA(xml->first_node("expr"));
		break;
	}
	node->_Name = GetXBaseValueA(xml->first_node("name"));
	tmpa = GetXBaseValueA(xml->first_attribute("id"));
	tmpv = atoi(tmpa);
	delete tmpa;
	node->_Id = tmpv;
	return node;
}