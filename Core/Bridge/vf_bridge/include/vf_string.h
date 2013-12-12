#pragma once

#include "vf_const.h"

namespace vapula
{
	//转换指定编码的单字节字符串到宽字节字符串
	//源编码默认系统编码，目标编码为UTF16
	VAPULA_API cstrw MbToWc(cstr src, cstr code = null);

	//转换宽字节字符串到指定编码单字节字符串
	//源编码为UTF16，目标编码默认系统编码
	VAPULA_API cstr WcToMb(cstrw src, cstr code = null);

	//复制字符串，返回副本字符串地址
	VAPULA_API cstr CopyStrA(cstr src);
	VAPULA_API cstrw CopyStrW(cstrw src);

	//将给定多字节字符串中所有指定子串替换为目标
	VAPULA_API cstr ReplaceStrA(cstr src, cstr from, cstr to);

	//将给定宽字节字符串中所有指定子串替换为目标
	VAPULA_API cstrw ReplaceStrW(cstrw src, cstrw from, cstrw to);
}