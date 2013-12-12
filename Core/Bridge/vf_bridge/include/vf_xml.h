#pragma once

#include "vf_base.h"
#include <cstdarg>

namespace vapula
{
	//TCM XML操作实用函数集 要求引入RapidXML
	namespace xml
	{
		//加载XML
		//返回的数据引用用于在不使用XML后释放内存
		VAPULA_API object Load(cstrw path, cstr& data);

		//解析XML
		VAPULA_API object Parse(cstr xml);

		//输出成字符串
		VAPULA_API cstr Print(object src);

		//模拟XPath查询输入节点的子节点
		VAPULA_API object Path(object src, int count, ...);

		//读取节点的值的多字节字符串
		VAPULA_API cstr ValueA(object src);

		//读取节点的值的宽字节字符串
		VAPULA_API cstrw ValueW(object src);

		//读取节点的值并转换整数
		VAPULA_API int ValueInt(object src);

		//读取节点的值并转换实数
		VAPULA_API double ValueReal(object src);

		//读取节点的值并匹配字符串
		VAPULA_API bool ValueBool(object src, cstr value);
	}
}