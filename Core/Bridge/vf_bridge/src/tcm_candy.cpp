#include "stdafx.h"
#include "vf_candy.h"

namespace vf
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

	VarAOO::VarAOO()
	{
		_Token = new byte[1];
		_Value = null;
	}

	VarAOO::~VarAOO()
	{
		if(_Token != null)
			delete [] _Token;
		if(_Value != null)
			delete [] _Value;
	}

	bool VarAOO::CanSet()
	{
		return (_Token != null);
	}

	void VarAOO::_Set(object data, uint32 len)
	{
		_Value = new byte[len];
		memcpy(_Value, data, len);
		delete [] (byte*)_Token;
		_Token = null;
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

	Dictionary::Dictionary()
	{
		_Lock = new Lock();
	}

	Dictionary::~Dictionary()
	{
		Clear();
		delete _Lock;
	}

	int Dictionary::GetCount()
	{
		_Lock->EnterEx();
		int i = _Keys.size();
		_Lock->Leave();
		return i;
	}

	bool Dictionary::Contain(strw key)
	{
		if(key == null)
			return false;
		bool ret = false;
		_Lock->EnterEx();
		for(iter i = _Keys.begin(); i != _Keys.end(); i++)
		{
			if(wcscmp(key, *i) != 0)
				continue;
			_Lock->Leave();
			ret = true;
			break;
		}
		_Lock->Leave();
		return ret;
	}

	bool Dictionary::Add(strw key, strw value)
	{
		if(key == null || value == null)
			return false;
		if(Contain(key)) 
			return false;
		_Lock->EnterEx();
		_Keys.push_back(CopyStrW(key));
		_Values.push_back(CopyStrW(value));
		_Lock->Leave();
		return true;
	}

	bool Dictionary::Remove(strw key)
	{
		if(key == null)
			return true;
		int i = 0;
		bool ret = false;
		_Lock->EnterEx();
		for(iter i1 = _Keys.begin(); i1 != _Keys.end(); i1++)
		{
			if(wcscmp(key, *i1) != 0) 	
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

	strw Dictionary::GetKey(uint32 id)
	{
		_Lock->EnterEx();
		strw ret = null;
		if(id < _Keys.size())
			ret = CopyStrW(_Keys[id]);
		_Lock->Leave();
		return ret;
	}

	strw Dictionary::GetValue(uint32 id)
	{
		if(id < 0) return null;
		_Lock->EnterEx();
		strw ret = null;
		if(id < _Values.size())
			ret = CopyStrW(_Values[id]);
		_Lock->Leave();
		return ret;
	}

	void Dictionary::Clear()
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

	strw Dictionary::Find(strw key)
	{
		if(key == null) return null;
		strw ret = null;
		_Lock->EnterEx();
		for(iter i = _Keys.begin(); i != _Keys.end(); i++)
		{
			if(wcscmp(key, *i) != 0)
				continue;
			ret = CopyStrW(*i);
			break;
		}
		_Lock->Leave();
		return ret;
	}
}