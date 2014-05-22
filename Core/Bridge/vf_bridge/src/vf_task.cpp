#include "vf_task.h"
#include "vf_runtime.h"
#include "vf_stack.h"
#include "vf_method.h"
#include "vf_context.h"
#include "vf_dataset.h"
#include "vf_worker.h"

namespace vapula
{
	Task::Task()
	{
		_Stack = null;
	}

	Task::~Task()
	{
		Clear(_Stack);
	}

	bool Task::Bind(Method* mt)
	{
		_Stack = new Stack();
		_Stack->SetMethodId(str::Copy(mt->GetMethodId()), this);
		_Stack->SetDataset(mt->GetDataset()->Copy(), this);
		_Stack->SetContext(new Context(), this);
		_Stack->SetProtect(mt->HasProtect(), this);
		return true;
	}

	void Task::Invoke()
	{
		Runtime* runtime = Runtime::Instance();
		runtime->Link(_Stack);
		_Stack->SetStackId(Stack::CurrentId(), this);

		Context* context = _Stack->GetContext();
		try {
			context->SetControlCode(VF_CTRL_NULL, this);
			context->SetReturnCode(VF_RETURN_NULLTASK);
			context->SetState(VF_STATE_BUSY_BACK, this);
			if (_Stack->HasProtect())
				OnSafeProcess();
			else
				OnProcess();
			context->SetReturnCode(VF_RETURN_NORMAL);
  		} catch (Error*) {
			context->SetState(VF_STATE_ROLLBACK, this);
			if (_Stack->HasProtect())
				OnSafeRollback();
			else
				OnRollback();
			context->SetReturnCode(VF_RETURN_ERROR);
		}
		context->SetState(VF_STATE_IDLE, this);

		_Stack->SetStackId(0, this);
		runtime->Kick<Stack>(_Stack->GetStackId());
	}

	void Task::OnSafeProcess()
	{
		__try {
			OnProcess();
		}
		__except (EXCEPTION_EXECUTE_HANDLER) {
			if (_Stack != null) {
				Context* ctx = _Stack->GetContext();
				if (ctx != null) {
					ctx->SetReturnCode(VF_RETURN_UNHANDLED);
				}
			}
			ShowMsgbox(_vf_fatal, _vf_bridge);
		}
	}

	void Task::OnSafeRollback()
	{
		__try {
			OnRollback();
		}
		__except (EXCEPTION_EXECUTE_HANDLER) {
			if (_Stack != null) {
				Context* ctx = _Stack->GetContext();
				if (ctx != null) {
					ctx->SetReturnCode(VF_RETURN_UNHANDLED);
				}
			}
			ShowMsgbox(_vf_fatal, _vf_bridge);
		}
	}

	Stack* Task::GetStack()
	{
		return _Stack;
	}

	void Task::Start()
	{
		Worker* worker = Worker::Instance();
		worker->StartTask(this);
	}

	void Task::Stop(uint32 wait)
	{
		Worker* worker = Worker::Instance();
		Context* context = _Stack->GetContext();
		if (wait != 0)
		{
			context->SetControlCode(VF_CTRL_CANCEL, this);
			uint32 time0 = GetTickCount();
			uint32 time1 = time0;
			while (time1 - time0 < wait)
			{
				if (context->GetCurrentState() == VF_STATE_IDLE)
					return;
				Sleep(20);
			}
		}
		worker->StopTask(this);
	}

	void Task::Pause(uint32 wait)
	{
		Worker* worker = Worker::Instance();
		Context* context = _Stack->GetContext();
		if (wait != 0)
		{
			context->SetControlCode(VF_CTRL_PAUSE, this);
			uint32 time0 = GetTickCount();
			uint32 time1 = time0;
			while (time1 - time0 < wait)
			{
				if (context->GetCurrentState() == VF_STATE_PAUSE)
					return;
				Sleep(20);
			}
		}
		worker->PauseTask(this);
	}

	void Task::Resume()
	{
		Worker* worker = Worker::Instance();
		if (!worker->ResumeTask(this))
		{
			Context* context = _Stack->GetContext();
			context->SetControlCode(VF_CTRL_RESUME, this);
		}
	}
}