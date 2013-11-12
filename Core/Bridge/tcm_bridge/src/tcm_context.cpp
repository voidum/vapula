#include "stdafx.h"
#include "tcm_context.h"

namespace tcm
{
	Context::Context() 
	{
		_State = TCM_STATE_IDLE;
		_ReturnCode = TCM_RETURN_NORMAL;
		_CtrlCode = TCM_CTRL_NULL;
		_ProgValue = 0;
		_Lock = new Lock();
		_TokenKey = new VarAOO();
	}

	Context::~Context()
	{
		Clear(_TokenKey);
		Clear(_Lock);
	}

	bool Context::ValidToken(Token* token)
	{
		if(token == null) return false;
		uint16 tmp_key = *(_TokenKey->Get<uint16>());
		return token->Match(tmp_key);
	}

	bool Context::CanSeal()
	{
		return _TokenKey->CanSet();
	}

	void Context::Seal(uint16 key)
	{
		_Lock->Enter();
		_TokenKey->Set(&key);
		_Lock->Leave();
	}

	void Context::SetState(Token* token, int state)
	{
		if(!ValidToken(token)) return;
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

	void Context::SetReturnCode(Token* token, int return_code)
	{
		if(!ValidToken(token)) return;
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

	void Context::SetCtrlCode(Token* token, int ctrl_code)
	{
		if(!ValidToken(token)) return;
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
		case TCM_CTRL_NULL:
			if(_State == TCM_STATE_BUSY)
				_State = TCM_STATE_UI;
			else if(_State == TCM_STATE_UI)
				_State = TCM_STATE_BUSY;
			break;
		case TCM_CTRL_CANCEL: break;
		case TCM_CTRL_PAUSE:
			_State = TCM_STATE_PAUSE;
			break;
		case TCM_CTRL_RESUME:
			_State = TCM_STATE_BUSY;
			break;
		}
		_CtrlCode = TCM_CTRL_NULL;
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