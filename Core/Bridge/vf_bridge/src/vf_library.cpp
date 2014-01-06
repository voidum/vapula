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
		_EntrySym = null;
	}

	Library::~Library()
	{
		Clear(_Id);
		Clear(_EntrySym);
		for(int i = 0; i < _Total; i++)
			Clear(_Envelopes[i]);
		Clear(_Envelopes, true);
	}

	Driver* Library::GetDriver()
	{
		return _Driver;
	}

	cstr8 Library::GetRuntimeId()
	{
		return _Driver->GetRuntimeId();
	}

	cstr8 Library::GetLibraryId()
	{
		return _Id;
	}

	cstr8 Library::GetEntrySym()
	{
		return _EntrySym;
	}

	Envelope* Library::CreateEnvelope(int fid)
	{
		return _Envelopes[fid]->Copy();
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
	}

	LibraryHub::~LibraryHub()
	{
		UnloadAll();
	}

	int LibraryHub::GetCount()
	{
		return _Libraries.size();
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

		xml_node<>* xe_lib = xdoc->first_node("library");

		DriverHub* drv_hub = DriverHub::GetInstance();
		cstr8 runtime = xml::ValueCh8(xe_lib->first_node("runtime"));
		Driver* driver = drv_hub->GetDriver(runtime);
		delete runtime;
		if(driver == null)
		{
			delete data;
			return null;
		}
		
		Library* lib = driver->CreateLibrary();
		lib->_Path = GetDirPath(path, true);
		lib->_Id = xml::ValueCh8(xe_lib->first_node("id"));
		lib->_EntrySym = xml::ValueCh8(xe_lib->first_node("entry"));
		
		vector<Envelope*> func_envs;
		vector<int> func_ids;
		xml_node<>* xe_func = 
			xe_lib->first_node("functions")->first_node("function");
		while(xe_func != null)
		{
			xml_node<>* xe_params = xe_func->first_node("params");
			Envelope* envelope = Envelope::Parse(xe_params);
			func_ids.push_back(xml::ValueInt(xe_func->first_attribute("id")));
			func_envs.push_back(envelope);
			xe_func = xe_func->next_sibling();
		}
		int total = func_ids.size();
		lib->_Envelopes = new PtrEnvelope[total];
		for(int i = 0; i < total; i++)
		{
			int id = func_ids[i];
			lib->_Envelopes[id] = func_envs[i];
		}
		func_envs.clear();
		func_ids.clear();

		lib->_Total = total;
		lib->_Driver = driver;

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