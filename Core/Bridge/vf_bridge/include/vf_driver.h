#pragma once

#include "vf_base.h"

namespace vf
{
	class Library;
	class Invoker;

	//驱动基类
	class TCM_BRIDGE_API Driver
	{
	public:
		Driver();
		virtual ~Driver();
	public:
		//获取运行环境标识
		virtual str
			GetRuntimeId() = 0;

		//创建组件
		virtual Library*
			CreateLibrary() = 0;

		//创建调用器
		virtual Invoker*
			CreateInvoker() = 0;
	};

	//驱动板
	class TCM_BRIDGE_API DriverHub
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
		int GetIndex(str id);
	public:
		//链接驱动
		bool Link(str id);

		//踢出驱动
		bool Kick(str id);

		//踢出所有驱动
		void KickAll();
	public:
		//获取驱动
		Driver* GetDriver(str id);

		//获取已接驳驱动数量
		int GetCount();
	};
}