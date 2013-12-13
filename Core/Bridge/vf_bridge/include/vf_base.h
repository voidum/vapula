#pragma once

#pragma warning(disable:4251)

#include "vf_const.h"
#include "vf_string.h"

namespace vapula
{
	//清理指定目标
	template<typename T>
	VAPULA_API void Clear(T target, bool isarr = false)
	{
		if(target != null)
		{
			if(isarr) delete [] target;
			else delete target;
			target = null;
		}
	}

	//转换Vector容器到定长数组
	template<typename T>
	VAPULA_API T* VectorToArray(vector<T>& src)
	{
		if(src.size() == 0)
			return null;
		T* dst = new T[src.size()];
		uint32 i=0;
		for(vector<T>::iterator iter = src.begin(); iter != src.end(); iter++)
			dst[i++] = *iter;
		return dst;
	}

	//获得指定VF类型的字节长度
	VAPULA_API int GetTypeUnit(int type);

	//生成本地唯一标识字符串
	VAPULA_API cstr8 GetLuid();

	//通过简易的信息框显示数值
	template<typename T>
	VAPULA_API void ShowMsgbox(T value)
	{
		ShowMsgStr(str::ValueTo(value), _vf_bridge);
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