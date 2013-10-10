#pragma once

#pragma warning(disable:4251) //禁用DLL模板警告

#ifdef TCM_BRIDGE_EXPORTS
#define TCM_BRIDGE_API __declspec(dllexport)
#else
#define TCM_BRIDGE_API
#endif

#include <windows.h>
#include <sstream>
#include <vector>

namespace tcm
{
	using std::vector;

	//TCM支持的数据类型
	enum DataType
	{
		TCM_DATA_POINTER = 0,
		TCM_DATA_INT8 = 1,
		TCM_DATA_INT16 = 2,
		TCM_DATA_INT32 = 3,
		TCM_DATA_INT64 = 4,
		TCM_DATA_UINT8 = 5,
		TCM_DATA_UINT16 = 6,
		TCM_DATA_UINT32 = 7,
		TCM_DATA_UINT64 = 8,
		TCM_DATA_REAL32 = 10,
		TCM_DATA_REAL64 = 11,
		TCM_DATA_BOOL = 20,
		TCM_DATA_CSTRA = 21,
		TCM_DATA_CSTRW = 22
	};

	//TCM支持的运行状态
	enum State
	{
		TCM_STATE_IDLE = 0,
		TCM_STATE_BUSY = 1,
		TCM_STATE_PAUSE = 2
	};

	//TCM支持的控制码
	enum CtrlCode
	{
		TCM_CTRL_NULL = 0,
		TCM_CTRL_PAUSE = 1,
		TCM_CTRL_RESUME = 2,
		TCM_CTRL_CANCEL = 3,
		TCM_CTRL_RESTART = 4
	};

	//TCM预置的返回值
	enum ReturnCode
	{
		TCM_RETURN_ERROR = 0,
		TCM_RETURN_NORMAL = 1,
		TCM_RETURN_CANCELBYMSG = 2,
		TCM_RETURN_CANCELBYFORCE = 3,
		TCM_RETURN_NULLENTRY = 4,
		TCM_RETURN_NULLTASK = 5,
	};

	//安全清理目标
	template<typename T>
	TCM_BRIDGE_API void Clear(T target, bool isarr = false)
	{
		if(target != NULL)
		{
			if(isarr) delete [] target;
			else delete target;
			target = NULL;
		}
	}

	//获得指定TCM类型的字节长度
	TCM_BRIDGE_API int GetTypeUnit(int type);

	//转换多字节到宽字节
	//转换到宽字节默认使用UTF8编码
	TCM_BRIDGE_API PCWSTR MbToWc(PCSTR src, UINT cp = CP_UTF8);

	//转换宽字节到多字节
	//转换到多字节默认使用UTF8编码
	TCM_BRIDGE_API PCSTR WcToMb(PCWSTR src, UINT cp = CP_UTF8);

	//将给定多字节字符串中所有指定子串替换为目标
	TCM_BRIDGE_API PCSTR ReplaceStrA(PCSTR src, PCSTR from, PCSTR to);

	//将给定宽字节字符串中所有指定子串替换为目标
	TCM_BRIDGE_API PCWSTR ReplaceStrW(PCWSTR src, PCWSTR from, PCWSTR to);

	//修正编码
	//将多字节经过指定中间编码转换成指定最终编码
	TCM_BRIDGE_API PCSTR FixEncoding(PCSTR src, UINT cpMid = CP_UTF8, UINT cpAim = CP_OEMCP);

	//复制字符串，返回副本字符串地址
	TCM_BRIDGE_API PCSTR  CopyStrA(PCSTR src);
	TCM_BRIDGE_API PCWSTR CopyStrW(PCWSTR src);

	//转换数值到多字节字符串
	template<typename T>
	TCM_BRIDGE_API PCSTR ValueToStrA(T value)
	{
		std::ostringstream oss;
		oss.imbue(std::locale("C"));
		oss<<value;
		return CopyStrA(oss.str().c_str());
	}

	//转换数值到宽字节字符串
	template<typename T>
	TCM_BRIDGE_API PCWSTR ValueToStrW(T value)
	{
		std::ostringstream oss;
		oss.imbue(std::locale("C"));
		oss<<value;
		return MbToWc(oss.str().c_str());
	}

	//转换Vector容器到定长数组
	template<typename T>
	TCM_BRIDGE_API T* VectorToArray(vector<T>& src)
	{
		if(src.size() == 0) return NULL;
		T* dst = new T[src.size()];
		int i=0;
		for(vector<T>::iterator iter=src.begin();iter!=src.end();iter++) dst[i++] = *iter;
		return dst;
	}
	
	//生成一个随机HEX字符串
	TCM_BRIDGE_API PCSTR GetRandomHexA(int len);
	TCM_BRIDGE_API PCWSTR GetRandomHexW(int len);

	//生成时间字符串
	TCM_BRIDGE_API PCSTR GetTimeStrA();
	TCM_BRIDGE_API PCWSTR GetTimeStrW();

	//显示简易的信息框
	TCM_BRIDGE_API void ShowMsgbox(PCWSTR value = NULL, PCWSTR caption = NULL);
	TCM_BRIDGE_API void ShowMsgbox(PCSTR value = NULL, PCSTR caption = NULL);

	template<typename T>
	TCM_BRIDGE_API void ShowMsgbox(T value, PCWSTR caption = NULL)
	{
		ShowMsgbox(ValueToStrW(value), caption);
	}

	//获取运行时所在目录
	TCM_BRIDGE_API PCWSTR GetRuntimeDir();

	//获取当前应用程序目录
	//NEED REWRITE
	TCM_BRIDGE_API PCWSTR GetAppDir();

	//获取当前应用程序名称
	//NEED REWRITE
	TCM_BRIDGE_API PCWSTR GetAppName();

	//获取路径的规范目录
	TCM_BRIDGE_API PCWSTR GetDirPath(PCWSTR path, bool isfile = false);

	//测试指定文件是否能以读的方式打开
	TCM_BRIDGE_API bool CanOpenRead(PCWSTR file);
}