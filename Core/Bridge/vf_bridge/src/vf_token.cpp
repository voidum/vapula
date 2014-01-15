#include "vf_token.h"

namespace vapula
{
	using namespace std::tr1;

	RequireTI::RequireTI()
	{
		_Token = new Token();
	}

	RequireTI::~RequireTI()
	{
		delete _Token;
	}

	bool RequireTI::AssertOffTI()
	{
		return _Token->IsOff();
	}

	void RequireTI::TokenOff(uint8& key)
	{
		_Token->Off(key);
	}

	void RequireTI::TokenOn(uint8 key)
	{
		_Token->On(key);
	}

	Token::Token()
	{
		_A = null;
		_Lock = new Lock();
	}

	Token::~Token()
	{
		Clear(_A, true);
		delete _Lock;
	}
	
	bool Token::IsOff()
	{
		return (_A != null);
	}

	void Token::Off(uint8& key)
	{
		if(IsOff())
			return;
		_Lock->Enter();
		Clear(_A, true);
		mt19937 rm_core;
		rm_core.seed((uint32)time(0));
		uniform_int<uint8> rm1(2, 4);
		uint8 len = rm1(rm_core);
		uint8* data = new uint8[len + 1];
		data[0] = len;
		//just a joke for 42
		uniform_int<uint8> rm2(13, 42);
		key = rm2(rm_core);
		uint8 value = key;
		for(int i=1; i<len+1; i++)
		{
			data[i] = rm2(rm_core);
			value ^= data[i] > key ? data[i] : key;
			value <<= 4;
		}
		_A = data;
		_B = value;
		_Lock->Leave();
	}

	void Token::On(uint8 key)
	{
		if(!IsOff())
			return;
		_Lock->Enter();
		uint8* data = (uint8*)_A;
		uint8 len = data[0];
		uint8 value = key;
		for(int i=1; i<len+1; i++)
		{
			value ^= data[i] > key ? data[i] : key;
			value <<= 4;
		}
		if(value == _B)
			Clear(_A, true);
		_Lock->Leave();
	}
}