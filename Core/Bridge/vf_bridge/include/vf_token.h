#pragma once

#include "vf_utility.h"

namespace vapula
{
	//支持令牌封签特性
	class VAPULA_API Stampable
	{
	public:
		Stampable();
		virtual ~Stampable();
	public:
		virtual bool CanSeal() = 0;
		virtual void Seal(uint16 key) = 0;
	};

	//操作令牌
	class VAPULA_API Token : Uncopiable
	{
	private:
		Token();
	public:
		~Token();
	public:
		static Token* Stamp(Stampable* target);
	private:
		object _A;
		uint16 _B;
	public:
		bool Match(uint16 key);
	};
}