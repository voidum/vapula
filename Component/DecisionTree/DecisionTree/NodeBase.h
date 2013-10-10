#pragma once

#include "tcm_base.h"
#include "tcm_xml.h"

enum NodeType
{
	NT_Class = 0,
	NT_Judge = 1
};

class NodeBase
{
public:
	NodeBase(){}
	~NodeBase();
public:
	int _Id;
	PCSTR _Name;
	UINT _Type;
public:
	static NodeBase* Parse(rapidxml::xml_node<>* xml);
public:
	virtual NodeBase* GetNodeLeft() { return NULL; }
	virtual NodeBase* GetNodeRight() { return NULL; }
};

typedef NodeBase* PNodeBase;