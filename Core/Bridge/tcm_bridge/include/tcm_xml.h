#pragma once

#include "tcm_base.h"
#include <cstdarg>

namespace tcm
{
	//TCM XML操作实用函数集 要求引入RapidXML
	namespace xml
	{
		//加载XML
		//返回的数据引用用于在不使用XML后释放内存
		TCM_BRIDGE_API LPVOID Load(PCWSTR path, PCSTR& data);

		//解析XML
		TCM_BRIDGE_API LPVOID Parse(PCSTR xml);

		//输出成字符串
		TCM_BRIDGE_API PCSTR Print(LPVOID src);

		//模拟XPath查询输入节点的子节点
		TCM_BRIDGE_API LPVOID Path(LPVOID src, int count, ...);

		//读取节点的值的多字节字符串
		TCM_BRIDGE_API PCSTR ValueA(LPVOID src);

		//读取节点的值的宽字节字符串
		TCM_BRIDGE_API PCWSTR ValueW(LPVOID src);

		//读取节点的值并转换整数
		TCM_BRIDGE_API int ValueInt(LPVOID src);

		//读取节点的值并转换实数
		TCM_BRIDGE_API double ValueReal(LPVOID src);

		//读取节点的值并匹配字符串
		TCM_BRIDGE_API bool ValueBool(LPVOID src, PCSTR value);
	}
}