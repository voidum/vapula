#pragma once

#include "vf_const.h"

namespace vapula
{
	namespace str
	{
		//转换数值到字符串
		template<typename T>
		VAPULA_API cstr8 ValueTo(T value)
		{
			std::ostringstream oss;
			oss.imbue(std::locale("C"));
			oss<<value;
			return Copy(oss.str().c_str());
		}

		//转换16位字节字符串到指定编码8位字节字符串
		//源编码为UTF16，目标编码默认系统编码
		VAPULA_API cstr8 ToCh8(cstr16 src, cstr8 cp = null);

		//转换指定编码的8位字节字符串到16位字节字符串
		//源编码默认为系统编码，目标编码为UTF16
		VAPULA_API cstr16 ToCh16(cstr8 src, cstr8 cp = null);

		//转换指定编码的8位字节字符串到另一指定编码
		VAPULA_API cstr8 EncodeCh8(cstr8 src, cstr8 cp_from, cstr8 cp_to);

		//复制字符串，返回副本字符串地址
		VAPULA_API cstr8 Copy(cstr8 src);
		VAPULA_API cstr16 Copy(cstr16 src);

		//将给定字符串中所有指定子串替换为目标
		VAPULA_API cstr8 Replace(cstr8 src, cstr8 from, cstr8 to);
		VAPULA_API cstr16 Replace(cstr16 src, cstr16 from, cstr16 to);
	}
}