#pragma once

#include "tcm_base.h"

namespace tcm
{
	class Library;
	class Executor;

	//驱动基类
	class TCM_BRIDGE_API Driver
	{
	public:
		Driver();
		virtual ~Driver();
	public:
		//获取运行环境标识
		virtual PCSTR
			GetRuntimeId() = 0;

		//创建组件
		virtual Library*
			CreateLibrary() = 0;

		//创建执行器
		virtual Executor*
			CreateExecutor() = 0;
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
		vector<HMODULE> _Modules;
	private:
		int GetIndex(PCSTR id);
	public:
		//链接驱动
		bool Link(PCSTR id);

		//踢出驱动
		bool Kick(PCSTR id);

		//踢出所有驱动
		void KickAll();
	public:
		//获取驱动
		Driver* GetDriver(PCSTR id);

		//获取已接驳驱动数量
		int GetCount();
	};
}