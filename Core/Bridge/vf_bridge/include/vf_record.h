#pragma once

#include "vf_base.h"

namespace vapula
{
	//get value unit
	VAPULA_API uint32 GetValueUnit(uint8 type);

	//record
	class VAPULA_API Record
	{
	protected:
		uint8 _Type; //data type
		uint8 _Access; //access mode
		uint32 _Size; //data size by byte
		raw _Data; //raw data

	protected:
		Record();
	public:
		virtual ~Record();

	public:
		//parse record from XML object
		//need node <record>
		static Record* Parse(raw xml);

	public:
		//get data type
		uint8 GetType();
		
		//get access mode
		uint8 GetAccess();

		//get size
		uint32 GetSize();

	public:
		//copy record
		Record* Copy();

		//zero record
		void Zero();

	public:
		//read data
		raw Read(bool copy = false);

		//write data by copy
		void Write(raw data, uint32 size);

		//deliver this to whom
		void Deliver(Record* who);

	//candy for C++
	public:
		//write string as 8-bit (UTF-8)
		void Write(pcstr data);

		//write string as 16-bit (UTF-16)
		void Write(pcwstr data);

		//write data at offset (at) by type (T)
		//write new data when diff size
		template<typename T>
		void WriteAt(T data, uint32 at = 0)
		{
			uint32 x = (at + 1) * sizeof(T);
			if (_Size >= x)
				((T*)_Data)[at] = data;
			else
			{
				T* mem = new T[at + 1];
				mem[at] = data;
				Write(mem, x);
				Clear(mem, true);
			}
		}

		//read data at offset (at) by type (T)
		template<typename T>
		T ReadAt(uint32 at = 0)
		{
			if (_Size >= sizeof(T)* (at + 1))
				return ((T*)_Data)[at];
			return null;
		}
	};

	typedef Record* PRecord;
}