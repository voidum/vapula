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
	void Clear(T& target, bool isarr = false)
	{
		if(target == null)
			return;
		if(isarr)
			delete [] target;
		else 
			delete target;
		target = null;
	}

	//non-typed scoped pointer
	class VAPULA_API Scoped : Uncopiable
	{
	protected:
		raw _Ptr;
		bool _IsArr;

	public:
		explicit Scoped(raw ptr, bool isarr = false);
		~Scoped();

	public:
		raw Get();
		
		bool IsNull();

		void Ref(raw ptr, bool isarr = false);

		void DeRef();

		template<typename T>
		T& Index(uint32 i) const
		{
			if(_Ptr == null)
				return null;
			return ((T*)_Ptr)[i];
		}
	};
}