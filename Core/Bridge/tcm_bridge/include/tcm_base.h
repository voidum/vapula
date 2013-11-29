#pragma once

#pragma warning(disable:4251) //禁用DLL模板警告

#include "tcm_const.h"
#include "tcm_string.h"

namespace tcm
{
	//安全清理目标
	template<typename T>
	TCM_BRIDGE_API void Clear(T target, bool isarr = false)
	{
		if(target != null)
		{
			if(isarr) delete [] target;
			else delete target;
			target = null;
		}
	}

	//获得指定TCM类型的字节长度
	TCM_BRIDGE_API int GetTypeUnit(int type);

	//转换数值到多字节字符串
	template<typename T>
	TCM_BRIDGE_API str ValueToStr(T value)
	{
		std::ostringstream oss;
		oss.imbue(std::locale("C"));
		oss<<value;
		return CopyStrA(oss.str().c_str());
	}

	//转换Vector容器到定长数组
	template<typename T>
	TCM_BRIDGE_API T* VectorToArray(vector<T>& src)
	{
		if(src.size() == 0)
			return null;
		T* dst = new T[src.size()];
		int i=0;
		for(vector<T>::iterator iter = src.begin(); iter != src.end(); iter++)
			dst[i++] = *iter;
		return dst;
	}
	
	//生成一个随机HEX字符串
	TCM_BRIDGE_API str GetRandomHexA(int len);
	TCM_BRIDGE_API strw GetRandomHexW(int len);

	//生成时间字符串
	TCM_BRIDGE_API str GetTimeStrA();
	TCM_BRIDGE_API strw GetTimeStrW();

	//显示简易的信息框，值内容
	template<typename T>
	TCM_BRIDGE_API void ShowMsgValue(T value, strw caption = null)
	{
		ShowMsgStr(MbToWc(ValueToStr(value)), caption);
	}

	//显示简易的信息框，字符串内容
	TCM_BRIDGE_API void ShowMsgStr(str value, str caption = null);
	TCM_BRIDGE_API void ShowMsgStr(strw value, strw caption = null);

	//获取运行时所在目录
	TCM_BRIDGE_API strw GetRuntimeDir();

	//获取当前应用程序目录
	TCM_BRIDGE_API strw GetAppDir();

	//获取当前应用程序名称
	TCM_BRIDGE_API strw GetAppName();

	//获取路径的规范目录
	TCM_BRIDGE_API strw GetDirPath(strw path, bool isfile = false);

	//测试指定文件是否能以读的方式打开
	TCM_BRIDGE_API bool CanOpenRead(strw file);
}