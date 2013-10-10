#include "stdafx.h"
#include "tcm_context.h"
#include <iostream>

namespace tcm
{
	Context::Context() 
	{
		_State = TCM_STATE_IDLE;
		_ReturnCode = TCM_RETURN_NORMAL;
		_CtrlCode = TCM_CTRL_NULL;
		_ProgValue = 0;
		_Stopwatch = new Stopwatch();
		_Lock = new Lock();
		_TokenKey = new VarAOO();
		_Logger = new Logger();
	}

	Context::~Context()
	{
		Clear(_TokenKey);
		Clear(_Logger);
		Clear(_Stopwatch);
		Clear(_Lock);
	}

	bool Context::ValidToken(Token* token)
	{
		if(token == NULL) return false;
		USHORT tmp_key = *(_TokenKey->Get<USHORT>());
		return token->Match(tmp_key);
	}

	bool Context::CanSeal()
	{
		return _TokenKey->CanSet();
	}

	void Context::Seal(USHORT key)
	{
		_Lock->Enter();
		_TokenKey->Set(&key);
		_Lock->Leave();
	}

	Stopwatch* Context::GetStopwatch()
	{
		return _Stopwatch;
	}

	Logger* Context::GetLogger()
	{
		return _Logger;
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
		case TCM_CTRL_NULL: break;
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