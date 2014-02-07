#pragma once
#pragma warning(disable:4275)

#include "vf_base.h"

namespace vapula
{
	//show value by simple message box
	template<typename T>
	VAPULA_API void ShowMsgbox(T value)
	{
		ShowMsgbox(str::Value(value), _vf_bridge);
	}

	//show string by simple message box
	VAPULA_API void ShowMsgbox(pcstr value, pcstr caption);

	//get runtime directory
	VAPULA_API pcstr GetRuntimeDir();

	//get process directory
	VAPULA_API pcstr GetProcessDir();

	//get process name
	VAPULA_API pcstr GetProcessName();

	//get path directory
	VAPULA_API pcstr GetDirPath(pcstr path, bool isfile = false);

	//test if file can be opened as read
	VAPULA_API bool CanOpenRead(pcstr file);
}