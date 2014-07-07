#include "vf_aspect_hub.h"
#include "vf_aspect.h"

namespace vapula
{
	AspectHub::AspectHub()
	{
		_Lock = new Lock();
	}

	AspectHub::~AspectHub()
	{
		RemoveAll();
		Clear(_Lock);
	}

	list<Aspect*>& AspectHub::GetInnerData()
	{
		return _Aspects;
	}

	int AspectHub::Count()
	{
		return _Aspects.size();
	}

	Aspect* AspectHub::Find(pcstr id)
	{
		typedef list<Aspect*>::iterator iter;
		_Lock->Enter();
		Aspect* aspect = null;
		for (iter i = _Aspects.begin(); i != _Aspects.end(); i++)
		{
			Aspect* tmp = *i;
			if (strcmp(tmp->GetAspectId(), id) == 0)
			{
				aspect = tmp;
				break;
			}
		}
		_Lock->Leave();
		return aspect;
	}

	void AspectHub::Add(Aspect* aspect)
	{
		typedef list<Aspect*>::iterator iter;
		_Lock->Enter();
		pcstr id = aspect->GetAspectId();
		for (iter i = _Aspects.begin(); i != _Aspects.end(); i++)
		{
			Aspect* tmp = *i;
			if (strcmp(tmp->GetAspectId(), id) == 0)
			{
				_Lock->Leave();
				return;
			}
		}
		_Aspects.push_back(aspect);
		_Lock->Leave();
	}

	void AspectHub::Remove(Aspect* aspect)
	{
		typedef list<Aspect*>::iterator iter;
		_Lock->Enter();
		for (iter i = _Aspects.begin(); i != _Aspects.end(); i++)
		{
			Aspect* tmp = *i;
			if (aspect == tmp)
			{
				Clear(tmp);
				_Aspects.erase(i);
				break;
			}
		}
		_Lock->Leave();
	}

	void AspectHub::RemoveAll()
	{
		typedef list<Aspect*>::iterator iter;
		_Lock->Enter();
		for (iter i = _Aspects.begin(); i != _Aspects.end(); i++)
			Clear(*i);
		_Aspects.clear();
		_Lock->Leave();
	}
}