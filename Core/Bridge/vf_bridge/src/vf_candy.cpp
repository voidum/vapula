#include "vf_candy.h"

namespace vapula
{
	Uncopiable::Uncopiable() { }

	Uncopiable::~Uncopiable() { }

	Lock::Lock()
	{
		_A = 15;
		_B = 300;
		_C = 10;
		_Core = (long*)_aligned_malloc(1, sizeof(long));
		InterlockedExchange(_Core, FALSE);
	}

	Lock::~Lock()
	{
		_aligned_free(_Core);
	}

	void Lock::Set(int a, int b, int c)
	{
		_A = a;
		_B = b;
		_C = c;
	}

	bool Lock::Enter()
	{
		for(int i=0; i<MAXINT; i++)
		{
			if(InterlockedExchange(_Core, TRUE) == FALSE)
				return true;
			Sleep(0);
		}
		return false;
	}

	bool Lock::EnterEx()
	{
		for(int i=0; i<_C; i++) //³¤Ë¯Ãß
		{
			int speed_old = 0x00;
			int speed_new = 0x01;
			for(int j=0; j<_A; j++) //Ë¥¼õ
			{
				if(speed_old >= _B) break;
				if(InterlockedExchange(_Core, TRUE) == FALSE)
					return true;
				Sleep(speed_old);
				speed_old = speed_new;
				speed_new += speed_old;
			}
			Sleep(_B);
		}
		return false;
	}

	void Lock::Leave()
	{
		InterlockedExchange(_Core, FALSE);
	}

	VarAO::VarAO()
	{
		_Seal = new byte[1];
		_Value = null;
	}

	VarAO::~VarAO()
	{
		Clear(_Seal, true);
		Clear(_Value);
	}

	bool VarAO::CanSet()
	{
		return (_Seal != null);
	}

	void VarAO::Set(object data, uint32 size)
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

	Dict::Dict()
	{
		_Lock = new Lock();
	}

	Dict::~Dict()
	{
		Clear();
		delete _Lock;
	}

	int Dict::GetCount()
	{
		_Lock->EnterEx();
		int i = _Keys.size();
		_Lock->Leave();
		return i;
	}

	bool Dict::Contain(cstr8 key)
	{
		if(key == null)
			return false;
		bool ret = false;
		_Lock->EnterEx();
		for(iter i = _Keys.begin(); i != _Keys.end(); i++)
		{
			if(strcmp(key, *i) != 0)
				continue;
			_Lock->Leave();
			ret = true;
			break;
		}
		_Lock->Leave();
		return ret;
	}

	bool Dict::Add(cstr8 key, cstr8 value)
	{
		if(key == null || value == null)
			return false;
		if(Contain(key)) 
			return false;
		_Lock->EnterEx();
		_Keys.push_back(str::Copy(key));
		_Values.push_back(str::Copy(value));
		_Lock->Leave();
		return true;
	}

	bool Dict::Remove(cstr8 key)
	{
		if(key == null)
			return true;
		int i = 0;
		bool ret = false;
		_Lock->EnterEx();
		for(iter i1 = _Keys.begin(); i1 != _Keys.end(); i1++)
		{
			if(strcmp(key, *i1) != 0) 	
			{
				i++;
				continue;
			}
			delete *i1; _Keys.erase(i1);
			iter i2 = _Values.begin() + i;
			delete *i2; _Values.erase(i2);
			ret = true; 
			break;
		}
		_Lock->Leave();
		return ret;
	}

	cstr8 Dict::GetKey(uint32 id)
	{
		_Lock->EnterEx();
		cstr8 ret = null;
		if(id < _Keys.size())
			ret = str::Copy(_Keys[id]);
		_Lock->Leave();
		return ret;
	}

	cstr8 Dict::GetValue(uint32 id)
	{
		if(id < 0) 
			return null;
		_Lock->EnterEx();
		cstr8 ret = null;
		if(id < _Values.size())
			ret = str::Copy(_Values[id]);
		_Lock->Leave();
		return ret;
	}

	void Dict::Clear()
	{
		_Lock->EnterEx();
		for(iter i = _Keys.begin(); i != _Keys.end(); i++)
			delete *i;
		_Keys.clear();
		for(iter i = _Values.begin(); i != _Values.end(); i++)
			delete *i;
		_Values.clear();
		_Lock->Leave();
	}

	cstr8 Dict::Find(cstr8 key)
	{
		if(key == null) 
			return null;
		cstr8 ret = null;
		_Lock->EnterEx();
		for(iter i = _Keys.begin(); i != _Keys.end(); i++)
		{
			if(strcmp(key, *i) != 0)
				continue;
			ret = str::Copy(*i);
			break;
		}
		_Lock->Leave();
		return ret;
	}
}