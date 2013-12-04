#pragma once

#include "tcm_base.h"

#define TCM_MMF_DATASIZE 4096
#define TCM_MMF_PRTCSIZE 32

namespace tcm
{
	//全双工跨进程信道
	class TCM_BRIDGE_API Pipe
	{
	public:
		Pipe();
		~Pipe();
	private:
		int _DataVol;
		str _Id;
		HANDLE _Mapping;
		object _Data;
 		bool _IsServer;
	private:
		bool CreateMMF(UINT vol);
		void CloseMMF();
		bool BeginUpdate();
		void EndUpdate();
	public:
		//获取信道标识;
		str GetPipeId();

		//获取数据容量
		int GetDataVol();
		
		//启动监听，可以指定信道容量
		bool Listen(UINT vol = TCM_MMF_DATASIZE);

		//连接指定的信道
		bool Connect(str pid);

		//检查新消息
		bool HasNewData();

		//写入数据
		void Write(strw data);

		//读取数据
		//同时可以获取数据标识
		strw Read(int* id);
	private:
		//获取数据标志
		BYTE GetFlag(int action);

		//设置数据标志
		void SetFlag(int action, int value = 0);

		//获取可读有效数据的大小
		UINT GetReadSize();

		//等待读取数据
		LPVOID WaitRead(int time = 0);

		//等待数据已读
		bool BeenRead(int time = 0);
	};
}