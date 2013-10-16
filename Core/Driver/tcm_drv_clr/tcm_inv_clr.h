#pragma once

#include "tcm_Invoker.h"
#include <metahost.h>

using namespace tcm;

class InvokerCLR : public Invoker
{
public:
	InvokerCLR();
	~InvokerCLR();
private:
	PCWSTR _Handle;
public:
	PCWSTR GetHandle();
protected:
	UINT WINAPI _ThreadProc();
public:
	bool Initialize(Library* lib, int fid);
};