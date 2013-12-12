#pragma once

#include "vf_base.h"

namespace vapula
{
	class Envelope;
	class Invoker;
	class Driver;

	//功能库基类
	class VAPULA_API Library
	{
	protected:
		Library();
	public:
		virtual ~Library();
	protected:
		cstrw _Dir; //库目录
		cstrw _LibId; //库标识
	protected:
		static int _Count;
	public:
		//根据配置加载库
		static Library* Load(cstrw path);
		
		//获取已装载库数量
		static int GetCount();
	public:
		//获取当前库标识
		cstrw GetLibraryId();

		//创建指定功能的参数信封
		Envelope* CreateEnvelope(int fid);

		//获取指定功能的调用器
		Invoker* CreateInvoker(int fid);
	public:
		//获取环境标识
		virtual cstr GetRuntimeId() = 0;

		//获取物理库扩展名
		virtual cstrw GetBinExt() = 0;

		//装载库
		virtual bool Mount();

		//卸载库
		virtual void Unmount();
	};
}