#pragma once

#pragma warning(disable:4100)

#include "vf_base.h"

namespace vapula
{
	//参数信封
	class VAPULA_API Envelope
	{
	private:
		Envelope(int32 total);
	public:
		~Envelope();

	public:
		//由XML字符串解析出信封对象
		//要求输入params节点
		static Envelope* Parse(cstr8 xml);

		//由XML对象解析出信封对象
		//要求输入params节点
		static Envelope* Parse(object xml);

	private:
		int32 _Total; //参数总数
		int8* _Types; //参数类型
		int8* _Modes; //参数模式
		uint32* _Addrs; //内存地址表
		uint32* _Lengths; //参数长度表

	private:
		bool _AssertId(int id, Envelope* env = null);
		object _Read(int idx, uint32 size, bool copy);
		void _Write(int idx, object value, uint32 size, bool clear, bool copy);

	public:
		//获取参数总数
		int32 GetTotal();

		//获取指定参数的模式
		int8 GetMode(int id);

		//获取指定参数的类型
		int8 GetType(int id);

		//获取指定参数的长度
		uint32 GetLength(int id);

	public:
		Envelope* Copy();

	public:
		//读取内存块
		//可选是否复制，默认不复制
		object ReadObject(int id, uint32* size = null, bool copy = false);

		//写入内存块
		//可选是否复制，默认不复制
		void WriteObject(int id, object value, uint32 size, bool copy = false);

	public:
		//读出参数数组
		//可选是否复制，默认不复制
		template<typename T>
		T* ReadArray(int id, uint32* length = null, bool copy = false)
		{
			if(!_AssertId(id))
				throw invalid_argument(_vf_err_1);
			int idx = id - 1;
			if(length != null)
				*length = _Lengths[idx];
			T* data = (T*)_Read(idx, _Lengths[idx] * sizeof(T), copy);
			return data;
		}

		//读出参数值
		template<typename T>
		T ReadValue(int id)
		{
			T* data = ReadArray<T>(id);
			return data[0];
		}

		//写入参数数组
		template<typename T>
		void WriteArray(int id, T* value, uint32 length, bool copy = false)
		{
			if(!_AssertId(id))
				throw invalid_argument(_vf_err_1);
			int idx = id - 1;

			bool clear = (value != null && _Lengths[idx] != length);
			_Write(idx, value, length * sizeof(T), clear, copy);
			_Lengths[idx] = length;
		}

		//写入参数值
		template<typename T>
		void WriteValue(int id, T value)
		{
			T tmp_value = value;
			WriteArray(id, &tmp_value, 1, true);
		}

	public:
		//特化8位字节字符串，读出参数值
		cstr8 ReadCh8(int id);

		//特化16位字节字符串，读出参数值
		cstr16 ReadCh16(int id);

		//特化8位字节字符串，写入参数值
		void WriteCh8(int id, cstr8 value);
		
		//特化16位字节字符串，写入参数值
		void WriteCh16(int id, cstr16 value);

	public:
		//读出数值并自动转型到字符串
		cstr8 CastReadValue(int id);

		//由字符串自动转型到数值并写入
		void CastWriteValue(int id, cstr8 value);

		//投递当前信封到目标
		void Deliver(Envelope* who, int from, int to);

		//自适应转型投递当前信封到目标
		void CastDeliver(Envelope* who, int from, int to);
	};
}