#pragma once

#include "tcm_executor.h"
#include <metahost.h>

using namespace tcm;

class ExecutorCLR : public Executor
{
public:
	ExecutorCLR();
	~ExecutorCLR();
private:
	PCWSTR _Handle;
public:
	PCWSTR GetHandle();
protected:
	UINT WINAPI _ThreadProc();
public:
	bool Initialize(Library* lib, int fid);
};