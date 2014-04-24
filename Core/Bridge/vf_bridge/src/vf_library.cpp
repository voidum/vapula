#include "vf_driver.h"
#include "vf_library.h"
#include "vf_method.h"
#include "vf_invoker.h"
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
		if(xml == null)
			return null;

		raw xdoc = xml->GetEntity();
		raw xe_lib = XML::XElem(xdoc, "library");

		pcstr cs8_rt = XML::ValStr(XML::XElem(xe_lib, "runtime"));
		Scoped autop1((raw)cs8_rt);

		DriverHub* drv_hub = DriverHub::GetInstance();
		Driver* drv = drv_hub->GetDriver(cs8_rt);
		if(drv == null && !drv_hub->Link(cs8_rt))
			return null;

		Library* lib = drv->CreateLibrary();
		lib->_Driver = drv;

		raw xe_lib_id = XML::XElem(xe_lib, "id");
		lib->_Id = XML::ValStr(xe_lib_id);

		pcstr path_dir = GetDirPath(path, true);
		Scoped autop2((raw)path_dir);
		ostringstream oss;
		oss<<path_dir<<lib->_Id<<"."<<drv->GetBinExt();
		lib->_Path = str::Copy(oss.str().c_str());

		raw xe_mt = XML::XPath(xe_lib, 2, "methods", "method");
		while(xe_mt != null)
		{
			pcstr cs8_mt = XML::Print(xe_mt);
			Method* mt = Method::Parse(cs8_mt);
			mt->SetLibrary(lib);
			lib->_Methods.push_back(mt);
			xe_mt = XML::Next(xe_mt);
			delete cs8_mt;
		}
		return lib;
	}

	void Library::ClearAll()
	{
		typedef list<Method*>::iterator iter;
		for(iter i=_Methods.begin(); i!=_Methods.end(); i++)
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
		for(iter i=_Methods.begin(); i!=_Methods.end(); i++)
		{
			Method* mt = *i;
			if(strcmp(mt->GetMethodId(), id) == 0)
				return mt;
		}
		return null;
	}

	Invoker* Library::CreateInvoker(pcstr id)
	{
		Invoker* inv = _Driver->CreateInvoker();
		Method* mt = GetMethod(id);
		if(inv->Bind(mt))
			return inv;
		else
		{
			delete inv;
			return null;
		}
	}


	LibraryHub* LibraryHub::_Instance = null;

	LibraryHub* LibraryHub::GetInstance()
	{
		Lock* lock = Lock::GetCtorLock();
		lock->Enter();
		if(LibraryHub::_Instance == null)
			LibraryHub::_Instance = new LibraryHub();
		lock->Leave();
		return LibraryHub::_Instance;
	}

	LibraryHub::LibraryHub() { }

	LibraryHub::~LibraryHub()
	{
		KickAll();
	}

	Library* LibraryHub::GetLibrary(pcstr id)
	{
		typedef list<Library*>::iterator iter;
		for(iter i = _Libraries.begin(); i != _Libraries.end(); i++)
		{
			Library* lib = *i;
			if(strcmp(lib->GetLibraryId(), id) == 0)
				return lib;
		}
		return null;
	}

	int LibraryHub::GetCount()
	{
		return _Libraries.size();
	}

	void LibraryHub::Link(Library* library)
	{
		Library* lib = GetLibrary(library->GetLibraryId());
		if(lib == null)
			_Libraries.push_back(library);
	}

	void LibraryHub::Kick(pcstr id)
	{
		typedef list<Library*>::iterator iter;
		for(iter i = _Libraries.begin(); i != _Libraries.end(); i++)
		{
			Library* lib = *i;
			if(strcmp(lib->GetLibraryId(), id) == 0)
			{
				_Libraries.erase(i);
				Clear(lib);
				break;
			}
		}
	}

	void LibraryHub::KickAll()
	{
		typedef list<Library*>::iterator iter;
		for(iter i = _Libraries.begin(); i != _Libraries.end(); i++)
			Clear(*i);
		_Libraries.clear();
	}
}