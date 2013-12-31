#pragma once
#pragma warning(disable:4275)

#include "vf_base.h"

namespace vapula
{
	//通过简易的信息框显示数值
	template<typename T>
	VAPULA_API void ShowMsgbox(T value)
	{
		ShowMsgbox(str::ValueTo(value), _vf_bridge);
	}

	//通过简易的信息框显示字符串
	VAPULA_API void ShowMsgbox(cstr8 value, cstr8 caption = null);
	VAPULA_API void ShowMsgbox(cstr16 value, cstr16 caption = null);

	//获取运行时所在目录
	VAPULA_API cstr8 GetRuntimeDir();

	//获取当前应用程序目录
	VAPULA_API cstr8 GetAppDir();

	//获取当前应用程序名称
	VAPULA_API cstr8 GetAppName();

	//获取路径的规范目录
	VAPULA_API cstr8 GetDirPath(cstr8 path, bool isfile = false);

	//测试指定文件是否能以读的方式打开
	VAPULA_API bool CanOpenRead(cstr8 file);
}