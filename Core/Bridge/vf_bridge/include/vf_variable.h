#pragma once

#include "vf_base.h"

namespace vapula
{
	//get type unit
	VAPULA_API uint32 GetTypeUnit(int8 type);

	//variable
	class VAPULA_API Variable
	{
	private:
		int8 _Type; //param type
		int8 _Mode; //param mode
		object _Data; //memory
		uint32 _Length; //param length

	private:
		Variable();
	public:
		~Variable();

	public:
		//parse variable from XML string
		//need node <param>
		static Variable* Parse(pcstr xml);

		//parse variable from XML object
		//need node <param>
		static Variable* Parse(object xml);

	public:
		//get var type
		int8 GetType();
		
		//get var mode
		int8 GetMode();

		//get var length
		uint32 GetLength();

	public:
		//clear data
		void Zero();

		//copy variable
		Variable* Copy();

	public:
		//read data
		object Read(bool copy = false);

		//write data
		void Write(object value, uint32 len, bool copy = false);

		//write string as 8-bit
		void Write(pcstr value);

		//read value at index
		template<typename T>
		T ReadAt(uint32 at = 0)
		{
			if(_Type == VF_DATA_STRING)
				throw invalid_argument(_vf_err_0);
			return ((T*)_Data)[at];
		}

		//write value at index
		template<typename T>
		void WriteAt(T value, uint32 at = 0)
		{
			if(_Type == VF_DATA_STRING)
				throw invalid_argument(_vf_err_0);
			((T*)_Data)[at] = value;
		}

		//deliver this to whom
		void Deliver(Variable* who, bool copy = false);

	public:
		//read and cast value to string at index
		pcstr CastReadAt(uint32 at = 0);

		//cast string to value and write at index
		void CastWriteAt(pcstr value, uint32 at = 0);

		//encode memory to string
		pcstr CastRead();

		//decode string to memory
		void CastWrite(pcstr value);
	};

	typedef Variable* PVar;
}