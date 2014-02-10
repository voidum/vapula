#include "vf_base.h"
#include "vf_stack.h"

namespace vapula
{
	Error::Error(int what)
	{
		_What = what;
	}

	Error::~Error() { }

	int Error::What()
	{
		return _What;
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

	Lock* Lock::_CtorLock = new Lock();

	Lock* Lock::GetCtorLock()
	{
		return _CtorLock;
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
		_Seal = new byte[1];
		_Data = null;
	}

	Once::~Once()
	{
		Clear(_Seal);
		Clear(_Data);
	}

	bool Once::CanSet()
	{
		return (_Seal != null);
	}

	void Once::Set(object data, uint32 size)
	{
		Lock* lock = Lock::GetCtorLock();
		lock->Enter();
		if(!CanSet())
			return;
		_Data = new byte[size];
		lock->Leave();
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

	uint32 GetTypeUnit(int8 type)
	{
		switch(type)
		{
		case VF_DATA_OBJECT:
		case VF_DATA_STRING:
		case VF_DATA_INT8:
		case VF_DATA_UINT8:
		case VF_DATA_BOOL:
			return 1;
		case VF_DATA_INT16:
		case VF_DATA_UINT16:
			return 2;
		case VF_DATA_INT32:
		case VF_DATA_UINT32:
		case VF_DATA_REAL32:
			return 4;
		case VF_DATA_INT64:
		case VF_DATA_UINT64:
		case VF_DATA_REAL64:
			return 8;
		default:
			return 1;
		}
	}

	pcstr GetLUID(bool logo)
	{
		std::ostringstream oss;
		oss.imbue(std::locale("C"));
		const time_t t = time(null);
		if(logo)
			oss<<"VAPULA_";
		oss<<t<<"_";
		srand((uint32)time(null));
		for(int8 i=0; i<16; i++)
		{
			int rnd = rand() % 10;
			oss<<rnd;
		}
		return str::Copy(oss.str().c_str());
	}

	pcstr GetVersion()
	{
		return _vf_version;
	}

	void ThrowError(int what)
	{
		Error* err = new Error(what);
		Stack* stack = Stack::GetInstance();
		if(stack != null)
		{
			Error* old_err = stack->GetError();
			Clear(old_err);
			stack->SetError(err);
		}
		throw err;
	}
}