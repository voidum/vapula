#include "vf_base.h"

namespace vapula
{
	Uncopiable::Uncopiable() { }

	Uncopiable::~Uncopiable() { }

	Lock::Lock(uint16 a, uint16 b, uint16 c)
	{
		_A = a;
		_B = b;
		_C = c;
		_Core = (uint64*)_aligned_malloc(1, sizeof(uint64));
		InterlockedExchange(_Core, FALSE);
	}

	Lock::~Lock()
	{
		_aligned_free(_Core);
	}

	Lock* Lock::_CtorLock = new Lock();

	Lock* Lock::GetCtorLock()
	{
		return _CtorLock;
	}

	bool Lock::Enter()
	{
		for(uint16 i=0; i<_B; i++)
		{
			//1T
			for(uint16 j=0; j<_A; j++)
				if(InterlockedExchange(_Core, TRUE) == FALSE)
					return true;
			//sleep more time
			Sleep(_C);
		}
		return false;
	}

	void Lock::Leave()
	{
		InterlockedExchange(_Core, FALSE);
	}

	Once::Once()
	{
		_Seal = new byte[1];
		_Value = null;
	}

	Once::~Once()
	{
		Clear(_Seal, true);
		Clear(_Value);
	}

	bool Once::CanSet()
	{
		return (_Seal != null);
	}

	void Once::Set(object data, uint32 size)
	{
		if(!CanSet())
			return;
		_Value = new byte[size];
		memcpy(_Value, data, size);
		Clear(_Seal, true);
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

	cstr8 GetLUID(bool logo)
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

	cstr8 GetVersion()
	{
		return "2.0.8.0";
	}
}