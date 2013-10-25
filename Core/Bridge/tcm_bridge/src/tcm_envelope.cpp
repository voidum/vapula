#include "stdafx.h"
#include "tcm_envelope.h"
#include "tcm_xml.h"

namespace tcm
{
	using rapidxml::xml_document;
	using rapidxml::xml_node;

	Envelope::Envelope(int total, int* types, bool* ins)
	{
		_Total = total;
		_Types = types;
		_InStates = ins;
		if(total > 0)
		{
			_Offsets = new int[total];
			int len = 0;
			for(int i=0; i<total; i++)
			{
				_Offsets[i] = len;
				len += GetTypeUnit(_Types[i]);
			}
			_Length = len;
			_Memory = new BYTE[len];
			memset(_Memory,0,len);
		}
		else
		{
			_Offsets = NULL;
			_Memory = NULL;
		}
	}

	Envelope::~Envelope()
	{
		if(_InStates != NULL) delete [] _InStates;
		if(_Types != NULL) delete [] _Types;
		if(_Offsets != NULL) delete [] _Offsets;
		if(_Memory != NULL) delete [] _Memory;
	}

	Envelope* Envelope::Parse(LPVOID xml)
	{
		if(xml == NULL) return NULL;

		vector<int> v_id;
		vector<int> v_type;
		vector<bool> v_in;

		xml_node<>* node = (xml_node<>*)xml;
		node = node->first_node();
		int total = 0;
		while(node)
		{
			v_id.push_back(xml::ValueInt(node->first_attribute("id")));
			v_type.push_back(xml::ValueInt(node->first_attribute("type")));
			v_in.push_back(xml::ValueBool(node->first_attribute("in"), "true"));
			node = node->next_sibling();
			total++;
		}

		if(total > 0)
		{
			int* arr_type = new int[total];
			bool* arr_in = new bool[v_id.size()];
			for(int i=0;i<total;i++)
			{
				int id = v_id[i];
				arr_type[id] = v_type[id];
				arr_in[id] = v_in[id];
			}
			Envelope* env = new Envelope(v_id.size(), arr_type, arr_in);
			return env;
		}
		else
		{
			Envelope* env = new Envelope(0, NULL, NULL);
			return env;
		}
	}

	Envelope* Envelope::Parse(PCSTR xml)
	{
		xml_document<>* xdoc = (xml_document<>*)xml::Parse(xml);
		Envelope* env = Parse(xdoc->first_node());
		delete xdoc;
		return env;
	}

	Envelope* Envelope::Load(PCWSTR path, int fid)
	{
		PCSTR data = NULL;
		xml_document<>* xdoc = (xml_document<>*)xml::Load(path, data);
		xml_node<>* xe = (xml_node<>*)xml::Path(
			xdoc, 3, "library", "functions", "function");
		while (xe)
		{
			int tmpv = xml::ValueInt(xe->first_attribute("id"));
			if(tmpv == fid) break;
			xe = xe->next_sibling();
		}
		xe = xe->first_node("params");
		Envelope* env = Envelope::Parse(xe);
		delete data;
		return env;
	}

	int Envelope::GetParamTotal()
	{
		return _Total;
	}

	bool Envelope::GetInState(int id)
	{
		if(id >= _Total) throw runtime_error(_tcm_env_err_1);
		return _InStates[id];
	}

	PCSTR Envelope::CastReadA(int id)
	{
		if(id >= _Total) throw runtime_error(_tcm_env_err_1);
		int type = _Types[id];
		switch(type)
		{
		case TCM_DATA_POINTER:	return ValueToStrA((ULONG)Read<LPVOID>(id));
		case TCM_DATA_INT8:	return ValueToStrA(Read<char>(id));
		case TCM_DATA_UINT8:	return ValueToStrA(Read<BYTE>(id));
		case TCM_DATA_INT16:	return ValueToStrA(Read<short>(id));
		case TCM_DATA_UINT16:	return ValueToStrA(Read<USHORT>(id));
		case TCM_DATA_INT32:	return ValueToStrA(Read<int>(id));
		case TCM_DATA_UINT32:	return ValueToStrA(Read<UINT>(id));
		case TCM_DATA_INT64:	return ValueToStrA(Read<LONGLONG>(id));
		case TCM_DATA_UINT64:	return ValueToStrA(Read<ULONGLONG>(id));
		case TCM_DATA_REAL32:	return ValueToStrA(Read<float>(id));
		case TCM_DATA_REAL64:	return ValueToStrA(Read<double>(id));
		case TCM_DATA_BOOL:	return Read<bool>(id) ? "true" : "false";
		case TCM_DATA_CSTRA:	return CopyStrA(Read<PCSTR>(id));
		case TCM_DATA_CSTRW:	return WcToMb(Read<PCWSTR>(id));
		default:				return NULL;
		}
	}

	PCWSTR Envelope::CastReadW(int id)
	{
		if(id >= _Total) throw runtime_error(_tcm_env_err_1);
		int type = _Types[id];
		switch(type)
		{
		case TCM_DATA_POINTER:	return ValueToStrW((ULONG)Read<LPVOID>(id));
		case TCM_DATA_INT8:	return ValueToStrW(Read<char>(id));
		case TCM_DATA_UINT8:	return ValueToStrW(Read<BYTE>(id));
		case TCM_DATA_INT16:	return ValueToStrW(Read<short>(id));
		case TCM_DATA_UINT16:	return ValueToStrW(Read<USHORT>(id));
		case TCM_DATA_INT32:	return ValueToStrW(Read<int>(id));
		case TCM_DATA_UINT32:	return ValueToStrW(Read<UINT>(id));
		case TCM_DATA_INT64:	return ValueToStrW(Read<LONGLONG>(id));
		case TCM_DATA_UINT64:	return ValueToStrW(Read<ULONGLONG>(id));
		case TCM_DATA_REAL32:	return ValueToStrW(Read<float>(id));
		case TCM_DATA_REAL64:	return ValueToStrW(Read<double>(id));
		case TCM_DATA_BOOL:	return Read<bool>(id) ? L"true" : L"false";
		case TCM_DATA_CSTRA:	return MbToWc(Read<PCSTR>(id));
		case TCM_DATA_CSTRW:	return CopyStrW(Read<PCWSTR>(id));
		default:				return NULL;
		}
	}

	void Envelope::CastWriteA(int id, PCSTR value)
	{
		if(id >= _Total) throw runtime_error(_tcm_env_err_1);
		if(value == NULL) throw runtime_error(_tcm_env_err_2);
		int type = _Types[id];
		switch(type)
		{
		case TCM_DATA_POINTER:	Write(id,(LPVOID)abs(atoi(value)),0); return;
		case TCM_DATA_INT8:	Write(id,(char)atoi(value)); return;
		case TCM_DATA_UINT8:	Write(id,(BYTE)abs(atoi(value))); return;
		case TCM_DATA_INT16:	Write(id,(short)atoi(value)); return;
		case TCM_DATA_UINT16:	Write(id,(USHORT)abs(atoi(value))); return;
		case TCM_DATA_INT32:	Write(id,atoi(value)); return;
		case TCM_DATA_UINT32:	Write(id,(unsigned)abs(atoi(value))); return;
		case TCM_DATA_INT64:	Write(id,_atoi64(value)); return;
		case TCM_DATA_UINT64:	Write(id,(unsigned)abs(_atoi64(value))); return;
		case TCM_DATA_REAL32:	Write(id,atof(value)); return;
		case TCM_DATA_REAL64:	Write(id,atof(value)); return;
		case TCM_DATA_BOOL:	Write(id,(strcmp(value,"true") == 0) ? TRUE : FALSE); return;
		case TCM_DATA_CSTRA:	Write(id,value); return;
		case TCM_DATA_CSTRW:	Write(id,MbToWc(value)); return;
		default:				return;
		}
	}

	void Envelope::CastWriteW(int id,PCWSTR value)
	{
		if(id >= _Total) throw runtime_error(_tcm_env_err_1);
		if(value == NULL) throw runtime_error(_tcm_env_err_2);
		int type = _Types[id];
		if(type == TCM_DATA_CSTRW)
		{
			Write(id, value);
			return;
		}
		PCSTR str = WcToMb(value);
		CastWriteA(id,str);
	}

	void Envelope::Deliver(Envelope* who, int from, int to)
	{
		if(from >= _Total || to >= who->_Total) throw runtime_error(_tcm_env_err_1);
		int type = _Types[from];
		if(type != who->_Types[to]) throw runtime_error(_tcm_env_err_3);

		LPVOID ptr = NULL;
		switch(type)
		{
		case TCM_DATA_POINTER:
			ptr = new LPVOID[1];
			*((LPVOID*)ptr) = Read<LPVOID>(from);
			who->Write(to,ptr);
			break;
		case TCM_DATA_INT8:
			ptr = new char[1];
			*((char*)ptr) = Read<char>(from);
			who->Write(to,*((char*)ptr));
			break;
		case TCM_DATA_UINT8:
			ptr = new BYTE[1];
			*((BYTE*)ptr) = Read<BYTE>(from);
			who->Write(to,*((BYTE*)ptr));
			break;
		case TCM_DATA_INT16:
			ptr = new short[1];
			*((short*)ptr) = Read<short>(from);
			who->Write(to,*((short*)ptr));
			break;
		case TCM_DATA_UINT16:
			ptr = new USHORT[1];
			*((USHORT*)ptr) = Read<USHORT>(from);
			who->Write(to,*((USHORT*)ptr));
			break;
		case TCM_DATA_INT32:
			ptr = new int[1];
			*((int*)ptr) = Read<int>(from);
			who->Write(to,*((int*)ptr));
			break;
		case TCM_DATA_UINT32:
			ptr = new UINT[1];
			*((UINT*)ptr) = Read<UINT>(from);
			who->Write(to,*((UINT*)ptr));
			break;
		case TCM_DATA_INT64:
			ptr = new LONGLONG[1];
			*((LONGLONG*)ptr) = Read<LONGLONG>(from);
			who->Write(to,*((LONGLONG*)ptr));
			break;
		case TCM_DATA_UINT64:
			ptr = new ULONGLONG[1];
			*((ULONGLONG*)ptr) = Read<ULONGLONG>(from);
			who->Write(to,*((ULONGLONG*)ptr));
			break;
		case TCM_DATA_REAL32:
			ptr = new float[1];
			*((float*)ptr) = Read<float>(from);
			who->Write(to,*((float*)ptr));
			break;
		case TCM_DATA_REAL64:
			ptr = new double[1];
			*((double*)ptr) = Read<double>(from);
			who->Write(to,*((double*)ptr));
			break;
		case TCM_DATA_BOOL:
			ptr = new char[1];
			*((char*)ptr) = Read<char>(from);
			who->Write(to,*((char*)ptr));
			break;
		case TCM_DATA_CSTRA:
			ptr = new PCSTR[1];
			*((PCSTR*)ptr) = Read<PCSTR>(from);
			who->Write(to,*((PCSTR*)ptr));
			break;
		case TCM_DATA_CSTRW:
			ptr = new PCWSTR[1];
			*((PCWSTR*)ptr) = Read<PCWSTR>(from);
			who->Write(to,*((PCWSTR*)ptr));
			break;
		default:
			return;
		}
		delete [] ptr;
	}

	void Envelope::CastDeliver(Envelope* who, int from, int to)
	{
		if(from >= _Total || to >= who->_Total) throw runtime_error(_tcm_env_err_1);
		int type = _Types[from];
		if(type == TCM_DATA_POINTER)
		{
			LPVOID* ptr = new LPVOID[1];
			ptr[0] = Read<LPVOID>(from);
			return who->Write(to,ptr[0]);
		}
		else
		{
			PCWSTR value = CastReadW(from);
			return who->CastWriteW(to,value);
		}
	}
}