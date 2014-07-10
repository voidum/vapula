#pragma once

#include "vf_base.h"

namespace vapula
{
	class VAPULA_API Pointer
	{
	private:
		raw _Data;
		uint32 _Size;

	public:
		Pointer();
		~Pointer();

	public:
		raw GetData();
		uint32 GetSize();

	public:
		void Capture(raw data, uint32 size);
		void Release();

	public:
		//read data as array
		//offset in byte & return max length in T
		template<typename T>
		uint32 ReadArray(T*& data, uint32 offset = 0)
		{
			data = null;
			if (_Size <= offset)
				return 0;
			uint32 size = _Size - offset;
			uint32 length = size / sizeof(T);
			if (length > 0)
			{
				data = (T*)((uint32)_Data + offset);
				return length;
			}
			return 0;
		}

		template<>
		uint32 ReadArray(raw& data, uint32 offset)
		{
			data = null;
			if (_Size <= offset)
				return 0;
			uint32 size = _Size - offset;
			if (size > 0)
			{
				data = (raw)((uint32)_Data + offset);
				return size;
			}
			return 0;
		}

		//write array data
		//offset in byte
		template<typename T>
		void WriteArray(T* data, uint32 length, uint32 offset = 0)
		{
			if (data == null || length == 0)
				return;
			uint32 size_need = sizeof(T) * length;
			uint32 size = _Size - offset;
			if (size < size_need)
			{
				//expand data
				uint32 size_new = size_need + offset;
				raw data_new = new byte[size_new];
				if (_Data != null)
				{
					memcpy(data_new, _Data, _Size);
					delete _Data;
				}
				_Data = data_new;
				_Size = size_new;
			}
			raw target = (raw)((uint32)_Data + offset);
			memcpy(target, data, size_need);
		}

		template<>
		void WriteArray(raw data, uint32 length, uint32 offset)
		{
			WriteArray((byte*)data, length, offset);
		}

	public:
		template<typename T>
		T ReadValue(uint32 offset = 0)
		{
			if (_Size <= offset)
				return 0;
			uint32 size = _Size - offset;
			if (size >= sizeof(T))
			{
				T* data = (T*)((uint32)_Data + offset);
				return data[0];
			}
			return null;
		}

		template<typename T>
		void WriteValue(T value, uint32 offset = 0)
		{
			uint32 size = _Size - offset;
			if (size <= sizeof(T))
			{
				//expand data
				uint32 size_new = sizeof(T) + offset;
				raw data_new = new byte[size_new];
				if (_Data != null)
				{
					memcpy(data_new, _Data, _Size);
					delete _Data;
				}
				_Data = data_new;
				_Size = size_new;
			}
			T* data = (T*)((uint32)_Data + offset);
			data[0] = value;
		}
	};
}