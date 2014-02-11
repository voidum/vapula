#pragma once

#include "vf_clr.h"

class InvokerCLR : public Invoker
{
private:
	pcstr _Handle;

public:
	InvokerCLR();
	~InvokerCLR();

protected:
	void OnProcess();
	void OnRollback();

public:
	pcstr GetHandle();
	bool Bind(Method* mt);
};