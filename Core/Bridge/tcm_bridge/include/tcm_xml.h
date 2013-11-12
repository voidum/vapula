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
		TCM_BRIDGE_API object Load(strw path, str& data);

		//解析XML
		TCM_BRIDGE_API object Parse(str xml);

		//输出成字符串
		TCM_BRIDGE_API str Print(object src);

		//模拟XPath查询输入节点的子节点
		TCM_BRIDGE_API object Path(object src, int count, ...);

		//读取节点的值的多字节字符串
		TCM_BRIDGE_API str ValueA(object src);

		//读取节点的值的宽字节字符串
		TCM_BRIDGE_API strw ValueW(object src);

		//读取节点的值并转换整数
		TCM_BRIDGE_API int ValueInt(object src);

		//读取节点的值并转换实数
		TCM_BRIDGE_API double ValueReal(object src);

		//读取节点的值并匹配字符串
		TCM_BRIDGE_API bool ValueBool(object src, str value);
	}
}