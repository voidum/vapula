#pragma once

#include "tcm_executor.h"

using namespace tcm;

typedef int (*Delegate)(int, Envelope*, Context*);

class ExecutorCRT : public Executor
{
public:
	ExecutorCRT();
	~ExecutorCRT();
private:
	Delegate _Entry;
protected:
	UINT WINAPI _ThreadProc();
public:
	bool Initialize(Library* lib, int fid);
};