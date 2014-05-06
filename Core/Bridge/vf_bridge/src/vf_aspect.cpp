#include "vf_aspect.h"
#include "vf_xml.h"
#include "vf_library.h"
#include "vf_invoker.h"
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
		_Invoker = null;
	}

	Aspect::~Aspect()
	{
		Clear(_Id);
		Clear(_Contact);
		Clear(_LibraryId);
		Clear(_MethodId);
		Clear(_Invoker);
	}

	Aspect* Aspect::Load(pcstr path)
	{
		XML* xml = XML::Load(path);
		Scoped autop_xml(xml);
		if(xml == null)
			return null;

		raw xdoc = xml->GetEntity();
		raw xe_aspe = XML::XElem(xdoc, "aspect");
		raw xe_aspe_id = XML::XElem(xe_aspe, "id");
		raw xe_lib = XML::XElem(xe_aspe, "library");
		raw xe_mt = XML::XElem(xe_aspe, "method");
		raw xe_mode = XML::XElem(xe_aspe, "mode");
		raw xe_contact = XML::XElem(xe_aspe, "contact");
		
		Aspect* aspect = new Aspect();
		aspect->_Id = XML::ValStr(xe_aspe_id);
		aspect->_LibraryId = XML::ValStr(xe_lib);
		aspect->_MethodId = XML::ValStr(xe_mt);
		aspect->_Contact = XML::ValStr(xe_contact);
		aspect->_Async = XML::ValBool(xe_mode, "true");
		return aspect;
	}

	pcstr Aspect::GetAspectId()
	{
		return _Id;
	}

	uint8 Aspect::GetMode()
	{
		return _Mode;
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

	Invoker* Aspect::GetInvoker()
	{
		if(_Invoker == null)
		{
			LibraryHub* lib_hub = LibraryHub::GetInstance();
			Library* lib = lib_hub->GetLibrary(_LibraryId);
			if(lib != null)
				_Invoker = lib->CreateInvoker(_MethodId);
		}
		return _Invoker;
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