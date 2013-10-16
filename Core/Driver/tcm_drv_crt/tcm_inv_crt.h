#pragma once

#include "tcm_Invoker.h"

using namespace tcm;

typedef int (*Delegate)(int, Envelope*, Context*);

class InvokerCRT : public Invoker
{
public:
	InvokerCRT();
	~InvokerCRT();
private:
	Delegate _Entry;
protected:
	UINT WINAPI _ThreadProc();
public:
	bool Initialize(Library* lib, int fid);
};