#pragma once

#include "tcm_base.h"

namespace tcm
{
	class Envelope;
	class Invoker;
	class Driver;

	//功能库基类
	class TCM_BRIDGE_API Library
	{
	protected:
		Library();
	public:
		virtual ~Library();
	protected:
		strw _Dir; //库目录
		strw _LibId; //库标识
	protected:
		static int _Count;
	public:
		//根据配置加载库
		static Library* Load(strw path);
		
		//获取已装载库数量
		static int GetCount();
	public:
		//获取当前库标识
		strw GetLibraryId();

		//创建指定功能的参数信封
		Envelope* CreateEnvelope(int fid);

		//获取指定功能的调用器
		Invoker* CreateInvoker(int fid);
	public:
		//获取环境标识
		virtual str GetRuntimeId() = 0;

		//获取物理库扩展名
		virtual strw GetBinExt() = 0;

		//装载库
		virtual bool Mount();

		//卸载库
		virtual void Unmount();
	};
}