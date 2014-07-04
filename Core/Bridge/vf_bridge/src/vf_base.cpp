#include "vf_base.h"
#include "vf_stack.h"
#include "vf_setting.h"
#include "modp_b64/modp_b64.h"

namespace vapula
{
	Lock* Lock::_CtorLock = new Lock();

	Lock* Lock::GetCtorLock()
	{
		return _CtorLock;
	}

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
		int size_target = modp_b64_encode_len(size);
		char* result = new char[size_target];
		int code = modp_b64_encode(result, (pcstr)data, size);
		if (code == -1)
		{
			delete result;
			return null;
		}
		return result;
	}

	raw Base64ToRaw(pcstr data)
	{
		int size = strlen(data);
		int size_target = modp_b64_decode_len(size);
		char* result = new char[size_target];
		int code = modp_b64_decode(result, data, size);
		if (code == -1)
		{
			delete result;
			return null;
		}
		return result;
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