#pragma once

#include "vf_base.h"

namespace vapula
{
	//参数信封
	class VAPULA_API Envelope
	{
	private:
		Envelope(int total, int* types, bool* ins);
	public:
		~Envelope();
	public:
		//由XML文件解析出信封对象
		static Envelope* Load(cstr8 path, int fid);
		
		//由XML字符串解析出信封对象
		//要求输入params节点
		static Envelope* Parse(cstr8 xml);
	private:
		//由XML对象解析出信封对象
		//要求输入params节点
		static Envelope* Parse(object xml);
	private:
		int _Length;
		int _Total;
		object _Memory;
		int*	_Types;
		bool* _InStates;
		int* _Offsets;
	private:
		inline bool AssertId(int id, Envelope* env = null)
		{
			if(id < 1)
				return false;
			return env == null ? id <= _Total : id <= env->_Total;
		}
	private:
		//求取标识对应参数的内存地址
		uint64 _AddrOf(int id);
	public:
		//获取参数数量
		int GetTotal();

		//获取参数方向
		//true - 输入 | false - 输出
		bool GetInState(int id);

		//获取参数类型
		int GetType(int id);
	public:
		//写入对象
		//size>0时浅拷贝对象数据
		void WriteEx(int id, object value, int size = 0);

		//读出参数
		template<typename T>
		T Read(int id)
		{
			if(!AssertId(id))
				throw invalid_argument(_vf_err_1);
			T* param = (T*)_AddrOf(id);
			return param[0];
		}

		//读出参数，特化bool
		template<>
		bool Read<bool>(int id)
		{
			char ret = Read<char>(id);
			return (ret == 1);
		}

		//读出参数，特化8位字节字符串
		template<>
		cstr8 Read<cstr8>(int id)
		{
			cstr8 tmp = (cstr8)Read<object>(id);
			cstr8 s8 = str::Copy(tmp);
			return s8;
		}

		//读出参数，特化16位字节字符串
		template<>
		cstr16 Read<cstr16>(int id)
		{
			cstr8 s8 = Read<cstr8>(id);
			cstr16 s16 = str::ToCh16(s8);
			delete s8;
			return s16;
		}

		//写入参数
		template<typename T>
		void Write(int id, T value)
		{
			if(!AssertId(id))
				throw invalid_argument(_vf_err_1);
			T* param = (T*)_AddrOf(id);
			param[0] = value;
		}

		//写入参数，特化bool
		template<>
		void Write<bool>(int id, bool value)
		{
			Write<char>(id, value ? 1 : 0);
		}

		//写入字符串，自动复制
		//务必写入UTF8编码的字符串
		template<>
		void Write<cstr8>(int id, cstr8 value)
		{
			WriteEx(id, (object)value, strlen(value) + 1);
		}

		//写入字符串，自动复制
		template<>
		void Write<cstr16>(int id, cstr16 value)
		{
			cstr8 s8 = str::ToCh8(value, _vf_msg_cp);
			Write(id, s8);
			delete s8;
		}

		//读出数值并自动转型到字符串
		cstr8 CastRead(int id);

		//由字符串自动转型到数值并写入
		void CastWrite(int id, cstr8 value);

		//投递当前信封到目标
		//要求类型完全一致
		//投递不会复制对象数据
		void Deliver(Envelope* who, int from, int to);

		//投递当前信封到目标
		//具备自动类型转换但效率较低
		//投递不会复制对象数据
		void CastDeliver(Envelope* who, int from, int to);
	};
}