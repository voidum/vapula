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
		uint8 _LastState;
		uint8 _CurrentState;
		uint8 _ReturnCode;
		uint8 _ControlCode;
		pcstr _KeyFrame;
		float _Progress; //0 - 100

	public:
		Context();
		~Context();

	public:
		//set state
		void SetState(uint8 value, Invoker* owner);

		//set return code
		void SetReturnCode(uint8 value);

		//set control code
		void SetCtrlCode(uint8 value, Invoker* owner);

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
		uint8 GetCurrentState();

		//get last state
		uint8 GetLastState();

		//get return code
		uint8 GetReturnCode();

		//get control code
		uint8 GetControlCode();

		//get progress
		float GetProgress();

		//get key frame
		pcstr GetKeyFrame();
	};
}