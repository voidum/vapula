#pragma once

#include "vf_crt.h"

typedef void (*Action)();

class TaskCRT : public Task
{
private:
	Action _EntryProcess;
	Action _EntryRollback;

public:
	TaskCRT();
	~TaskCRT();

protected:
	void OnProcess();
	void OnRollback();

public:
	bool Bind(Method* method);
};