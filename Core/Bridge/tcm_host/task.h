#pragma once

#include "tcm_task.h"

using namespace tcm;

//任务
class TaskEx : public Task
{
public:
	TaskEx();
	~TaskEx();
public:
	static Task* Parse(PCWSTR path);
private:
	Dictionary* _Tags;
	int _CtrlMode;
	PCSTR _CtrlConfig;
public:
	//获取扩展标签
	Dictionary* GetTags();

	//获取控制模式
	int GetCtrlMode();

	//获取控制配置
	PCSTR GetCtrlConfig();
public:
	bool RunAs(Worker* worker);
};