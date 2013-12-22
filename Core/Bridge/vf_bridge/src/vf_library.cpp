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
		_LibId = null;
		_EntryDpt = null;
		_FuncDpt = null;
	}

	Library::~Library()
	{
		Clear(_LibId);
		Clear(_EntryDpt);
		Clear(_FuncDpt);
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
		lib->_Dir = GetDirPath(path, true);
		lib->_LibId = xml::ValueCh8(xeroot->first_node("id"));
		lib->_EntryDpt = xml::ValueCh8(xeroot->first_node("entry"));
		lib->_FuncDpt = xml::Print(xeroot->first_node("functions"));

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