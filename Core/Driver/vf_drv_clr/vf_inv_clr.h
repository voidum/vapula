#pragma once

#include "vf_clr.h"

class InvokerCLR : public Invoker
{
public:
	InvokerCLR();
	~InvokerCLR();
private:
	cstr8 _Handle;
public:
	cstr8 GetHandle();
protected:
	uint32 WINAPI _ThreadProc();
public:
	bool Initialize(Library* lib, int fid);
};