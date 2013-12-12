#include "vf_context.h"

namespace vapula
{
	Context::Context() 
	{
		_State = VF_STATE_IDLE;
		_ReturnCode = VF_RETURN_NORMAL;
		_CtrlCode = VF_CTRL_NULL;
		_ProgValue = 0;
		_Lock = new Lock();
	}

	Context::~Context()
	{
		Clear(_Lock);
	}

	void Context::SetState(int state)
	{
		_Lock->Enter();
		_State = state;
		_Lock->Leave();
	}

	int Context::GetState()
	{
		_Lock->Enter();
		int v = _State;
		_Lock->Leave();
		return v;
	}

	void Context::SetReturnCode(int return_code)
	{
		_Lock->Enter();
		_ReturnCode = return_code;
		_Lock->Leave();
	}

	int Context::GetReturnCode()
	{
		_Lock->Enter();
		int v = _ReturnCode;
		_Lock->Leave();
		return v;
	}

	void Context::SetCtrlCode(int ctrl_code)
	{
		_Lock->Enter();
		_CtrlCode = ctrl_code;
		_Lock->Leave();
	}

	int Context::GetCtrlCode()
	{
		_Lock->Enter();
		int v = _CtrlCode;
		_Lock->Leave();
		return v;
	}

	void Context::ReplyCtrlCode()
	{
		_Lock->Enter();
		switch(_CtrlCode)
		{
		case VF_CTRL_NULL:
			if(_State == VF_STATE_BUSY)
				_State = VF_STATE_UI;
			else if(_State == VF_STATE_UI)
				_State = VF_STATE_BUSY;
			break;
		case VF_CTRL_CANCEL: break;
		case VF_CTRL_PAUSE:
			_State = VF_STATE_PAUSE;
			break;
		case VF_CTRL_RESUME:
			_State = VF_STATE_BUSY;
			break;
		}
		_CtrlCode = VF_CTRL_NULL;
		_Lock->Leave();
	}

	float Context::GetProgress()
	{
		_Lock->Enter();
		float v = _ProgValue;
		_Lock->Leave();
		return v;
	}

	void Context::SetProgress(float value)
	{
		_Lock->Enter();
		_ProgValue = value;
		_Lock->Leave();
	}
}