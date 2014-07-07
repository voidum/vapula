#include "vf_library.h"
#include "vf_library_hub.h"
#include "vf_driver.h"
#include "vf_driver_hub.h"
#include "vf_method.h"
#include "vf_task.h"
#include "vf_xml.h"

namespace vapula
{
	LibraryHub* Library::_Hub = null;

	LibraryHub* Library::Hub()
	{
		if (_Hub == null)
		{
			Lock* lock = Lock::GetCtorLock();
			lock->Enter();
			if (_Hub == null)
				_Hub = new LibraryHub();
			lock->Leave();
		}
		return _Hub;
	}

	Library* Library::Find(pcstr id)
	{
		LibraryHub* hub = Library::Hub();
		return hub->Find(id);
	}

	int Library::Count()
	{
		LibraryHub* hub = Library::Hub();
		return hub->Count();
	}

	Library::Library()
	{
		_LibraryId = null;
	}

	Library::~Library()
	{
		typedef list<Method*>::iterator iter;
		for (iter i = _Methods.begin(); i != _Methods.end(); i++)
			Clear(*i);
		_Methods.clear();
		Clear(_LibraryId);
	}

	Library* Library::Load(pcstr path)
	{
		XML* xml = XML::Load(path);
		Scoped autop_xml(xml);
		if (xml == null)
			return null;

		raw xdoc = xml->GetEntity();
		raw xe_library = XML::XElem(xdoc, "library");

		pcstr cs8_runtime = XML::ValStr(XML::XElem(xe_library, "runtime"));
		Scoped autop1((raw)cs8_runtime);

		Driver* driver = Driver::Find(cs8_runtime);
		if (driver == null)
			return null;

		Library* library = driver->CreateLibrary();
		library->_Driver = driver;

		raw xe_id = XML::XElem(xe_library, "id");
		library->_LibraryId = XML::ValStr(xe_id);

		pcstr cs8_dir = GetPathDir(path, true);
		Scoped autop2((raw)cs8_dir);
		ostringstream oss;
		oss << cs8_dir << library->_LibraryId << "." << driver->GetBinExt();
		library->_Path = str::Copy(oss.str().c_str());

		raw xe_method = XML::XPath(xe_library, 2, "methods", "method");
		while (xe_method != null)
		{
			Method* method = Method::Parse(xe_method);
			method->SetLibrary(library);
			library->_Methods.push_back(method);
			xe_method = XML::Next(xe_method);
		}
		return library;
	}

	Driver* Library::GetDriver()
	{
		return _Driver;
	}

	pcstr Library::GetLibraryId()
	{
		return _LibraryId;
	}

	Method* Library::GetMethod(pcstr id)
	{
		typedef list<Method*>::iterator iter;
		for (iter i = _Methods.begin(); i != _Methods.end(); i++)
		{
			Method* method = *i;
			if (strcmp(method->GetMethodId(), id) == 0)
				return method;
		}
		return null;
	}

	Task* Library::CreateTask(pcstr id)
	{
		Task* task = _Driver->CreateTask();
		Method* method = GetMethod(id);
		if (task->Bind(method))
			return task;
		else
		{
			delete task;
			return null;
		}
	}

	void Library::LinkHub()
	{
		LibraryHub* hub = Library::Hub();
		hub->Add(this);
	}

	void Library::KickHub()
	{
		LibraryHub* hub = Library::Hub();
		hub->Remove(this);
	}
}