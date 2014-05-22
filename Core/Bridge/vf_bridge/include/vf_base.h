#pragma once

#pragma warning(disable:4251)
#pragma warning(disable:4275)

#include "vf_assist.h"
#include "vf_string.h"
#include "vf_debug.h"

namespace vapula
{
	//spin lock
	class VAPULA_API Lock : Uncopiable
	{
	private:
		uint64* _Core; //TRUE - Lock , FALSE - Unlock

	public:
		Lock();
		~Lock();

	public:
		//capture lock
		void Enter();

		//release lock
		void Leave();

	private:
		static Lock* _CtorLock;

	public:
		//get lock for ctor
		static Lock* GetCtorLock();
	};

	//can be set only once
	class VAPULA_API Once : Uncopiable
	{
	private:
		Lock* _Lock;
		raw _Data;
		raw _Seal;

	public:
		Once();
		~Once();

	public:
		//test if can be set
		bool CanSet();

		//set value
		void Set(raw data, uint32 size);

		//get value
		raw Get();
	};

	//flag
	class VAPULA_API Flag : Uncopiable
	{
	private:
		Lock* _Lock;
		int _Value;

	public:
		Flag();
		~Flag();

	public:
		//enable flag
		void Enable(int flag);

		//disable flag
		void Disable(int flag);

		//valid flag
		bool Valid(int flag);
	};

	//get value unit
	VAPULA_API uint32 GetValueUnit(uint8 type);

	//convert raw data to base64 string
	VAPULA_API pcstr RawToBase64(raw data, uint32 size);

	//convert base64 string to raw data
	VAPULA_API raw Base64ToRaw(pcstr data);

	//show value by simple message box
	template<typename T>
	VAPULA_API void ShowMsgbox(T value)
	{
		ShowMsgbox(str::Value(value), _vf_bridge);
	}

	//show string by simple message box
	VAPULA_API void ShowMsgbox(pcstr value, pcstr caption);

	//get path directory
	VAPULA_API pcstr GetPathDir(pcstr path, bool file = false);

	//test if file can be opened as read
	VAPULA_API bool TryOpenRead(pcstr file);

	//wait for a moment by ms
	VAPULA_API void WaitSpan(uint32 wait);
}