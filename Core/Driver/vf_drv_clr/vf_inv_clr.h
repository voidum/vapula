#pragma once

#include "vf_clr.h"

class InvokerCLR : public Invoker
{
public:
	InvokerCLR();
	~InvokerCLR();
private:
	pcstr _Handle;
public:
	pcstr GetHandle();

protected:
	void _Entry();
public:
	bool Initialize(Function* func);
};