#pragma once

#include "vf_crt.h"

typedef int (*Delegate)(int, Envelope*, Context*);

class InvokerCRT : public Invoker
{
public:
	InvokerCRT();
	~InvokerCRT();
private:
	Delegate _Entry;
protected:
	uint32 WINAPI _ThreadProc();
public:
	bool Initialize(Library* lib, int fid);
};