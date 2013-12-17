#include "vf_driver.h"
#include "vf_library.h"
#include "vf_invoker.h"
#include "vf_xml.h"
#include "rapidxml/rapidxml.hpp"

namespace vapula
{
	using rapidxml::xml_node;
	using rapidxml::xml_document;
	using std::wstring;

	int Library::_Count = 0;

	Library::Library()
	{
		_Dir = null;
		_LibId = null;
	}

	Library::~Library()
	{
		Clear(_Dir);
		Clear(_LibId);
	}

	Library* Library::Load(cstr8 path)
	{
		cstr8 data = null;
		xml_document<>* xdoc 
			= (xml_document<>*)xml::Load(path, data);
		if(xdoc == null) 
			return null;

		xml_node<>* xeroot = xdoc->first_node("library");
		DriverHub* drv_hub = DriverHub::GetInstance();
		cstr8 drv_id = xml::ValueCh8(xeroot->first_node("runtime"));
		Driver* driver = drv_hub->GetDriver(drv_id);
		delete drv_id;
		if(driver == null)
		{
			Clear(data);
			return null;
		}
		Library* lib = driver->CreateLibrary();
		lib->_LibId = xml::ValueCh8(xeroot->first_node("id"));
		lib->_Dir = GetDirPath(path, true);
		delete data;
		return lib;
	}

	int Library::GetCount()
	{
		return Library::_Count;
	}

	cstr8 Library::GetLibraryId()
	{
		return _LibId;
	}

	Envelope* Library::CreateEnvelope(int fid)
	{
		string s = _Dir;
		s += _LibId;
		s += ".library";
		Envelope* env = Envelope::Load(s.c_str(), fid);
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

	bool Library::Mount()
	{
		Library::_Count++;
		return true;
	}

	void Library::Unmount()
	{
		Library::_Count--;
	}
}