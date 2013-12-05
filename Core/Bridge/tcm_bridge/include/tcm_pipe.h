#pragma once

#include "tcm_base.h"

#define TCM_PIPE_DATASIZE 1024
#define TCM_PIPE_PRTCSIZE 15

namespace tcm
{
	//全双工跨进程信道
	class TCM_BRIDGE_API Pipe
	{
	public:
		Pipe();
		~Pipe();
	private:
		str _Id;
		object _Data;
		object _Mapping;
		uint32 _Volume;
 		bool _IsServer;

	//物理实现
	private:
		bool _CreateMapping(uint32 vol);
		void _CloseMapping();
		bool _BeginUpdate();
		void _EndUpdate();
	
	//协议
	private:
		uint8 GetFlag(uint32 offset);
		void SetFlag(uint32 offset, uint8 value);
		uint32 GetValue(uint32 offset);
		void SetValue(uint32 offset, uint32 value);

	//链路
	public: 
		//获取信道标识;
		str GetPipeId();

		//获取数据容量
		int GetVolume();
		
		//启动监听
		//可指定信道的数据容量
		bool Listen(uint32 vol = TCM_PIPE_DATASIZE);

		//连接指定的信道
		bool Connect(str pid);

		//关闭信道
		void Close();

	//应用
	public:
		//检查新消息
		bool HasNewData();

		//获取可读有效数据的大小
		uint32 GetReadSize();

		//写入数据
		void Write(strw data);

		//读取数据
		strw Read();
	};
}