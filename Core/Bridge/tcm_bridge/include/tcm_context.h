#pragma once

#include "tcm_log.h"
#include "tcm_time.h"
#include "tcm_token.h"

namespace tcm
{
	//组件上下文
	class TCM_BRIDGE_API Context
		: public Stampable
	{
	public:
		Context();
		~Context();
	private:
		VarAOO* _TokenKey;
		Lock* _Lock;
	private:
		int	_State;
		int _ReturnCode;
		int _CtrlCode;
		float _ProgValue; //0 - 100
	private:
		Dictionary* _Extend;
	private:
		bool ValidToken(Token* token);
	public:
		//封签上下文
		void Seal(USHORT key);

		//检测是否可封签
		bool CanSeal();
	public:
		//获取精确计时器
		Stopwatch* GetStopwatch();

		//获取日志器
		Logger* GetLogger();
	public:
		//安全调用：设置状态
		void SetState(Token* token, int state);

		//获取状态
		int GetState();

		//安全调用：设置返回值
		void SetReturnCode(Token* token, int return_code);

		//获取返回值
		int GetReturnCode();

		//安全调用：设置控制码
		void SetCtrlCode(Token* token, int ctrl_code);

		//获取控制码
		int GetCtrlCode();

		//响应控制码
		void ReplyCtrlCode();

		//设置进度
		void SetProgress(float value);

		//获取进度
		float GetProgress();
	};
}