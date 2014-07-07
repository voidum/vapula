#include "vf_weaver.h"
#include "vf_runtime.h"
#include "vf_aspect.h"
#include "vf_aspect_hub.h"
#include "vf_task.h"
#include "vf_stack.h"
#include "vf_dataset.h"

namespace vapula
{
	Weaver* Weaver::_Instance = null;

	Weaver* Weaver::Instance()
	{
		return Weaver::_Instance;
	}

	Weaver::Weaver() { }

	Weaver::~Weaver()
	{
	}

	Task* Weaver::Invoke(Aspect* aspect)
	{
		Task* task = aspect->CreateTask();
		Stack* stack = task->GetStack();
		
		Dataset* dataset = stack->GetDataset();
		Record* record = (*dataset)[1];
		Stack* stack_attached = Stack::Instance();
		uint32 stack_addr = (uint32)(stack_attached);
		record->Write(&stack_addr, sizeof(uint32));

		task->Start();
		return task;
	}

	void Weaver::OnReachFrame(pcstr frame)
	{
		typedef list<Aspect*>::iterator iter1;
		typedef list<Task*>::iterator iter2;
		list<Task*> tasks;
		list<Aspect*> aspects = Aspect::Hub()->GetInnerData();
		for (iter1 i = aspects.begin(); i != aspects.end(); i++)
		{
			Aspect* aspect = *i;
			if (aspect->TryMatch(frame))
			{
				Task* task = Invoke(aspect);
				if (!aspect->IsAsync())
					tasks.push_back(task);
			}
		}
		for (iter2 i = tasks.begin(); i != tasks.end(); i++)
		{
			Task* task = *i;
			task->Join();
		}
	}
}