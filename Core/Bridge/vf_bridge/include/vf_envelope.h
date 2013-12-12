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
		static Envelope* Load(cstrw path, int fid);
		
		//由XML字符串解析出信封对象
		//要求输入params节点
		static Envelope* Parse(cstr xml);
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

		//不允许以多字节字符串形式读取参数
		template<>
		cstr Read<cstr>(int)
		{
			throw invalid_argument(_vf_err_4);
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
		//多字节字符串首先转换成宽字节字符串
		template<>
		void Write<cstr>(int id, cstr value)
		{
			cstrw strw = MbToWc(value);
			WriteEx(id, (object)strw, wcslen(strw) * 2 + 2);
			delete strw;
		}

		//写入字符串，自动复制
		template<>
		void Write<cstrw>(int id, cstrw value)
		{
			WriteEx(id, (object)value, wcslen(value) * 2 + 2);
		}

		//读出数值并自动转型到字符串
		cstr CastReadA(int id);
		cstrw CastReadW(int id);

		//由字符串自动转型到数值并写入
		void CastWriteA(int id, cstr value);
		void CastWriteW(int id, cstrw value);

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