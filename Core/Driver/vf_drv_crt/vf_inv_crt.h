#pragma once

#include "vf_crt.h"

typedef void (*Action)();

class InvokerCRT : public Invoker
{
private:
	Action _EntryProcess;
	Action _EntryRollback;

public:
	InvokerCRT();
	~InvokerCRT();

protected:
	void OnProcess();
	void OnRollback();

public:
	bool Bind(Method* mt);
};