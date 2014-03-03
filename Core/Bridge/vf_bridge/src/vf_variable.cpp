#include "vf_variable.h"

namespace vapula
{
	uint32 GetTypeUnit(int8 type)
	{
		switch(type)
		{
		case VF_DATA_OBJECT:
		case VF_DATA_STRING:
		case VF_DATA_INT8:
		case VF_DATA_UINT8:
		case VF_DATA_BOOL:
			return 1;
		case VF_DATA_INT16:
		case VF_DATA_UINT16:
			return 2;
		case VF_DATA_INT32:
		case VF_DATA_UINT32:
		case VF_DATA_REAL32:
			return 4;
		case VF_DATA_INT64:
		case VF_DATA_UINT64:
		case VF_DATA_REAL64:
			return 8;
		default:
			return 0;
		}
	}

	Variable::Variable()
	{
		_Type = VF_DATA_OBJECT;
		_Mode = VF_PM_INOUT;
		_Data = null;
		_Length = 0;
	}

	Variable::~Variable()
	{
		Zero();
	}

	Variable* Variable::Parse(pcstr xml)
	{
		xml = null;
		return null;
	}

	Variable* Variable::Parse(object xml)
	{
		xml = null;
		return null;
	}
	
	int8 Variable::GetType()
	{
		return _Type;
	}

	int8 Variable::GetMode()
	{
		return _Mode;
	}
	
	uint32 Variable::GetLength()
	{
		return _Length;
	}

	void Variable::Zero()
	{
		Clear(_Data);
	}

	Variable* Variable::Copy()
	{
		Variable* var = new Variable();
		var->_Type = _Type;
		var->_Mode = _Mode;
		return var;
	}

	object Variable::Read(bool copy)
	{
		if(!copy)
			return _Data;
		uint32 size = _Length * GetTypeUnit(_Type);
		object data = new byte[size];
		memcpy(data, _Data, size);
		return data;
	}

	void Variable::Write(object value, uint32 len, bool copy)
	{
		//null data
		if(value == null)
			return;

		//same address
		if(_Data == value)
			return;

		//clear old data
		if(_Data != null && _Length != len)
			Clear(_Data);

		//copy data
		if(copy)
		{
			uint32 size = len * GetTypeUnit(_Type);
			if(_Data == null)
				_Data = new byte[size];
			memcpy(_Data, value, size);
		}
		else
		{
			_Data = value;
		}
		_Length = len;
	}

	void Variable::Write(pcstr value)
	{
		uint32 len = strlen(value) + 1;
		Write((object)value, len, true);
	}

	void Variable::Deliver(Variable* who, bool copy)
	{
		uint32 unit = GetTypeUnit(who->_Type);
		uint32 len = _Length + _Length % unit;
		who->Write(_Data, len, copy);
	}

	pcstr Variable::CastReadAt(uint32 at)
	{
		switch(_Type)
		{
		case VF_DATA_INT8:
			return str::Value(ReadAt<int8>(at));
		case VF_DATA_UINT8:
			return str::Value(ReadAt<uint8>(at));
		case VF_DATA_INT16:
			return str::Value(ReadAt<int16>(at));
		case VF_DATA_UINT16:
			return str::Value(ReadAt<uint16>(at));
		case VF_DATA_INT32:
			return str::Value(ReadAt<int32>(at));
		case VF_DATA_UINT32:
			return str::Value(ReadAt<uint32>(at));
		case VF_DATA_INT64:
			return str::Value(ReadAt<int64>(at));
		case VF_DATA_UINT64:
			return str::Value(ReadAt<uint64>(at));
		case VF_DATA_REAL32:
			return str::Value(ReadAt<real32>(at));
		case VF_DATA_REAL64:
			return str::Value(ReadAt<real64>(at));
		case VF_DATA_BOOL:
			return ReadAt<bool>(at) ? "true" : "false";
		default:
			return null;
		}
	}

	void Variable::CastWriteAt(pcstr value, uint32 at)
	{
		if(value == null)
			throw invalid_argument(_vf_err_2);
		switch(_Type)
		{
		case VF_DATA_INT8:
			WriteAt((int8)atoi(value), at); break;
		case VF_DATA_UINT8:
			WriteAt((uint8)atoi(value), at); break;
		case VF_DATA_INT16:
			WriteAt((int16)atoi(value), at); break;
		case VF_DATA_UINT16:
			WriteAt((uint16)atoi(value), at); break;
		case VF_DATA_INT32:
			WriteAt(atoi(value), at); break;
		case VF_DATA_UINT32:
			WriteAt((uint32)atoi(value), at); break;
		case VF_DATA_INT64:
			WriteAt(_atoi64(value), at); break;
		case VF_DATA_UINT64:
			WriteAt((uint64)_atoi64(value), at); break;
		case VF_DATA_REAL32:
			WriteAt(atof(value), at); break;
		case VF_DATA_REAL64:
			WriteAt(atof(value), at); break;
		case VF_DATA_BOOL:
			WriteAt((strcmp(value,"true") == 0) ? 1 : 0, at); break;
		default:
			break;
		}
	}

	pcstr Variable::CastRead()
	{
		return null;
	}

	void Variable::CastWrite(pcstr value)
	{
		value = null;
	}
}