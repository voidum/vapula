#pragma once
#pragma warning(disable:4275) //禁用继承未导出警告

#include "tcm_base.h"

namespace tcm
{
	//不可复制
	class Uncopiable
	{
	protected:
		Uncopiable();
		~Uncopiable();
	private:
		Uncopiable(const Uncopiable&);
		Uncopiable& operator=(const Uncopiable&);
	};

	//自旋锁
	class TCM_BRIDGE_API Lock : Uncopiable
	{
	public:
		Lock();
		~Lock();
	public:
		//获得锁
		//适合轻量级操作
		bool Enter();

		//获得锁
		//适合重量级操作
		bool EnterEx();

		//释放锁
		void Leave();

		//设置参数
		void Set(int a, int b, int c);
	private:
		long* _Core; //TRUE - Lock , FALSE - Unlock
		BYTE _A; //Max Decay Times
		UINT16 _B; //Blank Time (ms)
		BYTE _C; //Max Blank Times
	};

	//一次性赋值，Assignable Only Once
	class VarAOO : Uncopiable
	{
	public:
		VarAOO();
		~VarAOO();
	private:
		LPVOID _Value;
		LPVOID _Token;
	private:
		void _Set(LPVOID data, UINT len);
	public:
		bool CanSet();

		template<typename T>
		void Set(T* value)
		{
			if(_Token == NULL) return;
			_Set(value, sizeof(T));
		}

		template<typename T>
		T* Get()
		{
			return (T*)_Value;
		}
	};

	//标志
	class TCM_BRIDGE_API Flag : Uncopiable
	{
	public:
		Flag();
		~Flag();
	private:
		Lock* _Lock;
		int _Value;
	public:
		//使能标志
		void Enable(int flag);

		//失能标志
		void Disable(int flag);

		//校验标志位
		bool Valid(int flag);
	};

	//字典
	class TCM_BRIDGE_API Dictionary : Uncopiable
	{
	public:
		Dictionary();
		~Dictionary();
	private:
		typedef vector<PCWSTR>::iterator iter;
		Lock* _Lock;
		vector<PCWSTR> _Keys;
		vector<PCWSTR> _Values;
	public:
		//获取记录数
		int GetCount();

		//检查记录是否存在
		bool Contain(PCWSTR key);

		//添加记录
		bool Add(PCWSTR key, PCWSTR value);

		//移除记录
		bool Remove(PCWSTR key);

		//获取指定索引的键
		PCWSTR GetKey(UINT id);

		//获取指定索引的值
		PCWSTR GetValue(UINT id);

		//清空字典
		void Clear();

		//根据Key查询记录的Value
		PCWSTR Find(PCWSTR key);
	};
}