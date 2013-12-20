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
		inline bool _AssertId(int id, Envelope* env = null)
		{
			if(id < 1)
				return false;
			if(env == null)
				return id <= _Total;
			else
				return id <= env->_Total;
		}

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
		//读取内存块
		//可选是否复制
		object ReadObject(int id, uint32& size, bool copy = false);

		//写入内存块
		//必须设置内存块大小，可选是否复制
		void WriteObject(int id, object value, uint32 size, bool copy = false);

	public:
		//读出参数数组
		//可选是否复制
		template<typename T>
		T* Read(int id, uint32& length, bool copy = false)
		{
			if(!_AssertId(id))
				throw invalid_argument(_vf_err_1);
			int idx = id - 1;
			length = _Lengths[idx];
			if(!copy)
				return (T*)(_Addrs[idx]);
			T* data = new T[length];
			memcpy(data, (object)(_Addrs[idx]), length * sizeof(T));
			return data;
		}

		//读出参数值
		template<typename T>
		T Read(int id)
		{
			if(!_AssertId(id))
				throw invalid_argument(_vf_err_1);
			int idx = id - 1;
			T* param = (T*)(_Addrs[idx]);
			return param[0];
		}

		//写入参数数组
		template<typename T>
		void Write(int id, T* value, uint32 length)
		{
			if(!_AssertId(id))
				throw invalid_argument(_vf_err_1);
			int idx = id - 1;
			//内源数据不处理
			if(value == _Addrs[idx])
				return;
			//空输入表示清除数据
			if(value == null)
			{
				delete (object)(_Addrs[idx]);
				return;
			}
			//外源数据复制
			if(_Lengths[idx] != length)
			{
				if(_Addrs[idx] != 0)
					delete (object)(_Addrs[idx]);
				_Addrs[idx] = new T[length];
				_Lengths[idx] = length;
			}
			memcpy(_Addrs[idx], value, length * sizeof(T));
		}

		//写入参数值
		template<typename T>
		void Write(int id, T value)
		{
			if(!_AssertId(id))
				throw invalid_argument(_vf_err_1);
			int idx = id - 1;
			object data = null;
			if(_Lengths[idx] != 1)
			{
				if(_Addrs[idx] != 0)
					delete (object)(_Addrs[idx]);
				data = new T[1];
				_Lengths[idx] = 1;
			}
			T* param = (T*)(data);
			param[0] = value;
			_Addrs[idx] = (uint32)data;
		}

	public:
		//该接口不适用内存块
		template<>
		object Read<object>(int id)
		{
			throw new bad_exception(_vf_err_2);
		}

		//该接口不适用内存块
		template<>
		object* Read<object>(int id, uint32& length, bool copy)
		{
			throw new bad_exception(_vf_err_2);
		}

		//该接口不适用内存块
		template<>
		void Write<object>(int id, object* value, uint32 length)
		{
			throw new bad_exception(_vf_err_2);
		}

		//该接口不适用内存块
		template<>
		void Write<object>(int id, object value)
		{
			throw new bad_exception(_vf_err_2);
		}

	public:
		//特化8位字节字符串，读出参数值
		template<>
		cstr8 Read<cstr8>(int id)
		{
			uint32 size = 0;
			cstr8 tmp = (cstr8)ReadObject(id, size, true);
			return s8;
		}

		//特化16位字节字符串，读出参数值
		template<>
		cstr16 Read<cstr16>(int id)
		{
			uint32 size = 0;
			cstr8 tmp = (cstr8)ReadObject(id, size, false);
			cstr16 s16 = str::ToCh16(s8, _vf_msg_cp);
			return s16;
		}

		//特化8位字节字符串，写入参数值
		template<>
		void Write<cstr8>(int id, cstr8 value)
		{
			WriteObject(id, value, strlen(value) + 1, true);
		}

		//特化16位字节字符串，写入参数值
		template<>
		void Write<cstr16>(int id, cstr16 value)
		{
			cstr8 s8 = str::ToCh8(value, _vf_msg_cp);
			Write(id, s8);
			delete s8;
		}

	public:
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