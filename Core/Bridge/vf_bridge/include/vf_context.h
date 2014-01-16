#pragma once

#include "vf_utility.h"

namespace vapula
{
	//routine context
	class VAPULA_API Context
	{
	public:
		Context();
		~Context();
	private:
		Lock* _Lock;
	private:
		uint8 _LastState;
		uint8 _CurrentState;
		uint8 _ReturnCode;
		uint8 _CtrlCode;
		float _Progress; //0 - 100
	public:
		//{TI2} set state
		void SetState(uint8 value);

		//get current state
		uint8 GetCurrentState();

		//get last state
		uint8 GetLastState();

		//set return code
		void SetReturnCode(uint8 value);

		//get return code
		uint8 GetReturnCode();

		//{TI2} set control code
		void SetCtrlCode(uint8 value);

		//get control code
		uint8 GetCtrlCode();

		//switch hold state between pause and resume
		void SwitchHold();

		//switch busy state between front and back
		void SwitchBusy();

		//set progress
		void SetProgress(float value);

		//get progress
		float GetProgress();
	};
}