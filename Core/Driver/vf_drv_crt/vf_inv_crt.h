#pragma once

#include "vf_crt.h"

typedef int (*Delegate)();

class InvokerCRT : public Invoker
{
public:
	InvokerCRT();
	~InvokerCRT();
private:
	Delegate _PtrEntry;

protected:
	void _Entry();
public:
	bool Initialize(Library* lib, int fid);
};