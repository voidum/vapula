#include "vf_base.h"
#include "vf_stack.h"
#include "vf_setting.h"
#include "modp_b64\modp_b64.h"

namespace vapula
{
	Lock::Lock()
	{
		_Core = (uint64*)_aligned_malloc(1, sizeof(uint64));
		InterlockedExchange(_Core, FALSE);
	}

	Lock::~Lock()
	{
		InterlockedExchange(_Core, FALSE);
		_aligned_free(_Core);
	}

	void Lock::Enter()
	{
		while(InterlockedExchange(_Core, TRUE) == TRUE)
			Sleep(0);
	}

	void Lock::Leave()
	{
		InterlockedExchange(_Core, FALSE);
	}

	Once::Once()
	{
		_Lock = new Lock();
		_Seal = new byte[1];
		_Data = null;
	}

	Once::~Once()
	{
		Clear(_Seal);
		Clear(_Data);
		delete _Lock;
	}

	bool Once::CanSet()
	{
		return (_Seal != null);
	}

	void Once::Set(raw data, uint32 size)
	{
		_Lock->Enter();
		if(!CanSet())
			return;
		_Data = new byte[size];
		_Lock->Leave();
		memcpy(_Data, data, size);
		delete _Seal;
	}

	Flag::Flag()
	{
		_Lock = new Lock();
		_Value = 0;
	}

	Flag::~Flag()
	{
		delete _Lock;
	}

	void Flag::Enable(int flag)
	{
		_Lock->Enter();
		_Value |= flag;
		_Lock->Leave();
	}

	void Flag::Disable(int flag)
	{
		int tmp = flag ^ 0xFFFFFFFF;
		_Lock->Enter();
		_Value &= tmp;
		_Lock->Leave();
	}

	bool Flag::Valid(int flag)
	{
		_Lock->Enter();
		int v = _Value;
		_Lock->Leave();
		return ((v & flag) == flag);
	}

	pcstr RawToBase64(raw data, uint32 size)
	{
		uint32 dst_size = modp_b64_encode_len(size);
		pstr dst = new char[dst_size];
		int ret = modp_b64_encode(dst, (pcstr)data, size);
		if (ret == -1)
		{
			delete dst;
			dst = null;
		}
		return dst;
	}

	raw Base64ToRaw(pcstr data)
	{
		uint32 src_size = strlen(data);
		uint32 dst_size = modp_b64_decode_len(src_size);
		pstr dst = new char[dst_size];
		int ret = modp_b64_decode(dst, data, src_size);
		if (ret == -1)
		{
			delete dst;
			dst = null;
		}
		return dst;
	}

	uint32 GetValueUnit(uint8 type)
	{
		switch (type)
		{
		case VF_VALUE_INT8:
		case VF_VALUE_UINT8:
			return 1;
		case VF_VALUE_INT16:
		case VF_VALUE_UINT16:
			return 2;
		case VF_VALUE_INT32:
		case VF_VALUE_UINT32:
		case VF_VALUE_REAL32:
			return 4;
		case VF_VALUE_INT64:
		case VF_VALUE_UINT64:
		case VF_VALUE_REAL64:
			return 8;
		default:
			return 0;
		}
	}

	void ShowMsgbox(pcstr value, pcstr caption)
	{
		Setting* setting = Setting::Instance();
		if(!setting->IsSilent())
			MessageBoxA(null, value, 
			caption == null ? _vf_bridge : caption, 0);
	}

	void ShowMsgbox(pcwstr value, pcwstr caption)
	{
		Setting* setting = Setting::Instance();
		if(!setting->IsSilent())
			MessageBoxW(null, value,
			caption == null ? str::ToStrW(_vf_bridge) : caption, 0);
	}

	pcstr GetPathDir(pcstr path, bool file)
	{
		uint32 len = strlen(path);
		if(len < 1) 
			return str::Copy("\\");
		pcstr cs8_fix = str::Replace(path, "/", "\\");
		string str = cs8_fix;
		uint32 pos = str.rfind('\\');
		if(!file)
		{
			if(pos == string::npos || len != pos + 1)
				str += "\\";
		}
		else
		{
			if(pos == string::npos)
				str = "\\";
			else
				str = str.substr(0, pos + 1);
		}
		pcstr cs8_ret = str::Copy(str.c_str());
		return cs8_ret;
	}

	bool TryOpenRead(pcstr file)
	{
		pcwstr cs16_file(str::ToStrW(file));
		HANDLE handle = 
			CreateFile(cs16_file, null, 
			FILE_SHARE_READ, null, 
			OPEN_EXISTING, null, null);
		if(handle == INVALID_HANDLE_VALUE) 
			return false;
		CloseHandle(handle);
		return true;
	}
}