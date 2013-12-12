#pragma once

#include "vf_context.h"
#include "vf_envelope.h"

namespace vapula
{
	class Library;

	//功能调用器
	class VAPULA_API Invoker
	{
	protected:
		Invoker();
	public:
		virtual ~Invoker();
	protected:
		int _FuncId;
		Envelope* _Envelope;
		Context* _Context;
	protected:
		object _Thread;
		bool _IsSuspend;
		virtual uint32 WINAPI _ThreadProc();
	public:
		//获取功能标识
		int GetFunctionId();

		//获取信封
		Envelope* GetEnvelope();

		//获取上下文
		Context* GetContext();
	public:
		//启动
		bool Start();

		//停止
		void Stop(uint32 wait = 0);

		//暂停
		void Pause(uint32 wait = 0);

		//恢复
		void Resume();

		//重启
		void Restart(uint32 wait = 0);
	public:
		//初始化调用器
		virtual bool Initialize(Library* lib, int fid); 
	};
}