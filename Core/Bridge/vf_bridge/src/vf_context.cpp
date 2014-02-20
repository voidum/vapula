#include "vf_context.h"
#include "vf_invoker.h"
#include "vf_stack.h"

namespace vapula
{
	Context::Context() 
	{
		_LastState = VF_STATE_IDLE;
		_CurrentState = VF_STATE_IDLE;
		_ReturnCode = VF_RETURN_NORMAL;
		_CtrlCode = VF_CTRL_NULL;
		_Progress = 0;
		_Lock = new Lock();
	}

	Context::~Context()
	{
		Clear(_Lock);
	}

	void Context::SetState(int8 value, Invoker* owner)
	{
		if(owner->GetStack()->GetContext() != this)
			return;
		_Lock->Enter();
		_LastState = _CurrentState;
		_CurrentState = value;
		_Lock->Leave();
	}

	int8 Context::GetCurrentState()
	{
		_Lock->Enter();
		int8 v = _CurrentState;
		_Lock->Leave();
		return v;
	}

	int8 Context::GetLastState()
	{
		_Lock->Enter();
		int8 v = _LastState;
		_Lock->Leave();
		return v;
	}

	void Context::SetReturnCode(int8 value)
	{
		_Lock->Enter();
		_ReturnCode = value;
		_Lock->Leave();
	}

	int8 Context::GetReturnCode()
	{
		_Lock->Enter();
		int8 v = _ReturnCode;
		_Lock->Leave();
		return v;
	}

	void Context::SetCtrlCode(int8 value, Invoker* owner)
	{
		if(owner->GetStack()->GetContext() != this)
			return;
		_Lock->Enter();
		_CtrlCode = value;
		_Lock->Leave();
	}

	int8 Context::GetCtrlCode()
	{
		_Lock->Enter();
		int8 v = _CtrlCode;
		_Lock->Leave();
		return v;
	}

	void Context::SwitchHold()
	{
		_Lock->Enter();
		if(_CtrlCode == VF_CTRL_PAUSE)
		{
			_LastState = _CurrentState;
			_CurrentState = VF_STATE_PAUSE;
		}
		else if(_CtrlCode == VF_CTRL_RESUME)
		{
			_CurrentState = _LastState;
			_LastState = VF_STATE_PAUSE;
		}
		_CtrlCode = VF_CTRL_NULL;
		_Lock->Leave();
	}

	void Context::SwitchBusy()
	{
		_Lock->Enter();
		if(_CurrentState == VF_STATE_BUSY_BACK)
		{
			_LastState = VF_STATE_BUSY_BACK;
			_CurrentState = VF_STATE_BUSY_FRONT;
		}
		else if(_CurrentState == VF_STATE_BUSY_FRONT)
		{
			_LastState = VF_STATE_BUSY_FRONT;
			_CurrentState = VF_STATE_BUSY_BACK;
		}
		_Lock->Leave();
	}

	float Context::GetProgress()
	{
		_Lock->Enter();
		float v = _Progress;
		_Lock->Leave();
		return v;
	}

	void Context::SetProgress(float value)
	{
		_Lock->Enter();
		_Progress = value;
		_Lock->Leave();
	}

	pcstr Context::GetKeyFrame()
	{
		_Lock->Enter();
		pcstr v = _KeyFrame;
		_Lock->Leave();
		return v;
	}

	void Context::SetKeyFrame(pcstr value)
	{
		_Lock->Enter();
		Clear(_KeyFrame);
		_KeyFrame = str::Copy(value);
		_Lock->Leave();
	}
}