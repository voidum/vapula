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
		if(!AssertId(id)) throw runtime_error(_tcm_env_err_1);
		return _InStates[id - 1];
	}

	PCSTR Envelope::CastReadA(int id)
	{
		if(!AssertId(id)) throw runtime_error(_tcm_env_err_1);
		int iid = id - 1;
		int type = _Types[iid];
		switch(type)
		{
		case TCM_DATA_POINTER:	return ValueToStrA((ULONG)Read<LPVOID>(iid));
		case TCM_DATA_INT8:	return ValueToStrA(Read<char>(iid));
		case TCM_DATA_UINT8:	return ValueToStrA(Read<BYTE>(iid));
		case TCM_DATA_INT16:	return ValueToStrA(Read<short>(iid));
		case TCM_DATA_UINT16:	return ValueToStrA(Read<USHORT>(iid));
		case TCM_DATA_INT32:	return ValueToStrA(Read<int>(iid));
		case TCM_DATA_UINT32:	return ValueToStrA(Read<UINT>(iid));
		case TCM_DATA_INT64:	return ValueToStrA(Read<LONGLONG>(iid));
		case TCM_DATA_UINT64:	return ValueToStrA(Read<ULONGLONG>(iid));
		case TCM_DATA_REAL32:	return ValueToStrA(Read<float>(iid));
		case TCM_DATA_REAL64:	return ValueToStrA(Read<double>(iid));
		case TCM_DATA_BOOL:	return Read<bool>(iid) ? "true" : "false";
		case TCM_DATA_CSTRA:	return CopyStrA(Read<PCSTR>(iid));
		case TCM_DATA_CSTRW:	return WcToMb(Read<PCWSTR>(iid));
		default:				return NULL;
		}
	}

	PCWSTR Envelope::CastReadW(int id)
	{
		if(!AssertId(id)) throw runtime_error(_tcm_env_err_1);
		int iid = id - 1;
		int type = _Types[iid];
		switch(type)
		{
		case TCM_DATA_POINTER:	return ValueToStrW((ULONG)Read<LPVOID>(iid));
		case TCM_DATA_INT8:	return ValueToStrW(Read<char>(iid));
		case TCM_DATA_UINT8:	return ValueToStrW(Read<BYTE>(iid));
		case TCM_DATA_INT16:	return ValueToStrW(Read<short>(iid));
		case TCM_DATA_UINT16:	return ValueToStrW(Read<USHORT>(iid));
		case TCM_DATA_INT32:	return ValueToStrW(Read<int>(iid));
		case TCM_DATA_UINT32:	return ValueToStrW(Read<UINT>(iid));
		case TCM_DATA_INT64:	return ValueToStrW(Read<LONGLONG>(iid));
		case TCM_DATA_UINT64:	return ValueToStrW(Read<ULONGLONG>(iid));
		case TCM_DATA_REAL32:	return ValueToStrW(Read<float>(iid));
		case TCM_DATA_REAL64:	return ValueToStrW(Read<double>(iid));
		case TCM_DATA_BOOL:	return Read<bool>(iid) ? L"true" : L"false";
		case TCM_DATA_CSTRA:	return MbToWc(Read<PCSTR>(iid));
		case TCM_DATA_CSTRW:	return CopyStrW(Read<PCWSTR>(iid));
		default:				return NULL;
		}
	}

	void Envelope::CastWriteA(int id, PCSTR value)
	{
		if(!AssertId(id)) throw runtime_error(_tcm_env_err_1);
		if(value == NULL) throw runtime_error(_tcm_env_err_2);
		int iid = id - 1;
		int type = _Types[iid];
		switch(type)
		{
		case TCM_DATA_POINTER:	Write(iid, (LPVOID)abs(atoi(value)),0); return;
		case TCM_DATA_INT8:	Write(iid, (char)atoi(value)); return;
		case TCM_DATA_UINT8:	Write(iid, (BYTE)abs(atoi(value))); return;
		case TCM_DATA_INT16:	Write(iid, (short)atoi(value)); return;
		case TCM_DATA_UINT16:	Write(iid, (USHORT)abs(atoi(value))); return;
		case TCM_DATA_INT32:	Write(iid, atoi(value)); return;
		case TCM_DATA_UINT32:	Write(iid, (unsigned)abs(atoi(value))); return;
		case TCM_DATA_INT64:	Write(iid, _atoi64(value)); return;
		case TCM_DATA_UINT64:	Write(iid, (unsigned)abs(_atoi64(value))); return;
		case TCM_DATA_REAL32:	Write(iid, atof(value)); return;
		case TCM_DATA_REAL64:	Write(iid, atof(value)); return;
		case TCM_DATA_BOOL:	Write(iid, (strcmp(value,"true") == 0) ? TRUE : FALSE); return;
		case TCM_DATA_CSTRA:	Write(iid, value); return;
		case TCM_DATA_CSTRW:	Write(iid, MbToWc(value)); return;
		default:				return;
		}
	}

	void Envelope::CastWriteW(int id,PCWSTR value)
	{
		if(!AssertId(id)) throw runtime_error(_tcm_env_err_1);
		if(value == NULL) throw runtime_error(_tcm_env_err_2);
		int iid = id - 1;
		int type = _Types[iid];
		if(type == TCM_DATA_CSTRW)
		{
			Write(iid, value);
			return;
		}
		PCSTR str = WcToMb(value);
		CastWriteA(iid,str);
	}

	void Envelope::Deliver(Envelope* who, int from, int to)
	{
		if(!AssertId(from) || !AssertId(to, who)) throw runtime_error(_tcm_env_err_1);
		int ifrom = from - 1;
		int ito = to - 1;
		int type = _Types[ifrom];
		if(type != who->_Types[ito]) throw runtime_error(_tcm_env_err_3);

		LPVOID ptr = NULL;
		switch(type)
		{
		case TCM_DATA_POINTER:
			ptr = new LPVOID[1];
			*((LPVOID*)ptr) = Read<LPVOID>(ifrom);
			who->Write(ito, ptr);
			break;
		case TCM_DATA_INT8:
			ptr = new char[1];
			*((char*)ptr) = Read<char>(ifrom);
			who->Write(ito, *((char*)ptr));
			break;
		case TCM_DATA_UINT8:
			ptr = new BYTE[1];
			*((BYTE*)ptr) = Read<BYTE>(ifrom);
			who->Write(ito, *((BYTE*)ptr));
			break;
		case TCM_DATA_INT16:
			ptr = new short[1];
			*((short*)ptr) = Read<short>(ifrom);
			who->Write(ito, *((short*)ptr));
			break;
		case TCM_DATA_UINT16:
			ptr = new USHORT[1];
			*((USHORT*)ptr) = Read<USHORT>(ifrom);
			who->Write(ito, *((USHORT*)ptr));
			break;
		case TCM_DATA_INT32:
			ptr = new int[1];
			*((int*)ptr) = Read<int>(ifrom);
			who->Write(ito, *((int*)ptr));
			break;
		case TCM_DATA_UINT32:
			ptr = new UINT[1];
			*((UINT*)ptr) = Read<UINT>(ifrom);
			who->Write(ito, *((UINT*)ptr));
			break;
		case TCM_DATA_INT64:
			ptr = new LONGLONG[1];
			*((LONGLONG*)ptr) = Read<LONGLONG>(ifrom);
			who->Write(ito, *((LONGLONG*)ptr));
			break;
		case TCM_DATA_UINT64:
			ptr = new ULONGLONG[1];
			*((ULONGLONG*)ptr) = Read<ULONGLONG>(ifrom);
			who->Write(ito, *((ULONGLONG*)ptr));
			break;
		case TCM_DATA_REAL32:
			ptr = new float[1];
			*((float*)ptr) = Read<float>(ifrom);
			who->Write(ito, *((float*)ptr));
			break;
		case TCM_DATA_REAL64:
			ptr = new double[1];
			*((double*)ptr) = Read<double>(ifrom);
			who->Write(ito, *((double*)ptr));
			break;
		case TCM_DATA_BOOL:
			ptr = new char[1];
			*((char*)ptr) = Read<char>(ifrom);
			who->Write(ito, *((char*)ptr));
			break;
		case TCM_DATA_CSTRA:
			ptr = new PCSTR[1];
			*((PCSTR*)ptr) = Read<PCSTR>(ifrom);
			who->Write(ito, *((PCSTR*)ptr));
			break;
		case TCM_DATA_CSTRW:
			ptr = new PCWSTR[1];
			*((PCWSTR*)ptr) = Read<PCWSTR>(ifrom);
			who->Write(ito, *((PCWSTR*)ptr));
			break;
		default:
			return;
		}
		delete [] ptr;
	}

	void Envelope::CastDeliver(Envelope* who, int from, int to)
	{
		if(!AssertId(from) || !AssertId(to, who)) throw runtime_error(_tcm_env_err_1);
		int ifrom = from - 1;
		int ito = to - 1;
		int type = _Types[ifrom];
		if(type == TCM_DATA_POINTER)
		{
			LPVOID* ptr = new LPVOID[1];
			ptr[0] = Read<LPVOID>(ifrom);
			return who->Write(ito, ptr[0]);
		}
		else
		{
			PCWSTR value = CastReadW(ifrom);
			return who->CastWriteW(ito, value);
		}
	}
}