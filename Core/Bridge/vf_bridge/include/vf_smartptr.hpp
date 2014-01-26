#pragma once

namespace vapula
{
	//scoped pointer
	template<typename T>
	class Scoped : Uncopiable
	{
	protected:
		T* _Ptr;
	public:
		explicit Scoped(T* ptr) { _Ptr = ptr; }
		explicit Scoped(const T* ptr) { _Ptr = const_cast<T*>(ptr); }

		~Scoped() { Clear(_Ptr); }

		T& operator*() const
		{
			if(_Ptr == null)
				return null;
			return *_Ptr;
		}

		T& operator[](uint32 i) const
		{
			if(_Ptr == null)
				return null;
			return _Ptr[i];
		}

		T* operator->() const { return _Ptr; }

		T* get() { return _Ptr; }

		bool empty() { return _Ptr == null; }
	};
}