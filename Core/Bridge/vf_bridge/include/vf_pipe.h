#pragma once

#include "vf_base.h"

#define VF_PIPE_DATASIZE 1024
#define VF_PIPE_PRTCSIZE 15

namespace vapula
{
	//full-duplex data pipe
	class VAPULA_API Pipe
	{
	public:
		Pipe();
		~Pipe();
	private:
		pcstr _Id;
		raw _Data;
		raw _Mapping;
		uint32 _Volume;
 		bool _IsServer;

	//physical
	private:
		bool _CreateMapping(uint32 vol);
		void _CloseMapping();
		bool _BeginUpdate();
		void _EndUpdate();
	
	//action
	private:
		uint8 _GetFlag(uint32 offset);
		void _SetFlag(uint32 offset, uint8 value);
		uint32 _GetValue(uint32 offset);
		void _SetValue(uint32 offset, uint32 value);
		void _Write(raw data, uint32 len);
		raw _Read();

	//link
	public: 
		//get pipe id
		pcstr GetPipeId();

		//get pipe volume
		int GetVolume();
		
		//test if pipe is closed
		bool IsClose();

		//listen pipe
		//vol = volume
		bool Listen(uint32 vol = VF_PIPE_DATASIZE);

		//connect pipe
		bool Connect(pcstr pid);

		//close pipe
		void Close();

	//utility
	public:
		//test if has new data
		bool HasNewData();

		//get size of data to read
		uint32 GetReadSize();

		//write data
		void Write(pcstr data);

		//read data
		pcstr Read();
	};
}