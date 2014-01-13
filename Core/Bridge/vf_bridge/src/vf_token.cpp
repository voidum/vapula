#include "vf_token.h"

namespace vapula
{
	using namespace std::tr1;

	Token::Token()
	{
		_A = null;
	}

	Token::~Token()
	{
		Clear(_A, true);
	}
	
	bool Token::IsLock()
	{
		if(_A == null)
			return false;
		uint8* data = (uint8*)_A;
		return (data[data[0]] == 0);
	}

	uint8 Token::Lock()
	{
		if(IsLock())
			return 0;
		Clear(_A, true);
		mt19937 rm_core;
		rm_core.seed((uint32)time(0));
		uniform_int<uint8> rm1(5, 8);
		uint8 len = rm1(rm_core);
		uint8* data = new uint8[len + 1];
		data[0] = len;
		//just a joke for 42
		uniform_int<uint8> rm2(13, 42);
		uint8 key = rm2(rm_core);
		uint8 value = key;
		for(int i=1; i<len+1; i++)
		{
			data[i] = rm2(rm_core);
			value ^= data[i] > key ? data[i] : key;
			value <<= 2;
		}
		_A = data;
		_B = value;
		return key;
	}

	void Token::Unlock(uint8 key)
	{
		if(!IsLock())
			return;
		uint8* data = (uint8*)_A;
		uint8 len = data[0];
		uint8 value = key;
		for(int i=1; i<len+1; i++)
		{
			value ^= data[i] > key ? data[i] : key;
			value <<= 2;
		}
		if(value == _B)
			data[data[0]] = 0;
	}
}