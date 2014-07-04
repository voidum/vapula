#include "vf_pipe.h"
#include "vf_runtime.h"

namespace vapula
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
		int tried = 0;
		Runtime* runtime = Runtime::Instance();
		do
		{
			Clear(_Id);
			if(tried++ > 10)
				return false;
			_Id = runtime->NewLUID();
			pcwstr cs16_id = str::ToStrW(_Id);
			_Mapping = CreateFileMapping(
				INVALID_HANDLE_VALUE, NULL, PAGE_READWRITE, 
				0, vol, cs16_id);
			delete cs16_id;
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
		if(_Data == null)
		{
			//int ret = GetLastError();
			return false;
		}
		return true;
	}

	void Pipe::_EndUpdate()
	{
		if(_Data != null) 
			UnmapViewOfFile(_Data);
	}

	uint8 Pipe::_GetFlag(uint32 offset)
	{
		if(_Data == null)
			return false;
		uint8 v = ((uint8*)((uint32)_Data + offset))[0];
		return v;
	}

	void Pipe::_SetFlag(uint32 offset, uint8 value)
	{
		if(_Data != null) 
			((uint8*)((uint32)_Data + offset))[0] = value;
	}

	uint32 Pipe::_GetValue(uint32 offset)
	{
		if(_Data == null)
			return false;
		uint32 v = ((uint32*)((uint32)_Data + offset))[0];
		return v;
	}

	void Pipe::_SetValue(uint32 offset, uint32 value)
	{
		if(_Data != null) 
			((uint32*)((uint32)_Data + offset))[0] = value;
	}

	pcstr Pipe::GetPipeId()
	{
		return _Id;
	}

	int Pipe::GetVolume()
	{
		return _Volume;
	}

	bool Pipe::IsClose()
	{
		return _GetFlag(0) == 0;
	}

	bool Pipe::Listen(uint32 vol)
	{
		if(vol < 1) 
			return false;
		Close();
		_IsServer = true;
		_Volume = vol;
		uint32 size = vol * 2 + VF_PIPE_PRTCSIZE;
		if(!_CreateMapping(size))
			return false;
		if(!_BeginUpdate())
			return false;
		_SetFlag(0, 1);
		return true;
	}

	bool Pipe::Connect(pcstr pid)
	{
		Close();
		_IsServer = false;
		_Id = str::Copy(pid);
		pcwstr cs16_id = str::ToStrW(_Id);
		_Mapping = OpenFileMapping(
			FILE_MAP_READ|FILE_MAP_WRITE, FALSE, cs16_id);
		if(GetLastError() != ERROR_SUCCESS)
			return false;
		if(!_BeginUpdate()) 
			return false;
		_Volume = _GetValue(2);
		return true;
	}

	void Pipe::Close()
	{
		if(_IsServer) 
			_SetFlag(0, 0);
		_CloseMapping();
		Clear(_Id);
	}

	bool Pipe::HasNewData()
	{
		uint8 flag = _GetFlag(_IsServer ? 2 : 1);
		return (flag != 0);
	}

	uint32 Pipe::GetReadSize()
	{
		uint32 size = _GetValue(_IsServer ? 7 : 11);
		return size;
	}


	void Pipe::Write(raw data, uint32 size)
	{
		if (_IsServer)
		{
			((uint32*)((uint32)_Data + 7))[0] = size;
			memcpy((raw)((uint32)_Data + VF_PIPE_PRTCSIZE), data, size);
			_SetFlag(1, 1);
		}
		else
		{
			((uint32*)((uint32)_Data + 11))[0] = size;
			memcpy((raw)((uint32)_Data + VF_PIPE_PRTCSIZE + _Volume), data, size);
			_SetFlag(2, 1);
		}
	}

	raw Pipe::Read()
	{
		raw data = null;
		uint32 size = 0;
		if (_IsServer)
		{
			size = _GetValue(11);
			data = new byte[size];
			memcpy(data, (raw)((uint32)_Data + VF_PIPE_PRTCSIZE + _Volume), size);
			_SetFlag(2, 0);
		}
		else
		{
			size = _GetValue(7);
			data = new byte[size];
			memcpy(data, (raw)((uint32)_Data + VF_PIPE_PRTCSIZE), size);
			_SetFlag(1, 0);
		}
		return data;
	}
}