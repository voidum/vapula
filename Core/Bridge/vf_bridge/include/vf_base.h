#pragma once

#pragma warning(disable:4251) //禁用DLL模板警告

#include "vf_const.h"
#include "vf_string.h"

namespace vapula
{
	//安全清理目标
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

	//转换数值到窄字符串
	template<typename T>
	VAPULA_API cstr ValueToStr(T value)
	{
		std::ostringstream oss;
		oss.imbue(std::locale("C"));
		oss<<value;
		return CopyStrA(oss.str().c_str());
	}

	//转换Vector容器到定长数组
	template<typename T>
	VAPULA_API T* VectorToArray(vector<T>& src)
	{
		if(src.size() == 0)
			return null;
		T* dst = new T[src.size()];
		int i=0;
		for(vector<T>::iterator iter = src.begin(); iter != src.end(); iter++)
			dst[i++] = *iter;
		return dst;
	}

	//获得指定VF类型的字节长度
	VAPULA_API int GetTypeUnit(int type);

	//生成本地唯一标识字符串
	VAPULA_API cstr GetLuidA();
	VAPULA_API cstrw GetLuidW();

	//显示简易的信息框，值内容
	template<typename T>
	VAPULA_API void ShowMsgValue(T value, cstrw caption = null)
	{
		ShowMsgStr(MbToWc(ValueToStr(value)), caption);
	}

	//显示简易的信息框，字符串内容
	VAPULA_API void ShowMsgStr(cstr value, cstr caption = null);
	VAPULA_API void ShowMsgStr(cstrw value, cstrw caption = null);

	//获取运行时所在目录
	VAPULA_API cstrw GetRuntimeDir();

	//获取当前应用程序目录
	VAPULA_API cstrw GetAppDir();

	//获取当前应用程序名称
	VAPULA_API cstrw GetAppName();

	//获取路径的规范目录
	VAPULA_API cstrw GetDirPath(cstrw path, bool isfile = false);

	//测试指定文件是否能以读的方式打开
	VAPULA_API bool CanOpenRead(cstrw file);
}