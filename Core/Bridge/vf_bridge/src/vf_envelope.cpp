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
		for(int i=0; i<_Total; i++)
		{
			if(_Addrs[i] != null)
				delete (object)(_Addrs[i]);
		}
		Clear(_Types, true);
		Clear(_Modes, true);
		Clear(_Addrs, true);
		Clear(_Lengths, true);
	}

	Envelope* Envelope::Parse(object xml)
	{
		if(xml == null)
			return null;

		vector<int> v_id;
		vector<int8> v_type;
		vector<int8> v_mode;

		xml_node<>* node = (xml_node<>*)xml;
		node = node->first_node();
		int total = 0;
		while(node)
		{
			v_id.push_back(xml::ValueInt(node->first_node("id")));
			v_type.push_back(xml::ValueInt(node->first_node("type")));
			v_mode.push_back(xml::ValueInt(node->first_node("mode")));
			node = node->next_sibling();
			total++;
		}

		Envelope* env = null;
		if(total > 0)
		{
			int8* arr_type = new int8[total];
			int8* arr_mode = new int8[total];
			for(int i=0; i<total; i++)
			{
				int id = v_id[i] - 1;
				arr_type[id] = v_type[i];
				arr_mode[id] = v_mode[i];
			}
			env = new Envelope(total);
			env->_Types = arr_type;
			env->_Modes = arr_mode;
		}
		else
		{
			env = new Envelope(0);
		}
		return env;
	}

	Envelope* Envelope::Parse(cstr8 xml)
	{
		xml_document<>* xdoc = (xml_document<>*)xml::Parse(xml);
		Envelope* env = Parse(xdoc->first_node());
		delete xdoc;
		return env;
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

	object Envelope::ReadObject(int id, uint32& size, bool copy)
	{
		if(!_AssertId(id))
			throw invalid_argument(_vf_err_1);
		int idx = id - 1;
		if(_Addrs[idx] == null)
			return null;
		if(!copy)
			return (object)(_Addrs[idx]);
		size = _Lengths[idx];
		object data = new byte[size];
		memcpy(data, (object)(_Addrs[idx]), size);
		return data;
	}

	void Envelope::WriteObject(int id, object value, uint32 size, bool copy)
	{
		if(!_AssertId(id))
			throw invalid_argument(_vf_err_1);
		int idx = id - 1;
		if(_Addrs[idx] != null)
			delete (object)(_Addrs[idx]);
		if(!copy)
			_Addrs[idx] = (uint32)value;
		object data = new byte[size];
		memcpy(data, value, size);
		_Lengths[idx] = size;
		_Addrs[idx] = (uint32)data;
	}

	cstr8 Envelope::CastRead(int id)
	{
		if(!_AssertId(id))
			throw invalid_argument(_vf_err_1);
		int type = GetType(id);
		switch(type)
		{
		case VF_DATA_INT8:	
			return str::ValueTo(Read<int8>(id));
		case VF_DATA_UINT8:
			return str::ValueTo(Read<uint8>(id));
		case VF_DATA_INT16:
			return str::ValueTo(Read<int16>(id));
		case VF_DATA_UINT16:
			return str::ValueTo(Read<uint16>(id));
		case VF_DATA_INT32:
			return str::ValueTo(Read<int32>(id));
		case VF_DATA_UINT32:
			return str::ValueTo(Read<uint32>(id));
		case VF_DATA_INT64:
			return str::ValueTo(Read<int64>(id));
		case VF_DATA_UINT64:
			return str::ValueTo(Read<uint64>(id));
		case VF_DATA_REAL32:
			return str::ValueTo(Read<real32>(id));
		case VF_DATA_REAL64:
			return str::ValueTo(Read<real64>(id));
		case VF_DATA_BOOL:	
			return Read<bool>(id) ? "true" : "false";
		case VF_DATA_STRING:
			return str::Copy(Read<cstr8>(id));
		default:
			return null;
		}
	}

	void Envelope::CastWrite(int id, cstr8 value)
	{
		if(value == null)
			throw invalid_argument(_vf_err_2);
		if(!_AssertId(id))
			throw invalid_argument(_vf_err_1);
		int type = GetType(id);
		switch(type)
		{
		case VF_DATA_INT8:	
			Write(id, (int8)atoi(value)); break;
		case VF_DATA_UINT8:
			Write(id, (uint8)atoi(value)); break;
		case VF_DATA_INT16:
			Write(id, (int16)atoi(value)); break;
		case VF_DATA_UINT16:
			Write(id, (uint16)atoi(value)); break;
		case VF_DATA_INT32:
			Write(id, atoi(value)); break;
		case VF_DATA_UINT32:
			Write(id, (uint32)atoi(value)); break;
		case VF_DATA_INT64:
			Write(id, _atoi64(value)); break;
		case VF_DATA_UINT64:
			Write(id, (uint64)_atoi64(value)); break;
		case VF_DATA_REAL32:
			Write(id, atof(value)); break;
		case VF_DATA_REAL64:
			Write(id, atof(value)); break;
		case VF_DATA_BOOL:	
			Write(id, (strcmp(value,"true") == 0) ? 1 : 0); break;
		case VF_DATA_STRING:
			Write(id, value); break;
		default:
			break;
		}
	}

	//TODO: Write Following
	void Envelope::Deliver(Envelope* who, int from, int to)
	{
		if(!_AssertId(from) || !_AssertId(to, who))
			throw invalid_argument(_vf_err_1);
		int type = GetType(from);
		if(type != who->GetType(to))
			throw invalid_argument(_vf_err_0);

		object ptr = null;
		switch(type)
		{
		case VF_DATA_MEMORY:
			ptr = new object[1];
			*((object*)ptr) = Read<object>(from);
			who->Write(to, ptr);
			break;
		case VF_DATA_INT8:
			ptr = new int8[1];
			*((int8*)ptr) = Read<int8>(from);
			who->Write(to, *((int8*)ptr));
			break;
		case VF_DATA_UINT8:
			ptr = new uint8[1];
			*((uint8*)ptr) = Read<uint8>(from);
			who->Write(to, *((uint8*)ptr));
			break;
		case VF_DATA_INT16:
			ptr = new int16[1];
			*((int16*)ptr) = Read<int16>(from);
			who->Write(to, *((int16*)ptr));
			break;
		case VF_DATA_UINT16:
			ptr = new uint16[1];
			*((uint16*)ptr) = Read<uint16>(from);
			who->Write(to, *((uint16*)ptr));
			break;
		case VF_DATA_INT32:
			ptr = new int32[1];
			*((int32*)ptr) = Read<int32>(from);
			who->Write(to, *((int32*)ptr));
			break;
		case VF_DATA_UINT32:
			ptr = new uint32[1];
			*((uint32*)ptr) = Read<uint32>(from);
			who->Write(to, *((uint32*)ptr));
			break;
		case VF_DATA_INT64:
			ptr = new int64[1];
			*((int64*)ptr) = Read<int64>(from);
			who->Write(to, *((int64*)ptr));
			break;
		case VF_DATA_UINT64:
			ptr = new uint64[1];
			*((uint64*)ptr) = Read<uint64>(from);
			who->Write(to, *((uint64*)ptr));
			break;
		case VF_DATA_REAL32:
			ptr = new real32[1];
			*((real32*)ptr) = Read<real32>(from);
			who->Write(to, *((real32*)ptr));
			break;
		case VF_DATA_REAL64:
			ptr = new real64[1];
			*((real64*)ptr) = Read<real64>(from);
			who->Write(to, *((real64*)ptr));
			break;
		case VF_DATA_BOOL:
			ptr = new int8[1];
			*((int8*)ptr) = Read<int8>(from);
			who->Write(to, *((int8*)ptr));
			break;
		case VF_DATA_STRING:
			ptr = new cstr16[1];
			*((cstr16*)ptr) = Read<cstr16>(from);
			who->Write(to, *((cstr16*)ptr));
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
		if(type == VF_DATA_MEMORY)
		{
			object* ptr = new object[1];
			ptr[0] = Read<object>(from);
			return who->Write(to, ptr[0]);
		}
		else
		{
			cstr8 value = CastRead(from);
			return who->CastWrite(to, value);
		}
	}
}