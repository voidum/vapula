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
			for(int i=0; i<total; i++)
			{
				int id = v_id[i] - 1;
				arr_type[id] = v_type[i];
				arr_in[id] = v_in[i];
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
		if(!AssertId(id)) throw invalid_argument(_tcm_env_err_1);
		return _InStates[id - 1];
	}

	void Envelope::_Write(int id, LPVOID value, int size)
	{
		if(!AssertId(id)) throw invalid_argument(_tcm_env_err_1);
		LPVOID* param = (LPVOID*)((UINT)_Memory + _Offsets[id - 1]);
		if(size > 0)
		{
			//Ç³¿½±´
			param[0] = new BYTE[size];
			memcpy(param[0], value, size);
		}
		else
		{
			//ÒýÓÃ¸´ÖÆ
			param[0] = value;
		}
	}

	PCSTR Envelope::CastReadA(int id)
	{
		if(!AssertId(id)) throw invalid_argument(_tcm_env_err_1);
		int type = _Types[id - 1];
		switch(type)
		{
		case TCM_DATA_POINTER:	return ValueToStr((ULONG)Read<LPVOID>(id));
		case TCM_DATA_INT8:	return ValueToStr(Read<char>(id));
		case TCM_DATA_UINT8:	return ValueToStr(Read<BYTE>(id));
		case TCM_DATA_INT16:	return ValueToStr(Read<short>(id));
		case TCM_DATA_UINT16:	return ValueToStr(Read<USHORT>(id));
		case TCM_DATA_INT32:	return ValueToStr(Read<int>(id));
		case TCM_DATA_UINT32:	return ValueToStr(Read<UINT>(id));
		case TCM_DATA_INT64:	return ValueToStr(Read<LONGLONG>(id));
		case TCM_DATA_UINT64:	return ValueToStr(Read<ULONGLONG>(id));
		case TCM_DATA_REAL32:	return ValueToStr(Read<float>(id));
		case TCM_DATA_REAL64:	return ValueToStr(Read<double>(id));
		case TCM_DATA_BOOL:	return Read<bool>(id) ? "true" : "false";
		case TCM_DATA_STRING:	return WcToMb(Read<PCWSTR>(id));
		default:				return NULL;
		}
	}

	PCWSTR Envelope::CastReadW(int id)
	{
		if(!AssertId(id)) throw invalid_argument(_tcm_env_err_1);
		int type = _Types[id - 1];
		if(type == TCM_DATA_STRING)
			return CopyStrW(Read<PCWSTR>(id));
		else if(type == TCM_DATA_BOOL)
			return Read<bool>(id) ? L"true" : L"false";
		else 
			return MbToWc(CastReadA(id));
	}

	void Envelope::CastWriteA(int id, PCSTR value)
	{
		if(value == NULL) throw invalid_argument(_tcm_env_err_2);
		if(!AssertId(id)) throw invalid_argument(_tcm_env_err_1);
		int type = _Types[id - 1];
		switch(type)
		{
		case TCM_DATA_POINTER:	_Write(id, (LPVOID)abs(atoi(value)),0); return;
		case TCM_DATA_INT8:	Write(id, (char)atoi(value)); return;
		case TCM_DATA_UINT8:	Write(id, (BYTE)abs(atoi(value))); return;
		case TCM_DATA_INT16:	Write(id, (short)atoi(value)); return;
		case TCM_DATA_UINT16:	Write(id, (USHORT)abs(atoi(value))); return;
		case TCM_DATA_INT32:	Write(id, atoi(value)); return;
		case TCM_DATA_UINT32:	Write(id, (unsigned)abs(atoi(value))); return;
		case TCM_DATA_INT64:	Write(id, _atoi64(value)); return;
		case TCM_DATA_UINT64:	Write(id, (unsigned)abs(_atoi64(value))); return;
		case TCM_DATA_REAL32:	Write(id, atof(value)); return;
		case TCM_DATA_REAL64:	Write(id, atof(value)); return;
		case TCM_DATA_BOOL:	Write(id, (strcmp(value,"true") == 0) ? TRUE : FALSE); return;
		case TCM_DATA_STRING:	Write(id, MbToWc(value)); return;
		default:				return;
		}
	}

	void Envelope::CastWriteW(int id, PCWSTR value)
	{
		if(value == NULL) throw invalid_argument(_tcm_env_err_2);
		if(!AssertId(id)) throw invalid_argument(_tcm_env_err_1);
		int type = _Types[id - 1];
		if(type == TCM_DATA_STRING)
		{
			Write(id, value);
			return;
		}
		PCSTR str = WcToMb(value);
		CastWriteA(id, str);
	}

	void Envelope::Deliver(Envelope* who, int from, int to)
	{
		if(!AssertId(from) || !AssertId(to, who))
			throw invalid_argument(_tcm_env_err_1);
		int type = _Types[from - 1];
		if(type != who->_Types[to - 1])
			throw invalid_argument(_tcm_env_err_3);

		LPVOID ptr = NULL;
		switch(type)
		{
		case TCM_DATA_POINTER:
			ptr = new LPVOID[1];
			*((LPVOID*)ptr) = Read<LPVOID>(from);
			who->Write(to, ptr);
			break;
		case TCM_DATA_INT8:
			ptr = new char[1];
			*((char*)ptr) = Read<char>(from);
			who->Write(to, *((char*)ptr));
			break;
		case TCM_DATA_UINT8:
			ptr = new BYTE[1];
			*((BYTE*)ptr) = Read<BYTE>(from);
			who->Write(to, *((BYTE*)ptr));
			break;
		case TCM_DATA_INT16:
			ptr = new short[1];
			*((short*)ptr) = Read<short>(from);
			who->Write(to, *((short*)ptr));
			break;
		case TCM_DATA_UINT16:
			ptr = new USHORT[1];
			*((USHORT*)ptr) = Read<USHORT>(from);
			who->Write(to, *((USHORT*)ptr));
			break;
		case TCM_DATA_INT32:
			ptr = new int[1];
			*((int*)ptr) = Read<int>(from);
			who->Write(to, *((int*)ptr));
			break;
		case TCM_DATA_UINT32:
			ptr = new UINT[1];
			*((UINT*)ptr) = Read<UINT>(from);
			who->Write(to, *((UINT*)ptr));
			break;
		case TCM_DATA_INT64:
			ptr = new LONGLONG[1];
			*((LONGLONG*)ptr) = Read<LONGLONG>(from);
			who->Write(to, *((LONGLONG*)ptr));
			break;
		case TCM_DATA_UINT64:
			ptr = new ULONGLONG[1];
			*((ULONGLONG*)ptr) = Read<ULONGLONG>(from);
			who->Write(to, *((ULONGLONG*)ptr));
			break;
		case TCM_DATA_REAL32:
			ptr = new float[1];
			*((float*)ptr) = Read<float>(from);
			who->Write(to, *((float*)ptr));
			break;
		case TCM_DATA_REAL64:
			ptr = new double[1];
			*((double*)ptr) = Read<double>(from);
			who->Write(to, *((double*)ptr));
			break;
		case TCM_DATA_BOOL:
			ptr = new char[1];
			*((char*)ptr) = Read<char>(from);
			who->Write(to, *((char*)ptr));
			break;
		case TCM_DATA_STRING:
			ptr = new PCWSTR[1];
			*((PCWSTR*)ptr) = Read<PCWSTR>(from);
			who->Write(to, *((PCWSTR*)ptr));
			break;
		default:
			return;
		}
		delete [] ptr;
	}

	void Envelope::CastDeliver(Envelope* who, int from, int to)
	{
		if(!AssertId(from) || !AssertId(to, who))
			throw invalid_argument(_tcm_env_err_1);
		int type = _Types[from - 1];
		if(type == TCM_DATA_POINTER)
		{
			LPVOID* ptr = new LPVOID[1];
			ptr[0] = Read<LPVOID>(from);
			return who->Write(to, ptr[0]);
		}
		else
		{
			PCWSTR value = CastReadW(from);
			return who->CastWriteW(to, value);
		}
	}
}