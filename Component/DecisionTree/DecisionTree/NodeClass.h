#include "NodeBase.h"

class NodeClass : public NodeBase
{
public:
	NodeClass();
public:
	int _ClassId;
	BYTE _ColorR;
	BYTE _ColorG;
	BYTE _ColorB;
public:
	void SetColor(PCSTR color);
};