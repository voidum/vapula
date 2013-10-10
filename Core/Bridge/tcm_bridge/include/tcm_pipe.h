#pragma once

#include "tcm_base.h"

#define TCM_MMF_DATASIZE 4096
#define TCM_MMF_PRTCSIZE 32

namespace tcm
{
	//信道标志起始索引
	enum PipeFlag
	{
		TCM_PIPE_GLOBAL = 0,
		TCM_PIPE_S2C = 12,
		TCM_PIPE_C2S = 22
	};

	//全双工跨进程信道
	class TCM_BRIDGE_API Pipe
	{
	public:
		Pipe();
		~Pipe();
	private:
		int _DataVol;
		PCWSTR _Id;
		HANDLE _Mapping;
		LPVOID _Data;
 		bool _IsServer;
	private:
		bool CreateMMF(UINT vol);
		void CloseMMF();
		bool BeginUpdate();
		void EndUpdate();
	public:
		//获取信道标识;
		PCWSTR GetPipeId();

		//获取数据容量
		int GetDataVol();
		
		//启动监听，可以指定信道容量
		bool Listen(UINT vol = TCM_MMF_DATASIZE);

		//连接指定的信道
		bool Connect(PCWSTR pid);

		//获取数据标志
		BYTE GetFlag(int action);

		//设置数据标志
		void SetFlag(int action, int value = 0);

		//写入数据
		//将指定位置的指定长度的数据写入数据段
		//发送数据不检查是否已读
		void Write(LPVOID value, UINT size);

		//读取数据
		//将数据复制到指定的位置
		//读取数据不检查是否发送
		void Read(LPVOID data);

		//获取可读有效数据的大小
		UINT GetReadSize();

		//等待读取数据
		LPVOID WaitRead(int time = 0);

		//等待数据已读
		bool BeenRead(int time = 0);
	};
}