#include "stdafx.h"
#include "NodeClass.h"

using std::string;

NodeClass::NodeClass()
{
	_Type = NT_Class;
	_ClassId = -1;
}

void NodeClass::SetColor(PCSTR color)
{
	string tmps = color;
	string tmps2 = tmps.substr(tmps.find(',')+1,string::npos);
	tmps = tmps.substr(0,tmps.find(','));
	_ColorR = atoi(tmps.c_str());
	tmps = tmps2.substr(0,tmps.find(','));
	_ColorG = atoi(tmps.c_str());
	tmps2 = tmps2.substr(tmps.find(',')+1,string::npos);
	tmps = tmps2.substr(0,string::npos);
	_ColorB = atoi(tmps.c_str());
}