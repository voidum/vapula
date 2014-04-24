#pragma once

#include "vf_base.h"

namespace vapula
{
	//get value unit
	VAPULA_API uint32 GetValueUnit(int8 type);

	//record
	class VAPULA_API Record
	{
	protected:
		int8 _Type; //data type
		int8 _Access; //access mode
		raw _Data; //raw data
		uint32 _Size; //data size by byte

	protected:
		Record();
	public:
		virtual ~Record();

	public:
		//parse record from XML string
		//need node <record>
		static Record* Parse(pcstr xml);

		//parse record from XML object
		//need node <record>
		static Record* Parse(raw xml);

	public:
		//get data type
		int8 GetType();
		
		//get access mode
		int8 GetAccess();

		//get size
		uint32 GetSize();

	public:
		//clear data
		void Zero();

		//copy record
		Record* Copy();

	public:
		//read data
		raw Read(bool copy = false);

		//write data
		void Write(raw value, uint32 size, bool copy = false);

		//deliver this to whom
		void Deliver(Record* who, bool copy = false);

	//candy for C++
	public:
		//write string as 8-bit
		void Write(pcstr value);

		template<typename T>
		void WriteAt()
		{
		}

		template<typename T>
		T ReadAt()
		{
			return null;
		}
	};

	typedef Record* PRecord;
}