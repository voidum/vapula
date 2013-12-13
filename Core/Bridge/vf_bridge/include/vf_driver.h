#pragma once

#include "vf_base.h"

namespace vapula
{
	class Library;
	class Invoker;

	//驱动基类
	class VAPULA_API Driver
	{
	public:
		Driver();
		virtual ~Driver();
	public:
		//获取运行环境标识
		virtual cstr8
			GetRuntimeId() = 0;

		//创建组件
		virtual Library*
			CreateLibrary() = 0;

		//创建调用器
		virtual Invoker*
			CreateInvoker() = 0;
	};

	//驱动板
	class VAPULA_API DriverHub
	{
	private:
		DriverHub();
	public:
		~DriverHub();
	private:
		static DriverHub* _Instance;
	public:
		//获取驱动板实例
		static DriverHub* GetInstance();
	private:
		vector<Driver*> _Drivers;
		vector<object> _Modules;
	private:
		int GetIndex(cstr8 id);
	public:
		//链接驱动
		bool Link(cstr8 id);

		//踢出驱动
		bool Kick(cstr8 id);

		//踢出所有驱动
		void KickAll();
	public:
		//获取驱动
		Driver* GetDriver(cstr8 id);

		//获取已接驳驱动数量
		int GetCount();
	};
}