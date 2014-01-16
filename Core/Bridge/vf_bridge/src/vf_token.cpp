#include "vf_token.h"

namespace vapula
{
	using namespace std::tr1;

	Token::Token()
	{
		_Lock = new Lock();
		mt19937 rm_core;
		rm_core.seed((uint32)time(0));
		uniform_int<uint8> rm1(2, 4);
		uint8 len = rm1(rm_core);
		uint8* data = new uint8[len + 1];
		memset(data, 0, len+1);
		data[0] = len;
		_A = data;
	}

	Token::~Token()
	{
		delete _Lock;
		Clear(_A, true);
	}
	
	bool Token::IsOff()
	{
		uint8* data = (uint8*)_A;
		return (data[data[0]] != 0);
	}

	void Token::Off(uint8& key)
	{
		_Lock->Enter();
		if(!IsOff())
		{
			mt19937 rm_core;
			rm_core.seed((uint32)time(0));
			//just a joke for 42
			uniform_int<uint8> rm(13, 42);
			key = rm(rm_core);

			uint8* data = (uint8*)_A;
			uint8 len = data[0];
			uint8 value = key;
			for(uint8 i=1; i<len+1; i++)
			{
				data[i] = rm(rm_core);
				value ^= data[i] > key ? data[i] : key;
				value <<= 4;
			}
			_B = value;
		}
		_Lock->Leave();
	}

	void Token::On(uint8 key)
	{
		_Lock->Enter();
		if(IsOff())
		{
			uint8* data = (uint8*)_A;
			uint8 len = data[0];
			uint8 value = key;
			for(uint8 i=1; i<len+1; i++)
			{
				value ^= data[i] > key ? data[i] : key;
				value <<= 4;
			}
			if(value == _B)
				data[data[0]] = 0;
		}
		_Lock->Leave();
	}
}