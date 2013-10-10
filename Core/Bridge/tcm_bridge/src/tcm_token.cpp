#include "stdafx.h"
#include "tcm_token.h"
#include <random>
#include <ctime>

namespace tcm
{
	using std::runtime_error;
	using namespace std::tr1;

	Stampable::Stampable() { }
	Stampable::~Stampable() { }

	Token::Token()
	{
		_A = NULL;
	}

	Token::~Token()
	{
		if(_A != NULL) delete [] _A;
	}
	
	Token* Token::Stamp(Stampable* target)
	{
		if(!target->CanSeal()) return NULL;
		mt19937 rm_core;
		rm_core.seed((UINT)time(0));
		uniform_int<BYTE>* rm = new uniform_int<BYTE>(5, 7);
		USHORT key = (*rm)(rm_core) * (*rm)(rm_core);
		delete rm;
		rm = new uniform_int<BYTE>(0, 255);
		BYTE len = (*rm)(rm_core);
		BYTE* data = new BYTE[len+1];
		data[0] = len;
		USHORT value = key;
		for(int i=1; i<len+1; i++)
		{
			data[i] = (BYTE)(*rm)(rm_core);
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

	bool Token::Match(USHORT key)
	{
		if(_A == NULL) return false;
		BYTE* data = (BYTE*)_A;
		int len = data[0];
		USHORT tmp = key;
		for(int i=1; i<len+1; i++)
		{
			tmp ^= data[i] > key ? data[i] : key;
			tmp <<= 4;
		}
		return (tmp == _B);
	}
}