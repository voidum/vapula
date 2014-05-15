#pragma once
#pragma warning(disable:4275)

#include "vf_const.h"

namespace vapula
{
	//restrict copy
	class Uncopiable
	{
	protected:
		Uncopiable();
		~Uncopiable();

	private:
		Uncopiable(const Uncopiable&);
		Uncopiable& operator=(const Uncopiable&);
	};

	//clear target
	template<typename T>
	void Clear(T& target, bool arr = false)
	{
		if(target == null)
			return;
		if (arr)
			delete[] target;
		else
			delete target;
		target = null;
	}

	//non-typed scoped pointer
	class VAPULA_API Scoped : Uncopiable
	{
	protected:
		raw _Ptr;

	public:
		explicit Scoped(raw ptr);
		~Scoped();

	public:
		raw Get();
		
		bool IsNull();

		void Ref(raw ptr);

		void DeRef();
	};

	template<typename T>
	class Traits
	{
	public:
		enum { TypeId = VF_CORE_UNKNOWN };
	};
}