#include "vf_record.h"
#include "vf_xml.h"

namespace vapula
{
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
		
		Record* record = new Record();
		record->_Type = (uint8)XML::ValInt(xe_type);
		record->_Access = (uint8)XML::ValInt(xe_access);
		return record;
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
		Record* record = new Record();
		record->_Type = _Type;
		record->_Access = _Access;
		return record;
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