#include "stdafx.h"
#include "tcm_pipe.h"

namespace tcm
{
	Pipe::Pipe()
	{
		_Mapping = null;
		_Id = null;
		_Data = null;
		_Volume = 0;
	}

	Pipe::~Pipe()
	{
		_CloseMapping();
	}

	bool Pipe::_CreateMapping(uint32 vol)
	{
		int ntry = 0;
		do
		{
			Clear(_Id);
			if(ntry++ > 10) return false;
			_Id = GetLuidA();
			strw tmp = MbToWc(_Id);
			_Mapping = CreateFileMapping(INVALID_HANDLE_VALUE, NULL, PAGE_READWRITE, 0, vol, tmp);
			delete tmp;
		} while (GetLastError() != ERROR_SUCCESS);
		return true;
	}

	void Pipe::_CloseMapping()
	{
		_EndUpdate();
		if(_Mapping != null)
		{
			CloseHandle(_Mapping);
			_Mapping = null;
		}
	}

	bool Pipe::_BeginUpdate()
	{
		if(_Mapping == null)
			return false;
		_Data = MapViewOfFile(_Mapping, FILE_MAP_READ|FILE_MAP_WRITE, 0, 0, 0);
		return (_Data != null);
	}

	void Pipe::_EndUpdate()
	{
		if(_Data != null) 
			UnmapViewOfFile(_Data);
	}

	uint8 Pipe::GetFlag(uint32 offset)
	{
		if(_Data == null)
			return false;
		uint8 v = *(uint8*)((uint32)_Data + offset);
		return v;
	}

	void Pipe::SetFlag(uint32 offset, uint8 value)
	{
		if(_Data != null) 
			*(uint8*)((uint32)_Data + offset) = value;
	}

	uint32 Pipe::GetValue(uint32 offset)
	{
		if(_Data == null)
			return false;
		uint32 v = *(uint32*)((uint32)_Data + offset);
		return v;
	}

	void Pipe::SetValue(uint32 offset, uint32 value)
	{
		if(_Data != null) 
			*(uint32*)((uint32)_Data + offset) = value;
	}

	str Pipe::GetPipeId()
	{
		return _Id;
	}

	int Pipe::GetVolume()
	{
		return _Volume;
	}

	bool Pipe::Listen(uint32 vol)
	{
		if(vol < 1) 
			return false;
		Close();
		_IsServer = true;
		_Volume = vol;
		uint32 size = vol * 2 + TCM_PIPE_PRTCSIZE;
		if(!_CreateMapping(size))
			return false;
		if(!_BeginUpdate())
			return false;
		SetFlag(0, 1);
		return true;
	}

	bool Pipe::Connect(str pid)
	{
		Close();
		_IsServer = false;
		_Id = CopyStrA(pid);
		strw tmp = MbToWc(_Id);
		_Mapping = OpenFileMapping(FILE_MAP_READ|FILE_MAP_WRITE, FALSE, tmp);
		if(GetLastError() != ERROR_SUCCESS)
			return false;
		if(!_BeginUpdate()) 
			return false;
		_Volume = GetValue(2);
		return true;
	}

	void Pipe::Close()
	{
		if(_IsServer) 
			SetFlag(0, 0);
		_CloseMapping();
		Clear(_Id);
	}

	bool Pipe::HasNewData()
	{
		uint8 flag = GetFlag(_IsServer ? 2 : 1);
		return (flag != 0);
	}

	uint32 Pipe::GetReadSize()
	{
		uint32 size = GetValue(_IsServer ? 7 : 11);
		return size;
	}

	void Pipe::Write(strw data)
	{
		uint32 size = wcslen(data) * 2;
		if(_IsServer)
		{
			*(uint32*)((uint32)_Data + 7) = size;
			memcpy((object)((uint32)_Data + TCM_PIPE_PRTCSIZE), data, size);
			SetFlag(1, 1);
		}
		else
		{
			*(uint32*)((uint32)_Data + 11) = size;
			memcpy((object)((uint32)_Data + TCM_PIPE_PRTCSIZE + _Volume), data, size);
			SetFlag(2, 1);
		}
	}

	strw Pipe::Read()
	{
		strw data;
		uint32 size = 0;
		if(_IsServer)
		{
			size = GetValue(11);
			memcpy((object)data, (object)((uint32)_Data + TCM_PIPE_PRTCSIZE + _Volume), size);
			SetFlag(2, 0);
		}
		else
		{
			size = GetValue(7);
			memcpy((object)data, (object)((uint32)_Data + TCM_PIPE_PRTCSIZE), size);
			SetFlag(1, 0);
		}
		return data;
	}
}