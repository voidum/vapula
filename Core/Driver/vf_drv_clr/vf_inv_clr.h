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
	void _Entry();
public:
	bool Initialize(Library* lib, int fid);
};