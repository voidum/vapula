#pragma once

#include "vf_base.h"

namespace vapula
{
	//record
	class VAPULA_API Record
	{
	protected:
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
		//write data
		void Write(raw data, uint32 size, bool copy = false);

		//read data
		raw Read(bool copy = false);
	};

	typedef Record* PRecord;
}