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

		list<int> ids;
		list<PVar> vars;
		object xe = XML::XPath(xdoc, 2, "params", "param");
		while(xe != null)
		{
			PVar var = Variable::Parse(xe);
			int id = XML::ValInt(XML::XAttr(xe, "id"));
			ids.push_back(id);
			vars.push_back(var);
			xe = XML::Next(xe);
		}

		Envelope* env = new Envelope();
		env->_Vars = new PVar[vars.size()];

		list<int>::iterator i1 = ids.begin();
		list<PVar>::iterator i2 = vars.begin();
		for(;;)
		{
			if(i1 == ids.end())
				break;
			int id = *i1;
			PVar var = *i2;
			env->_Vars[id - 1] = var;
			i1++;
			i2++;
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

	PVar Envelope::operator[](int id)
	{
		if(!_AssertId(id))
			return null;
		return _Vars[id - 1];
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
}