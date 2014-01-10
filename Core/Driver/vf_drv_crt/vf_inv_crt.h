#pragma once

#include "vf_crt.h"

typedef void (*Action)();

class InvokerCRT : public Invoker
{
public:
	InvokerCRT();
	~InvokerCRT();
private:
	Action _Action;

protected:
	void _Entry();
public:
	bool Initialize(Library* lib, int fid);
};