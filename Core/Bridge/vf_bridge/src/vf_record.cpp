#include "vf_record.h"

namespace vapula
{
	uint32 GetValueUnit(int8 type)
	{
		switch(type)
		{
		case VF_VALUE_INT8:
		case VF_VALUE_UINT8:
			return 1;
		case VF_VALUE_INT16:
		case VF_VALUE_UINT16:
			return 2;
		case VF_VALUE_INT32:
		case VF_VALUE_UINT32:
		case VF_VALUE_REAL32:
			return 4;
		case VF_VALUE_INT64:
		case VF_VALUE_UINT64:
		case VF_VALUE_REAL64:
			return 8;
		default:
			return 0;
		}
	}

	Record::Record()
	{
		_Type = VF_DATA_RAW;
		_Access = VF_ACCESS_INOUT;
		_Data = null;
		_Size = 0;
	}

	Record::~Record()
	{
		Zero();
	}

	Record* Record::Parse(pcstr xml)
	{
		xml = null;
		return null;
	}

	Record* Record::Parse(raw xml)
	{
		xml = null;
		return null;
	}
	
	int8 Record::GetType()
	{
		return _Type;
	}

	int8 Record::GetAccess()
	{
		return _Access;
	}
	
	uint32 Record::GetSize()
	{
		return _Size;
	}

	void Record::Zero()
	{
		Clear(_Data);
	}

	Record* Record::Copy()
	{
		Record* rec = new Record();
		rec->_Type = _Type;
		rec->_Access = _Access;
		return rec;
	}

	raw Record::Read(bool copy)
	{
		if(!copy)
			return _Data;
		raw data = new byte[_Size];
		memcpy(data, _Data, _Size);
		return data;
	}

	void Record::Write(raw value, uint32 size, bool copy)
	{
		//null data
		if(value == null)
			return;

		//same address
		if(_Data == value)
			return;

		//clear old data
		if(_Data != null && _Size != size)
			Clear(_Data);

		//copy data
		if(copy)
		{
			if(_Data == null)
				_Data = new byte[size];
			memcpy(_Data, value, size);
		}
		else
		{
			_Data = value;
		}
		_Size = size;
	}

	void Record::Write(pcstr value)
	{
		uint32 len = strlen(value) + 1;
		Write((raw)value, len, true);
	}

	void Record::Deliver(Record* who, bool copy)
	{
		who->Write(_Data, _Size, copy);
	}
}