#include "vf_aspect.h"
#include "vf_runtime.h"
#include "vf_library.h"
#include "vf_task.h"
#include "vf_xml.h"

#include <regex>

namespace vapula
{
	Aspect::Aspect()
	{
		_Id = null;
		_Contact = null;
		_LibraryId = null;
		_MethodId = null;
		_Async = false;
		_Task = null;
	}

	Aspect::~Aspect()
	{
		Clear(_Id);
		Clear(_Contact);
		Clear(_LibraryId);
		Clear(_MethodId);
		Clear(_Task);
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
		aspect->_Id = XML::ValStr(xe_id);
		aspect->_LibraryId = XML::ValStr(xe_library);
		aspect->_MethodId = XML::ValStr(xe_method);
		aspect->_Contact = XML::ValStr(xe_contact);
		aspect->_Async = XML::ValBool(xe_async, "true");
		return aspect;
	}

	pcstr Aspect::GetAspectId()
	{
		return _Id;
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

	Task* Aspect::GetTask()
	{
		if(_Task == null)
		{
			Runtime* runtime = Runtime::Instance();
			Library* library = (Library*)runtime->SelectObject(VF_CORE_LIBRARY, _LibraryId);
			if (library != null)
				_Task = library->CreateTask(_MethodId);
		}
		return _Task;
	}

	bool Aspect::TryMatch(pcstr frame)
	{
		using std::regex;
		const regex pattern(_Contact);
		string str_frame = frame;
		bool match = std::regex_match(str_frame, pattern);
		return match;
	}
}