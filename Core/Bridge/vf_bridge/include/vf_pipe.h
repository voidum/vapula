#pragma once

#include "vf_base.h"

#define VF_PIPE_DATASIZE 1024
#define VF_PIPE_PRTCSIZE 15

namespace vapula
{
	//ȫ˫��������ŵ�
	class VAPULA_API Pipe
	{
	public:
		Pipe();
		~Pipe();
	private:
		cstr _Id;
		object _Data;
		object _Mapping;
		uint32 _Volume;
 		bool _IsServer;

	//����ʵ��
	private:
		bool _CreateMapping(uint32 vol);
		void _CloseMapping();
		bool _BeginUpdate();
		void _EndUpdate();
	
	//Э��
	private:
		uint8 _GetFlag(uint32 offset);
		void _SetFlag(uint32 offset, uint8 value);
		uint32 _GetValue(uint32 offset);
		void _SetValue(uint32 offset, uint32 value);
		void _Write(object data, uint32 len);
		object _Read();

	//��·
	public: 
		//��ȡ�ŵ���ʶ;
		cstr GetPipeId();

		//��ȡ��������
		int GetVolume();
		
		//����ŵ��Ƿ�ر�
		bool IsClose();

		//��������
		//��ָ���ŵ�����������
		bool Listen(uint32 vol = VF_PIPE_DATASIZE);

		//����ָ�����ŵ�
		bool Connect(cstr pid);

		//�ر��ŵ�
		void Close();

	//Ӧ��
	public:
		//�������Ϣ
		bool HasNewData();

		//��ȡ�ɶ���Ч���ݵĴ�С
		uint32 GetReadSize();

		//д������
		void Write(cstrw data);
		void WriteA(cstr data);

		//��ȡ����
		cstrw Read();
		cstr ReadA();
	};
}