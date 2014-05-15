#include "vf_library.h"
#include "vf_runtime.h"
#include "vf_driver.h"
#include "vf_method.h"
#include "vf_task.h"
#include "vf_xml.h"

namespace vapula
{
	Library::Library()
	{
		_Id = null;
	}

	Library::~Library()
	{
		Clear(_Id);
		ClearAll();
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

		Runtime* runtime = Runtime::Instance();
		Driver* driver = runtime->Select<Driver>(cs8_runtime);
		if (driver == null)
			return null;

		Library* library = driver->CreateLibrary();
		library->_Driver = driver;

		raw xe_id = XML::XElem(xe_library, "id");
		library->_Id = XML::ValStr(xe_id);

		pcstr cs8_dir = GetPathDir(path, true);
		Scoped autop2((raw)cs8_dir);
		ostringstream oss;
		oss << cs8_dir << library->_Id << "." << driver->GetBinExt();
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

	void Library::ClearAll()
	{
		typedef list<Method*>::iterator iter;
		for (iter i = _Methods.begin(); i != _Methods.end(); i++)
			Clear(*i);
		_Methods.clear();
	}

	Driver* Library::GetDriver()
	{
		return _Driver;
	}

	pcstr Library::GetLibraryId()
	{
		return _Id;
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
}