#pragma once

#include "vf_clr.h"

class TaskCLR : public Task
{
private:
	pcstr _Handle;

public:
	TaskCLR();
	~TaskCLR();

protected:
	void OnProcess();
	void OnRollback();

public:
	pcstr GetHandle();
	bool Bind(Method* method);
};