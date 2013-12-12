#include "vf_token.h"

namespace vapula
{
	using namespace std::tr1;

	Stampable::Stampable() { }
	Stampable::~Stampable() { }

	Token::Token()
	{
		_A = null;
	}

	Token::~Token()
	{
		if(_A != null) delete [] _A;
	}
	
	Token* Token::Stamp(Stampable* target)
	{
		if(!target->CanSeal()) return null;
		mt19937 rm_core;
		rm_core.seed((uint32)time(0));
		uniform_int<uint8>* rm = new uniform_int<uint8>(5, 7);
		uint16 key = (*rm)(rm_core) * (*rm)(rm_core);
		delete rm;
		rm = new uniform_int<uint8>(0, 255);
		uint8 len = (*rm)(rm_core);
		byte* data = new byte[len + 1];
		data[0] = len;
		uint16 value = key;
		for(int i=1; i < len + 1; i++)
		{
			data[i] = (uint8)(*rm)(rm_core);
			value ^= data[i] > key ? data[i] : key;
			value <<= 4;
		}
		delete rm;
		Token* token = new Token();
		token->_A = data;
		token->_B = value;
		target->Seal(key);
		return token;
	}

	bool Token::Match(uint16 key)
	{
		if(_A == null) return false;
		byte* data = (byte*)_A;
		int len = data[0];
		uint16 tmp = key;
		for(int i=1; i < len + 1; i++)
		{
			tmp ^= data[i] > key ? data[i] : key;
			tmp <<= 4;
		}
		return (tmp == _B);
	}
}