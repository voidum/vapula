#pragma once

#pragma warning(disable:4100)

#include "vf_base.h"

namespace vapula
{
	//envelope for data
	class VAPULA_API Envelope
	{
	private:
		Envelope(int32 total);
	public:
		~Envelope();

	public:
		//parse envelope from XML string
		//need node <params>
		static Envelope* Parse(cstr8 xml);

		//parse envelope from XML object
		//need node <params> and RapidXML
		static Envelope* Parse(object xml);

	private:
		int32 _Total; //param total
		int8* _Types; //param type
		int8* _Modes; //param mode
		uint32* _Addrs; //memory address table
		uint32* _Lengths; //param length table

	private:
		bool _AssertId(int id, Envelope* env = null);
		object _Read(int idx, uint32 size, bool copy);
		void _Write(int idx, object value, uint32 size, bool clear, bool copy);

	public:
		//get param total
		int32 GetTotal();

		//get param mode
		int8 GetMode(int id);

		//get param type
		int8 GetType(int id);

		//get param length
		uint32 GetLength(int id);

	public:
		//zero envelope
		void Zero();

		//copy envelope
		Envelope* Copy();

	public:
		//read object
		//optional if copy, default non-copy
		object ReadObject(int id, uint32* size = null, bool copy = false);

		//write object
		//optional if copy, default non-copy
		void WriteObject(int id, object value, uint32 size, bool copy = false);

	public:
		//read param array
		//optional if copy, default non-copy
		template<typename T>
		T* ReadArray(int id, uint32* length = null, bool copy = false)
		{
			if(!_AssertId(id))
				throw invalid_argument(_vf_err_1);
			int idx = id - 1;
			if(length != null)
				*length = _Lengths[idx];
			T* data = (T*)_Read(idx, _Lengths[idx] * sizeof(T), copy);
			return data;
		}

		//read param value
		template<typename T>
		T ReadValue(int id)
		{
			T* data = ReadArray<T>(id);
			return data[0];
		}

		//write param array
		template<typename T>
		void WriteArray(int id, T* value, uint32 length, bool copy = false)
		{
			if(!_AssertId(id))
				throw invalid_argument(_vf_err_1);
			int idx = id - 1;

			bool clear = (value != null && _Lengths[idx] != length);
			_Write(idx, value, length * sizeof(T), clear, copy);
			_Lengths[idx] = length;
		}

		//write param value
		template<typename T>
		void WriteValue(int id, T value)
		{
			T tmp_value = value;
			WriteArray(id, &tmp_value, 1, true);
		}

	public:
		//read string as 8-bit
		cstr8 ReadCh8(int id);

		//read string as 16-bit
		cstr16 ReadCh16(int id);

		//write string as 8-bit
		void WriteCh8(int id, cstr8 value);
		
		//write string as 16-bit
		void WriteCh16(int id, cstr16 value);

	public:
		//read and cast value to string
		cstr8 CastReadValue(int id);

		//cast string to value and write
		void CastWriteValue(int id, cstr8 value);

		//deliver envelope
		void Deliver(Envelope* who, int from, int to);

		//deliver envelope with auto-cast
		void CastDeliver(Envelope* who, int from, int to);
	};
}