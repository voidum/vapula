#include "vf_envelope.h"
#include "vf_xml.h"
#include "rapidxml/rapidxml.hpp"

namespace vapula
{
	using rapidxml::xml_document;
	using rapidxml::xml_node;

	Envelope::Envelope(int32 total)
	{
		_Total = total;
		if(total > 0)
		{
			_Types = new int8[total];
			_Modes = new int8[total];
			_Addrs = new uint32[total];
			_Lengths = new uint32[total];
			memset(_Addrs, 0, total * sizeof(uint32));
			memset(_Lengths, 0, total * sizeof(uint32));
		}
		else
		{
			_Types = null;
			_Modes = null;
			_Addrs = null;
			_Lengths = null;
		}
	}

	Envelope::~Envelope()
	{
		Zero();
		Clear(_Types, true);
		Clear(_Modes, true);
		Clear(_Addrs, true);
		Clear(_Lengths, true);
	}

	Envelope* Envelope::Parse(cstr8 xml)
	{
		xml_node<>* xdoc = (xml_node<>*)xml::Parse(xml);

		vector<int32> v_id;
		vector<int8> v_type;
		vector<int8> v_mode;

		xml_node<>* xe = (xml_node<>*)xml::Path(xdoc, 2, "params", "param");
		int total = 0;
		while(xe != null)
		{
			v_id.push_back(xml::ValueInt(xe->first_attribute("id")));
			v_type.push_back(xml::ValueInt(xe->first_node("type")));
			v_mode.push_back(xml::ValueInt(xe->first_node("mode")));
			xe = xe->next_sibling();
			total++;
		}

		Envelope* env = new Envelope(total);
		for(int i=0; i<total; i++)
		{
			int id = v_id[i] - 1;
			env->_Types[id] = v_type[i];
			env->_Modes[id] = v_mode[i];
		}

		delete xdoc;
		return env;
	}

	bool Envelope::_AssertId(int id, Envelope* env)
	{
		if(id < 1)
			return false;
		if(env == null)
			return id <= _Total;
		else
			return id <= env->_Total;
	}

	object Envelope::_Read(int idx, uint32 size, bool copy)
	{
		if(_Addrs[idx] == null)
			return null;
		if(!copy)
			return (object)(_Addrs[idx]);
		object data = new byte[size];
		memcpy(data, (object)(_Addrs[idx]), size);
		return data;
	}

	void Envelope::_Write(int idx, object value, uint32 size, bool clear, bool copy)
	{
		//same address & do nothing
		if((uint32)value == _Addrs[idx])
			return;

		//clear old data
		if(clear)
			if(_Addrs[idx] != null)
				delete (object)(_Addrs[idx]);

		//null data & do nothing
		if(value == null)
			return;

		if(!copy)
		{
			_Addrs[idx] = (uint32)value;
			return;
		}
		object data = new byte[size];
		memcpy(data, value, size);
		_Addrs[idx] = (uint32)data;
	}

	int32 Envelope::GetTotal()
	{
		return _Total;
	}

	int8 Envelope::GetType(int id)
	{
		if(!_AssertId(id)) 
			throw invalid_argument(_vf_err_1);
		return _Types[id - 1];
	}

	int8 Envelope::GetMode(int id)
	{
		if(!_AssertId(id)) 
			throw invalid_argument(_vf_err_1);
		return _Modes[id - 1];
	}

	uint32 Envelope::GetLength(int id)
	{
		if(!_AssertId(id)) 
			throw invalid_argument(_vf_err_1);
		return _Lengths[id - 1];
	}

	void Envelope::Zero()
	{
		for(int i=0; i<_Total; i++)
		{
			if(_Addrs[i] != null)
				delete (object)(_Addrs[i]);
		}
	}

	Envelope* Envelope::Copy()
	{
		Envelope* env = new Envelope(_Total);
		for(int i=0; i<_Total; i++)
		{
			env->_Types[i] = _Types[i];
			env->_Modes[i] = _Modes[i];
		}
		return env;
	}

	object Envelope::ReadObject(int id, uint32* size, bool copy)
	{
		if(!_AssertId(id))
			throw invalid_argument(_vf_err_1);
		int idx = id - 1;
		object data = _Read(idx, _Lengths[idx], copy);
		if(size != null)
			*size = _Lengths[idx];
		return data;
	}

	void Envelope::WriteObject(int id, object value, uint32 size, bool copy)
	{
		if(!_AssertId(id))
			throw invalid_argument(_vf_err_1);
		int idx = id - 1;
		bool clear = (value != null && _Lengths[idx] != size);
		_Write(idx, value, size, clear, copy);
		_Lengths[idx] = size;
	}

	cstr8 Envelope::ReadCh8(int id)
	{
		uint32 size = 0;
		cstr8 s8 = (cstr8)ReadObject(id, &size, true);
		return s8;
	}

	cstr16 Envelope::ReadCh16(int id)
	{
		cstr8 s8 = (cstr8)ReadObject(id, null, false);
		cstr16 s16 = str::ToCh16(s8, _vf_msg_cp);
		return s16;
	}

	void Envelope::WriteCh8(int id, cstr8 value)
	{
		WriteObject(id, const_cast<str8>(value), strlen(value) + 1, true);
	}

	void Envelope::WriteCh16(int id, cstr16 value)
	{
		cstr8 s8 = str::ToCh8(value, _vf_msg_cp);
		WriteObject(id, const_cast<str8>(s8), wcslen(value) * 2 + 2, false);
	}

	cstr8 Envelope::CastReadValue(int id)
	{
		if(!_AssertId(id))
			throw invalid_argument(_vf_err_1);
		int8 type = GetType(id);
		switch(type)
		{
		case VF_DATA_INT8:	
			return str::ValueTo(ReadValue<int8>(id));
		case VF_DATA_UINT8:
			return str::ValueTo(ReadValue<uint8>(id));
		case VF_DATA_INT16:
			return str::ValueTo(ReadValue<int16>(id));
		case VF_DATA_UINT16:
			return str::ValueTo(ReadValue<uint16>(id));
		case VF_DATA_INT32:
			return str::ValueTo(ReadValue<int32>(id));
		case VF_DATA_UINT32:
			return str::ValueTo(ReadValue<uint32>(id));
		case VF_DATA_INT64:
			return str::ValueTo(ReadValue<int64>(id));
		case VF_DATA_UINT64:
			return str::ValueTo(ReadValue<uint64>(id));
		case VF_DATA_REAL32:
			return str::ValueTo(ReadValue<real32>(id));
		case VF_DATA_REAL64:
			return str::ValueTo(ReadValue<real64>(id));
		case VF_DATA_BOOL:	
			return ReadValue<bool>(id) ? "true" : "false";
		case VF_DATA_STRING:
			return ReadCh8(id);
		default:
			return null;
		}
	}

	void Envelope::CastWriteValue(int id, cstr8 value)
	{
		if(value == null)
			throw invalid_argument(_vf_err_2);
		if(!_AssertId(id))
			throw invalid_argument(_vf_err_1);
		int8 type = GetType(id);
		switch(type)
		{
		case VF_DATA_INT8:	
			WriteValue(id, (int8)atoi(value)); break;
		case VF_DATA_UINT8:
			WriteValue(id, (uint8)atoi(value)); break;
		case VF_DATA_INT16:
			WriteValue(id, (int16)atoi(value)); break;
		case VF_DATA_UINT16:
			WriteValue(id, (uint16)atoi(value)); break;
		case VF_DATA_INT32:
			WriteValue(id, atoi(value)); break;
		case VF_DATA_UINT32:
			WriteValue(id, (uint32)atoi(value)); break;
		case VF_DATA_INT64:
			WriteValue(id, _atoi64(value)); break;
		case VF_DATA_UINT64:
			WriteValue(id, (uint64)_atoi64(value)); break;
		case VF_DATA_REAL32:
			WriteValue(id, atof(value)); break;
		case VF_DATA_REAL64:
			WriteValue(id, atof(value)); break;
		case VF_DATA_BOOL:	
			WriteValue(id, (strcmp(value,"true") == 0) ? 1 : 0); break;
		case VF_DATA_STRING:
			WriteCh8(id, value); break;
		default:
			break;
		}
	}

	void Envelope::Deliver(Envelope* who, int from, int to)
	{
		if(!_AssertId(from) || !_AssertId(to, who))
			throw invalid_argument(_vf_err_1);
		int type = GetType(from);
		if(type != who->GetType(to))
			throw invalid_argument(_vf_err_0);

		object ptr = null;
		uint32 size = 0;
		switch(type)
		{
		case VF_DATA_OBJECT:
		case VF_DATA_STRING:
			ptr = ReadObject(from, &size, false);
			who->WriteObject(to, ptr, size, false);
			break;
		case VF_DATA_INT8:
			ptr = ReadArray<int8>(from, &size, false);
			who->WriteArray(to, (int8*)ptr, size, false);
			break;
		case VF_DATA_UINT8:
			ptr = ReadArray<uint8>(from, &size, false);
			who->WriteArray(to, (uint8*)ptr, size, false);
			break;
		case VF_DATA_INT16:
			ptr = ReadArray<int16>(from, &size, false);
			who->WriteArray(to, (int16*)ptr, size, false);
			break;
		case VF_DATA_UINT16:
			ptr = ReadArray<uint16>(from, &size, false);
			who->WriteArray(to, (uint16*)ptr, size, false);
			break;
		case VF_DATA_INT32:
			ptr = ReadArray<int32>(from, &size, false);
			who->WriteArray(to, (int32*)ptr, size, false);
			break;
		case VF_DATA_UINT32:
			ptr = ReadArray<uint32>(from, &size, false);
			who->WriteArray(to, (uint32*)ptr, size, false);
			break;
		case VF_DATA_INT64:
			ptr = ReadArray<int64>(from, &size, false);
			who->WriteArray(to, (int64*)ptr, size, false);
			break;
		case VF_DATA_UINT64:
			ptr = ReadArray<uint64>(from, &size, false);
			who->WriteArray(to, (uint64*)ptr, size, false);
			break;
		case VF_DATA_REAL32:
			ptr = ReadArray<real32>(from, &size, false);
			who->WriteArray(to, (real32*)ptr, size, false);
			break;
		case VF_DATA_REAL64:
			ptr = ReadArray<real64>(from, &size, false);
			who->WriteArray(to, (real64*)ptr, size, false);
			break;
		case VF_DATA_BOOL:
			ptr = ReadArray<bool>(from, &size, false);
			who->WriteArray(to, (bool*)ptr, size, false);
			break;
		default:
			return;
		}
		delete [] ptr;
	}

	void Envelope::CastDeliver(Envelope* who, int from, int to)
	{
		if(!_AssertId(from) || !_AssertId(to, who))
			throw invalid_argument(_vf_err_1);
		int type = GetType(from);
		uint32 size = _Lengths[from - 1];
		if(type == VF_DATA_OBJECT || size > 1)
		{
			uint32 size = 0;
			object ptr = ReadObject(from, &size, false);
			who->WriteObject(to, ptr, size, false);
		}
		else
		{
			cstr8 value = CastReadValue(from);
			return who->CastWriteValue(to, value);
		}
	}
}