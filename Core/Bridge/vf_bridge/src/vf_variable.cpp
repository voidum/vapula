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
		_IsProto = true;
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
		if(!_IsProto)
			Clear(_Data);
	}

	Variable* Variable::Copy()
	{
		Variable* var = new Variable();
		var->_Type = _Type;
		var->_Mode = _Mode;
		var->_IsProto = false;
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

	void Variable::Deliver(Variable* who)
	{
		if(_Type != who->_Type)
			throw invalid_argument(_vf_err_0);
		who->Write(_Data, _Length, true);
	}

	pcstr Variable::CastRead(uint32 at)
	{
		switch(_Type)
		{
		case VF_DATA_INT8:	
			return str::Value(Get<int8>(at));
		case VF_DATA_UINT8:
			return str::Value(Get<uint8>(at));
		case VF_DATA_INT16:
			return str::Value(Get<int16>(at));
		case VF_DATA_UINT16:
			return str::Value(Get<uint16>(at));
		case VF_DATA_INT32:
			return str::Value(Get<int32>(at));
		case VF_DATA_UINT32:
			return str::Value(Get<uint32>(at));
		case VF_DATA_INT64:
			return str::Value(Get<int64>(at));
		case VF_DATA_UINT64:
			return str::Value(Get<uint64>(at));
		case VF_DATA_REAL32:
			return str::Value(Get<real32>(at));
		case VF_DATA_REAL64:
			return str::Value(Get<real64>(at));
		case VF_DATA_BOOL:	
			return Get<bool>(at) ? "true" : "false";
		case VF_DATA_STRING:
			return (pcstr)Read(true);
		default:
			return null;
		}
	}

	void Variable::CastWrite(pcstr value, uint32 at)
	{
		if(value == null)
			throw invalid_argument(_vf_err_2);
		switch(_Type)
		{
		case VF_DATA_INT8:	
			Set((int8)atoi(value), at); break;
		case VF_DATA_UINT8:
			Set((uint8)atoi(value), at); break;
		case VF_DATA_INT16:
			Set((int16)atoi(value), at); break;
		case VF_DATA_UINT16:
			Set((uint16)atoi(value), at); break;
		case VF_DATA_INT32:
			Set(atoi(value), at); break;
		case VF_DATA_UINT32:
			Set((uint32)atoi(value), at); break;
		case VF_DATA_INT64:
			Set(_atoi64(value), at); break;
		case VF_DATA_UINT64:
			Set((uint64)_atoi64(value), at); break;
		case VF_DATA_REAL32:
			Set(atof(value), at); break;
		case VF_DATA_REAL64:
			Set(atof(value), at); break;
		case VF_DATA_BOOL:	
			Set((strcmp(value,"true") == 0) ? 1 : 0, at); break;
		case VF_DATA_STRING:
			Write(value); break;
		default:
			break;
		}
	}
}