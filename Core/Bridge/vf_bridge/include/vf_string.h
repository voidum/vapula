#pragma once

#include "vf_const.h"

namespace vf
{
	//转换指定编码的单字节字符串到宽字节字符串
	//源编码默认系统编码，目标编码为UTF16
	TCM_BRIDGE_API strw MbToWc(str src, str code = null);

	//转换宽字节字符串到指定编码单字节字符串
	//源编码为UTF16，目标编码默认系统编码
	TCM_BRIDGE_API str WcToMb(strw src, str code = null);

	//复制字符串，返回副本字符串地址
	TCM_BRIDGE_API str CopyStrA(str src);
	TCM_BRIDGE_API strw CopyStrW(strw src);

	//将给定多字节字符串中所有指定子串替换为目标
	TCM_BRIDGE_API str ReplaceStrA(str src, str from, str to);

	//将给定宽字节字符串中所有指定子串替换为目标
	TCM_BRIDGE_API strw ReplaceStrW(strw src, strw from, strw to);
}