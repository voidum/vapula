#include "vf_record.h"
#include "vf_xml.h"

namespace vapula
{
	uint32 GetValueUnit(uint8 type)
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

	Record* Record::Parse(raw xml)
	{
		raw xe_type = XML::XElem(xml, "type");
		raw xe_access = XML::XElem(xml, "access");
		
		Record* rec = new Record();
		rec->_Type = (uint8)XML::ValInt(xe_type);
		rec->_Access = (uint8)XML::ValInt(xe_access);
		return rec;
	}
	
	uint8 Record::GetType()
	{
		return _Type;
	}

	uint8 Record::GetAccess()
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

	void Record::Write(raw data, uint32 size)
	{
		//null data
		if(data == null)
			return;

		//same address
		if(_Data == data)
			return;

		//write data
		if (_Size != size)
		{
			if (_Data != null)
				Clear(_Data);
			_Data = new byte[size];
		}
		memcpy(_Data, data, size);
		_Size = size;
	}

	void Record::Write(pcstr data)
	{
		uint32 size = strlen(data) + 1;
		Write((raw)data, size);
	}

	void Record::Write(pcwstr data)
	{
		uint32 size = wcslen(data) * 2 + 2;
		Write((raw)data, size);
	}

	void Record::Deliver(Record* who)
	{
		if (_Data == null)
			return;
		who->Write(_Data, _Size);
	}
}