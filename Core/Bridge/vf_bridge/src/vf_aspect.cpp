#include "vf_aspect.h"
#include "vf_aspect_hub.h"
#include "vf_library.h"
#include "vf_library_hub.h"
#include "vf_task.h"
#include "vf_xml.h"

#include <regex>

namespace vapula
{
	AspectHub* Aspect::_Hub = null;

	AspectHub* Aspect::Hub()
	{
		if (_Hub == null)
		{
			Lock* lock = Lock::GetCtorLock();
			lock->Enter();
			if (_Hub == null)
				_Hub = new AspectHub();
			lock->Leave();
		}
		return _Hub;
	}

	Aspect* Aspect::Find(pcstr id)
	{
		AspectHub* hub = Aspect::Hub();
		return hub->Find(id);
	}

	int Aspect::Count()
	{
		AspectHub* hub = Aspect::Hub();
		return hub->Count();
	}

	Aspect::Aspect()
	{
		_AspectId = null;
		_Contact = null;
		_LibraryId = null;
		_MethodId = null;
		_Async = false;
	}

	Aspect::~Aspect()
	{
		Clear(_AspectId);
		Clear(_Contact);
		Clear(_LibraryId);
		Clear(_MethodId);
	}

	Aspect* Aspect::Load(pcstr path)
	{
		XML* xml = XML::Load(path);
		Scoped autop_xml(xml);
		if(xml == null)
			return null;

		raw xdoc = xml->GetEntity();
		raw xe_aspect = XML::XElem(xdoc, "aspect");
		raw xe_id = XML::XElem(xe_aspect, "id");
		raw xe_library = XML::XElem(xe_aspect, "library");
		raw xe_method = XML::XElem(xe_aspect, "method");
		raw xe_async = XML::XElem(xe_aspect, "async");
		raw xe_contact = XML::XElem(xe_aspect, "contact");
		
		Aspect* aspect = new Aspect();
		aspect->_AspectId = XML::ValStr(xe_id);
		aspect->_LibraryId = XML::ValStr(xe_library);
		aspect->_MethodId = XML::ValStr(xe_method);
		aspect->_Contact = XML::ValStr(xe_contact);
		aspect->_Async = XML::ValBool(xe_async, "true");
		return aspect;
	}

	pcstr Aspect::GetAspectId()
	{
		return _AspectId;
	}

	bool Aspect::IsAsync()
	{
		return _Async;
	}

	pcstr Aspect::GetContact()
	{
		return _Contact;
	}

	pcstr Aspect::GetLibraryId()
	{
		return _LibraryId;
	}

	pcstr Aspect::GetMethodId()
	{
		return _MethodId;
	}

	Task* Aspect::CreateTask()
	{
		Library* library = Library::Find(_LibraryId);
		Task* task = null;
		if (library != null)
			task = library->CreateTask(_MethodId);
		return task;
	}

	bool Aspect::TryMatch(pcstr frame)
	{
		using std::regex;
		const regex pattern(_Contact);
		string str_frame = frame;
		bool match = std::regex_match(str_frame, pattern);
		return match;
	}

	void Aspect::LinkHub()
	{
		AspectHub* hub = Aspect::Hub();
		hub->Link(this);
	}

	void Aspect::KickHub()
	{
		AspectHub* hub = Aspect::Hub();
		hub->Kick(_AspectId);
	}
}