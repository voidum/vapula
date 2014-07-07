#include "vf_library_hub.h"
#include "vf_library.h"

namespace vapula
{
	LibraryHub::LibraryHub()
	{
		_Lock = new Lock();
	}

	LibraryHub::~LibraryHub()
	{
		KickAll();
		Clear(_Lock);
	}

	list<Library*>& LibraryHub::GetInnerData()
	{
		return _Libraries;
	}

	int LibraryHub::Count()
	{
		return _Libraries.size();
	}

	Library* LibraryHub::Find(pcstr id)
	{
		typedef list<Library*>::iterator iter;
		_Lock->Enter();
		Library* library = null;
		for (iter i = _Libraries.begin(); i != _Libraries.end(); i++)
		{
			Library* tmp = *i;
			if (strcmp(tmp->GetLibraryId(), id) == 0)
			{
				library = tmp;
				break;
			}
		}
		_Lock->Leave();
		return library;
	}

	void LibraryHub::Link(Library* library)
	{
		typedef list<Library*>::iterator iter;
		_Lock->Enter();
		pcstr id = library->GetLibraryId();
		for (iter i = _Libraries.begin(); i != _Libraries.end(); i++)
		{
			Library* tmp = *i;
			if (strcmp(tmp->GetLibraryId(), id) == 0)
			{
				_Lock->Leave();
				return;
			}
		}
		_Libraries.push_back(library);
		_Lock->Leave();
	}

	void LibraryHub::Kick(pcstr id)
	{
		typedef list<Library*>::iterator iter;
		_Lock->Enter();
		for (iter i = _Libraries.begin(); i != _Libraries.end(); i++)
		{
			Library* tmp = *i;
			if (strcmp(tmp->GetLibraryId(), id) == 0)
			{
				Clear(tmp);
				_Libraries.erase(i);
				break;
			}
		}
		_Lock->Leave();
	}

	void LibraryHub::KickAll()
	{
		typedef list<Library*>::iterator iter;
		_Lock->Enter();
		for (iter i = _Libraries.begin(); i != _Libraries.end(); i++)
			Clear(*i);
		_Libraries.clear();
		_Lock->Leave();
	}
}