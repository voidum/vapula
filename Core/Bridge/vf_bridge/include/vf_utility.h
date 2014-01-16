#pragma once
#pragma warning(disable:4275)

#include "vf_base.h"

namespace vapula
{
	class Token;

	//require [TrustedInvoker v2]
	class VAPULA_API RequireTI
	{
	public:
		RequireTI();
		virtual ~RequireTI();
	protected:
		Token* _Token;
	public:
		bool AssertOffTI();
		void TokenOff(uint8& key);
		void TokenOn(uint8 key);
	};

	//show value by simple message box
	template<typename T>
	VAPULA_API void ShowMsgbox(T value)
	{
		ShowMsgbox(str::ValueTo(value), _vf_bridge);
	}

	//show string by simple message box
	VAPULA_API void ShowMsgbox(cstr8 value, cstr8 caption = null);
	VAPULA_API void ShowMsgbox(cstr16 value, cstr16 caption = null);

	//get runtime directory
	VAPULA_API cstr8 GetRuntimeDir();

	//get process directory
	VAPULA_API cstr8 GetAppDir();

	//get process name
	VAPULA_API cstr8 GetAppName();

	//get path directory
	VAPULA_API cstr8 GetDirPath(cstr8 path, bool isfile = false);

	//test if file can be opened as read
	VAPULA_API bool CanOpenRead(cstr8 file);
}