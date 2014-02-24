#include "vf_envelope.h"
#include "vf_xml.h"

namespace vapula
{
	Envelope::Envelope()
	{
		_Vars = null;
	}

	Envelope::~Envelope()
	{
		Zero();
		if(_Vars == null)
			return;
		for (int i = 0; i < _Total; i++)
			if(_Vars[i] != null)
				delete _Vars[i];
		delete _Vars;
	}

	Envelope* Envelope::Parse(pcstr xml)
	{
		XML* xobj = XML::Parse(xml);
		Handle autop_xml(xobj);
		if(xobj == null)
			return null;
		object xdoc = xobj->GetEntity();

		vector<int32> v_id;
		vector<int8> v_type;
		vector<int8> v_mode;

		object xe = XML::XPath(xdoc, 2, "params", "param");
		int total = 0;
		while(xe != null)
		{
			v_id.push_back(XML::ValInt(XML::XAttr(xe, "id")));
			v_type.push_back((int8)XML::ValInt(XML::XElem(xe, "type")));
			v_mode.push_back((int8)XML::ValInt(XML::XElem(xe, "mode")));
			xe = XML::Next(xe);
			total++;
		}

		Envelope* env = new Envelope(total);
		for(int i = 0; i < total; i++)
		{
			int idx = v_id[i] - 1;
			env->_Types[idx] = v_type[i];
			env->_Modes[idx] = v_mode[i];
		}
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

	int32 Envelope::GetTotal()
	{
		return _Total;
	}

	void Envelope::Zero()
	{
		for(int i = 0; i < _Total; i++)
		{
			if(_Vars[i] != null)
				_Vars[i]->Zero();
		}
	}

	Envelope* Envelope::Copy()
	{
		Envelope* env = new Envelope();
		env->_Total = _Total;
		env->_Vars = new PVar[_Total];
		for(int i = 0; i < _Total; i++)
			env->_Vars[i] = _Vars[i]->Copy();
		return env;
	}

	void Envelope::Deliver(Envelope* who, int from, int to)
	{
		if(!_AssertId(from) || !_AssertId(to, who))
			throw invalid_argument(_vf_err_1);
		_Vars[from - 1]->Deliver(who->_Vars[to - 1]);
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
			object ptr = ReadObject(from, false);
			who->WriteObject(to, ptr, size, false);
		}
		else
		{
			pcstr value = CastRead(from);
			return who->CastWrite(to, value);
		}
	}
}