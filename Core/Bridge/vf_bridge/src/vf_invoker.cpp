#include "vf_invoker.h"
#include "vf_method.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_dataset.h"
#include "vf_runtime.h"
#include "vf_worker.h"
#include "process.h"

namespace vapula
{
	Invoker::Invoker()
	{
		_Stack = null;
		_IsSuspend = false;
	}

	Invoker::~Invoker()
	{
		Clear(_Stack);
	}

	bool Invoker::Bind(Method* mt)
	{
		_Stack = new Stack();
		_Stack->SetMethodId(str::Copy(mt->GetMethodId()), this);
		_Stack->SetDataset(mt->GetDataset()->Copy(), this);
		_Stack->SetContext(new Context(), this);
		_Stack->SetProtect(mt->HasProtect(), this);
		return true;
	}

	void Invoker::Invoke()
	{
		Runtime* runtime = Runtime::Instance();
		runtime->Link(_Stack);
		_Stack->SetStackId(Stack::CurrentId(), this);

		Context* context = _Stack->GetContext();
		try {
			context->SetCtrlCode(VF_CTRL_NULL, this);
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
		runtime->Kick(_Stack);
	}

	void Invoker::OnSafeProcess()
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

	void Invoker::OnSafeRollback()
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

	Stack* Invoker::GetStack()
	{
		return _Stack;
	}

	bool Invoker::Start(uint32 wait)
	{
		Worker* worker = Worker::Instance();
		return worker->Start(this, wait);
	}

	void Invoker::Stop(uint32 wait)
	{
		Worker* worker = Worker::Instance();
		worker->Stop(this, wait);
	}

	void Invoker::Pause(uint32 wait)
	{
		Worker* worker = Worker::Instance();
		worker->Pause(this, wait);
	}

	void Invoker::Resume()
	{
		Worker* worker = Worker::Instance();
		worker->Resume(this);
	}

	bool Invoker::Restart(uint32 wait)
	{
		Worker* worker = Worker::Instance();
		return worker->Restart(this, wait);
	}
}