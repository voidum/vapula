#pragma once

#include "vf_candy.h"

namespace vapula
{
	//组件上下文
	class VAPULA_API Context
	{
	public:
		friend class Invoker;
	public:
		Context();
		~Context();
	private:
		Lock* _Lock;
	private:
		int	_State;
		int _ReturnCode;
		int _CtrlCode;
		float _ProgValue; //0 - 100
	private:
		//安全调用：设置状态
		void SetState(int state);

		//安全调用：设置返回值
		void SetReturnCode(int return_code);

		//安全调用：设置控制码
		void SetCtrlCode(int ctrl_code);
	public:
		//获取状态
		int GetState();

		//获取返回值
		int GetReturnCode();

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