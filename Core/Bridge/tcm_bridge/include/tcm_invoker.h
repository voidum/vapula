#pragma once

#include "tcm_context.h"
#include "tcm_envelope.h"

namespace tcm
{
	class Token;
	class Library;

	//功能调用器
	class TCM_BRIDGE_API Invoker
	{
	protected:
		Invoker();
	public:
		virtual ~Invoker();
	protected:
		int _FuncId;
		Envelope* _Envelope;
		Context* _Context;
		Token* _ContextToken;
	protected:
		HANDLE _Thread;
		bool _IsSuspend;
		virtual UINT WINAPI _ThreadProc();
	public:
		//获取功能标识
		int GetFunctionId();

		//获取信封
		Envelope* GetEnvelope();

		//获取上下文
		Context* GetContext();

		//获取上下文令牌
		Token* GetContextToken();
	public:
		//启动
		bool Start();

		//停止
		void Stop(UINT wait = 0);

		//暂停
		void Pause(UINT wait = 0);

		//恢复
		void Resume();

		//重启
		void Restart(UINT wait = 0);
	public:
		//初始化调用器
		virtual bool Initialize(Library* lib, int fid); 
	};
}