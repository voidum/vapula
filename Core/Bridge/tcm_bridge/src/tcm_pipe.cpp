#include "stdafx.h"
#include "tcm_pipe.h"

namespace tcm
{
	Pipe::Pipe()
	{
		_Mapping = NULL;
		_Id = NULL;
		_Data = NULL;
		_DataVol = 0;
	}

	Pipe::~Pipe()
	{
		CloseMMF();
	}

	bool Pipe::CreateMMF(UINT vol)
	{
		std::wstring id;
		std::wstring str;
		int trytimes = 0;
		do
		{
			if(trytimes++ > 20) return false;
			PCWSTR tmp = GetRandomHexW(6); id = tmp; delete tmp;
			id += L"_";
			tmp = GetTimeStrW(); id += tmp; delete tmp;
			str = id + L"_TCM_PIPE";
			_Mapping = CreateFileMapping(INVALID_HANDLE_VALUE,NULL,PAGE_READWRITE,0,vol,str.c_str());
		} while (GetLastError() != ERROR_SUCCESS);
		_Id = CopyStrW(id.c_str());
		return true;
	}

	void Pipe::CloseMMF()
	{
		if(_IsServer) SetFlag(TCM_PIPE_GLOBAL, 0);
		EndUpdate();
		if(_Mapping != NULL)
		{
			CloseHandle(_Mapping);
			_Mapping = NULL;
		}
		if(_Id != NULL)
		{
			delete _Id;
			_Id = NULL;
		}
	}

	bool Pipe::BeginUpdate()
	{
		if(_Mapping == NULL) return false;
		_Data = MapViewOfFile(_Mapping, FILE_MAP_READ|FILE_MAP_WRITE, 0, 0, 0);
		return (_Data != NULL);
	}

	void Pipe::EndUpdate()
	{
		if(_Data != NULL) UnmapViewOfFile(_Data);
	}

	PCWSTR Pipe::GetPipeId()
	{
		return _Id;
	}

	int Pipe::GetDataVol()
	{
		return _DataVol;
	}

	BYTE Pipe::GetFlag(int action)
	{
		if(_Data == NULL) return false;
		BYTE flag = ((BYTE*)_Data)[action];
		return flag;
	}

	void Pipe::SetFlag(int action, int value)
	{
		if(_Data == NULL) return;
		((BYTE*)_Data)[action] = value;
	}

	bool Pipe::Listen(UINT vol)
	{
		if(vol < 2) return false;
		CloseMMF();
		_IsServer = true;
		_DataVol = vol;
		UINT size = vol * 2 + TCM_MMF_PRTCSIZE;
		if(!CreateMMF(size)) return false;
		if(!BeginUpdate()) return false;
		return true;
	}

	bool Pipe::Connect(PCWSTR pid)
	{
		CloseMMF();
		_IsServer = false;
		std::wstring str = pid;
		str += L"_TCM_PIPE";
		_Mapping = OpenFileMapping(FILE_MAP_READ|FILE_MAP_WRITE, FALSE, str.c_str());
		if(GetLastError() != ERROR_SUCCESS) return false;
		if(!BeginUpdate()) return false;
		_Id = CopyStrW(pid);
		return true;
	}

	void Pipe::Write(LPVOID value, UINT size)
	{
		//if(size > _DataVol) return;
		//TODO: Connect方式无法确定Vol，可以调整协议以适应容量全不定MMF
		if(_IsServer)
		{
			((UINT*)((UINT)_Data + TCM_PIPE_S2C + 1))[0] = size;
			memcpy((LPVOID)((UINT)_Data + TCM_MMF_PRTCSIZE), value, size);
			SetFlag(TCM_PIPE_S2C, 1);
		}
		else
		{
			((UINT*)((UINT)_Data + TCM_PIPE_C2S + 1))[0] = size;
			memcpy((LPVOID)((UINT)_Data + TCM_MMF_PRTCSIZE + TCM_MMF_DATASIZE), value, size);
			SetFlag(TCM_PIPE_C2S, 1);
		}
	}

	void Pipe::Read(LPVOID data)
	{
		if(_IsServer)
		{
			UINT size = ((UINT*)((UINT)_Data + TCM_PIPE_C2S + 1))[0];
			memcpy(data, (LPVOID)((UINT)_Data + TCM_MMF_PRTCSIZE + TCM_MMF_DATASIZE), size);
			SetFlag(TCM_PIPE_C2S, 0);
		}
		else
		{
			UINT size = ((UINT*)((UINT)_Data + TCM_PIPE_S2C + 1))[0];
			memcpy(data, (LPVOID)((UINT)_Data + TCM_MMF_PRTCSIZE), size);
			SetFlag(TCM_PIPE_S2C, 0);
		}
	}

	UINT Pipe::GetReadSize()
	{
		UINT size = 0;
		if(_IsServer)
			size = ((UINT*)((UINT)_Data + TCM_PIPE_C2S + 1))[0];
		else
			size = ((UINT*)((UINT)_Data + TCM_PIPE_S2C + 1))[0];
		return size;
	}

	LPVOID Pipe::WaitRead(int time)
	{
		int times = time / 50;
		times = (times == 0 ? 1 : times);
		int i = 0;
		while(time == 0 || i < times) //time为0表示持续等待
		{
			BYTE flag = GetFlag(_IsServer ? TCM_PIPE_C2S : TCM_PIPE_S2C);
			if(flag == 0x01)
			{
				LPVOID data = new BYTE[GetReadSize()];
				Read(data);
				return data;
			}
			i++;
			Sleep(50);
		}
		return NULL;
	}

	bool Pipe::BeenRead(int time)
	{
		int times = time / 50;
		times = (times == 0 ? 1 : times);
		int i = 0;
		while(time == 0 || i < times) //time为0表示持续等待
		{
			BYTE flag = GetFlag(_IsServer ? TCM_PIPE_S2C : TCM_PIPE_C2S);
			if(flag == 0x00) return true;
			i++;
			Sleep(50);
		}
		return false;
	}
}