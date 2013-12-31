#include "vf_pipe.h"

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
		int ntry = 0;
		do
		{
			Clear(_Id);
			if(ntry++ > 10) return false;
			_Id = GetLUID(true);
			cstr16 tmp = str::ToCh16(_Id);
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

	void Pipe::_Write(object data, uint32 len)
	{
		if(_IsServer)
		{
			((uint32*)((uint32)_Data + 7))[0] = len;
			memcpy((object)((uint32)_Data + VF_PIPE_PRTCSIZE), data, len);
			_SetFlag(1, 1);
		}
		else
		{
			((uint32*)((uint32)_Data + 11))[0] = len;
			memcpy((object)((uint32)_Data + VF_PIPE_PRTCSIZE + _Volume), data, len);
			_SetFlag(2, 1);
		}
	}

	object Pipe::_Read()
	{
		object data = null;
		uint32 len = 0;
		if(_IsServer)
		{
			len = _GetValue(11);
			data = new byte[len];
			memcpy(data, (object)((uint32)_Data + VF_PIPE_PRTCSIZE + _Volume), len);
			_SetFlag(2, 0);
		}
		else
		{
			len = _GetValue(7);
			data = new byte[len];
			memcpy(data, (object)((uint32)_Data + VF_PIPE_PRTCSIZE), len);
			_SetFlag(1, 0);
		}
		return data;
	}

	cstr8 Pipe::GetPipeId()
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

	bool Pipe::Connect(cstr8 pid)
	{
		Close();
		_IsServer = false;
		_Id = str::Copy(pid);
		cstr16 tmp = str::ToCh16(_Id);
		_Mapping = OpenFileMapping(FILE_MAP_READ|FILE_MAP_WRITE, FALSE, tmp);
		delete tmp;
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

	void Pipe::Write(cstr8 data)
	{
		uint32 len = strlen(data) + 1;
		_Write((object)data, len);
	}

	cstr8 Pipe::Read()
	{
		cstr8 data = (cstr8)_Read();
		return data;
	}
}