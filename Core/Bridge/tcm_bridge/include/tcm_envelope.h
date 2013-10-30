#pragma once

#include "tcm_base.h"

namespace tcm
{
	using std::invalid_argument;
	const PCSTR _tcm_env_err_1 = "try to access null param";
	const PCSTR _tcm_env_err_2 = "try to write null value";
	const PCSTR _tcm_env_err_3 = "try to deliver between different types";

	//参数信封
	class TCM_BRIDGE_API Envelope
	{
	private:
		Envelope(int total, int* types, bool* ins);
	public:
		~Envelope();
	public:
		//由XML文件解析出信封对象
		static Envelope* Load(PCWSTR path, int fid);
		
		//由XML字符串解析出信封对象
		//要求输入params节点
		static Envelope* Parse(PCSTR xml);
	private:
		//由XML对象解析出信封对象
		//要求输入params节点
		static Envelope* Parse(LPVOID xml);
	private:
		int _Length;
		int _Total;
		LPVOID _Memory;
		int*	_Types;
		bool* _InStates;
		int* _Offsets;
	private:
		inline bool AssertId(int id, Envelope* env = NULL)
		{
			if(id < 1) return false;
			return env == NULL ? id <= _Total : id <= env->_Total;
		}
	public:
		//获取参数数量
		int GetParamTotal();

		//获取参数方向
		//true - 输入 | false - 输出
		bool GetInState(int id);
	public:
		//读出参数
		template<typename T>
		T Read(int id)
		{
			if(!AssertId(id))
				throw invalid_argument(_tcm_env_err_1);
			T* param = (T*)((UINT)_Memory + _Offsets[id - 1]);
			return param[0];
		}

		//读出参数，特化bool
		template<>
		bool Read<bool>(int id)
		{
			char ret = Read<char>(id);
			return (ret == TRUE);
		}

		//写入参数
		template<typename T>
		void Write(int id, T value)
		{
			if(!AssertId(id)) throw invalid_argument(_tcm_env_err_1);
			T* param = (T*)((UINT)_Memory + _Offsets[id - 1]);
			param[0] = value;
		}

		//写入参数，特化bool
		template<>
		void Write<bool>(int id, bool value)
		{
			Write<char>(id, value ? TRUE : FALSE);
		}

		//写入对象
		//size>0时浅拷贝对象数据
		void Write(int id, LPVOID value, int size = 0)
		{
			if(!AssertId(id)) throw invalid_argument(_tcm_env_err_1);
			LPVOID* param = (LPVOID*)((UINT)_Memory + _Offsets[id - 1]);
			if(size > 0)
			{
				//浅拷贝
				param[0] = (LPVOID)(new BYTE[size]);
				memcpy(param[0], value, size);
			}
			else
			{
				//引用复制
				param[0] = value;
			}
		}

		//写入字符串，自动复制
		void Write(int id, PCSTR value)
		{
			Write(id, (LPVOID)value, strlen(value) + 1);
		}

		//写入宽字符串，自动复制
		void Write(int id, PCWSTR value)
		{
			Write(id, (LPVOID)value, wcslen(value) * 2 + 2);
		}

		//读出数值并自动转型到字符串
		PCSTR CastReadA(int id);
		PCWSTR CastReadW(int id);

		//由字符串自动转型到数值并写入
		void CastWriteA(int id, PCSTR value);
		void CastWriteW(int id, PCWSTR value);

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