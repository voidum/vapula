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
		bool _IsProto; //is prototype

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
		//zero var
		void Zero();

		//copy var
		Variable* Copy();

	public:
		//read data
		object Read(bool copy = false);

		//write data
		void Write(object value, uint32 len, bool copy = false);

		//write string as 8-bit
		void Write(pcstr value);

		//deliver this to who
		void Deliver(Variable* who);

	public:
		//sugar: read value at index
		template<typename T>
		T Get(uint32 at = 0)
		{
			if(_Type == VF_DATA_STRING)
				throw invalid_argument(_vf_err_0);
			return ((T*)_Data)[at];
		}

		//sugar: write value at index
		template<typename T>
		void Set(T value, uint32 at = 0)
		{
			if(_Type == VF_DATA_STRING)
				throw invalid_argument(_vf_err_0);
			((T*)_Data)[at] = value;
		}

	public:
		//read and cast value to string
		pcstr CastRead(uint32 at = 0);

		//cast string to value and write
		void CastWrite(pcstr value, uint32 at = 0);
	};

	typedef Variable* PVar;
}