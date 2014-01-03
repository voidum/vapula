#include "vf_driver.h"
#include "vf_library.h"
#include "vf_invoker.h"
#include "vf_envelope.h"
#include "vf_xml.h"
#include "rapidxml/rapidxml.hpp"

namespace vapula
{
	using rapidxml::xml_node;
	using rapidxml::xml_document;
	using std::wstring;

	Library::Library()
	{
		_Id = null;
		_EntryDpt = null;
		_FuncDpt = null;
	}

	Library::~Library()
	{
		Clear(_Id);
		Clear(_EntryDpt);
		Clear(_FuncDpt);
	}

	Driver* Library::GetDriver()
	{
		return _Driver;
	}

	cstr8 Library::GetRuntimeId()
	{
		return _Driver->GetRuntimeId();
	}

	cstr8 Library::GetBinExt()
	{
		return _Driver->GetBinExt();
	}

	cstr8 Library::GetLibraryId()
	{
		return _Id;
	}

	cstr8 Library::GetEntryDpt()
	{
		return _EntryDpt;
	}

	Envelope* Library::CreateEnvelope(int fid)
	{
		cstr8 data = str::Copy(_FuncDpt);
		xml_node<>* xml = (xml_node<>*)xml::Parse(data);
		xml_node<>* xe = (xml_node<>*)xml::Path(xml, 2, "functions", "function");
		while (xe)
		{
			int tmpv = xml::ValueInt(xe->first_attribute("id"));
			if(tmpv == fid) break;
			xe = xe->next_sibling();
		}
		xe = xe->first_node("params");
		Envelope* env = Envelope::Parse(xe);
		delete data;
		return env;
	}

	Invoker* Library::CreateInvoker(int fid)
	{
		DriverHub* drv_hub = DriverHub::GetInstance();
		Driver* driver = drv_hub->GetDriver(GetRuntimeId());
		if(driver == null) 
			return null;
		Invoker* inv = driver->CreateInvoker();

		inv->Initialize(this, fid);
		return inv;
	}


	LibraryHub* LibraryHub::_Instance = null;

	LibraryHub* LibraryHub::GetInstance()
	{
		Lock* lock = Lock::GetCtorLock();
		if(lock->Enter())
		{
			if(LibraryHub::_Instance == null)
				LibraryHub::_Instance = new LibraryHub();
			lock->Leave();
		}
		return LibraryHub::_Instance;
	}

	LibraryHub::LibraryHub()
	{
		_Count = 0;
	}

	LibraryHub::~LibraryHub()
	{
		UnloadAll();
	}

	int LibraryHub::GetCount()
	{
		return _Count;
	}

	Library* LibraryHub::GetLibrary(cstr8 id)
	{
		typedef vector<Library*>::iterator iter;
		for(iter i = _Libraries.begin(); i != _Libraries.end(); i++)
		{
			Library* lib = *i;
			if(strcmp(id, lib->GetLibraryId()) == 0)
				return lib;
		}
		return null;
	}

	Library* LibraryHub::Load(cstr8 path)
	{
		cstr8 data = null;
		xml_document<>* xdoc 
			= (xml_document<>*)xml::Load(path, data);
		if(xdoc == null) 
			return null;

		xml_node<>* xeroot = xdoc->first_node("library");

		DriverHub* drv_hub = DriverHub::GetInstance();
		cstr8 runtime = xml::ValueCh8(xeroot->first_node("runtime"));
		Driver* driver = drv_hub->GetDriver(runtime);
		delete runtime;
		if(driver == null)
		{
			delete data;
			return null;
		}
		
		Library* lib = driver->CreateLibrary();
		lib->_Dir = GetDirPath(path, true);
		lib->_Id = xml::ValueCh8(xeroot->first_node("id"));
		lib->_EntryDpt = xml::ValueCh8(xeroot->first_node("entry"));
		lib->_FuncDpt = xml::Print(xeroot->first_node("functions"));
		lib->_Driver = driver;

		_Count++;

		delete data;
		return lib;
	}

	void LibraryHub::Unload(cstr8 id)
	{
		typedef vector<Library*>::iterator iter;
		for(iter i = _Libraries.begin(); i != _Libraries.end(); i++)
		{
			Library* lib = *i;
			if(strcmp(id, lib->GetLibraryId()) == 0)
			{
				_Libraries.erase(i);
				Clear(lib);
				break;
			}
		}
	}

	void LibraryHub::UnloadAll()
	{
		typedef vector<Library*>::iterator iter;
		for(iter i = _Libraries.begin(); i != _Libraries.end(); i++)
			Clear(*i);
		_Libraries.clear();
	}
}