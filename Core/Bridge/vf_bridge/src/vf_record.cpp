#include "vf_record.h"
#include "vf_xml.h"

namespace vapula
{
	Record::Record()
	{
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
		Record* record = new Record();
		raw xe_access = XML::XElem(xml, "access");
		record->_Access = (uint8)XML::ValInt(xe_access);
		return record;
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
		_Size = 0;
	}

	Record* Record::Copy()
	{
		Record* record = new Record();
		return record;
	}

	void Record::Write(raw data, uint32 size)
	{
		//null data
		if (data == null)
			return;

		//same address
		if (_Data == data)
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

	raw Record::Read(bool copy)
	{
		if (!copy)
			return _Data;
		raw data = new byte[_Size];
		memcpy(data, _Data, _Size);
		return data;
	}

	void Record::Deliver(Record* who)
	{
		if (_Data == null)
			return;

		if (_Access == VF_ACCESS_IN 
			|| who->_Access == VF_ACCESS_OUT)
			return;

		who->Write(_Data, _Size);
	}
}