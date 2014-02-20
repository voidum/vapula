#pragma once

#include "vf_base.h"

namespace vapula
{
	class Invoker;

	//routine context
	class VAPULA_API Context
	{
	private:
		Lock* _Lock;
	private:
		int8 _LastState;
		int8 _CurrentState;
		int8 _ReturnCode;
		int8 _CtrlCode;
		pcstr _KeyFrame;
		float _Progress; //0 - 100

	public:
		Context();
		~Context();

	public:
		//set state
		void SetState(int8 value, Invoker* owner);

		//set return code
		void SetReturnCode(int8 value);

		//set control code
		void SetCtrlCode(int8 value, Invoker* owner);

		//set progress
		void SetProgress(float value);

		//set key frame
		void SetKeyFrame(pcstr value);

		//switch between pause and resume
		void SwitchHold();

		//switch between front and back
		void SwitchBusy();

	public:
		//get current state
		int8 GetCurrentState();

		//get last state
		int8 GetLastState();

		//get return code
		int8 GetReturnCode();

		//get control code
		int8 GetCtrlCode();

		//get progress
		float GetProgress();

		//get key frame
		pcstr GetKeyFrame();
	};
}