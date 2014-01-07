#pragma once

#include "vf_utility.h"

namespace vapula
{
	//组件上下文
	class VAPULA_API Context
	{
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
	public:
		//安全调用：设置状态
		void SetState(int state);

		//获取状态
		int GetState();

		//安全调用：设置返回值
		void SetReturnCode(int return_code);

		//获取返回值
		int GetReturnCode();

		//安全调用：设置控制码
		void SetCtrlCode(int ctrl_code);

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