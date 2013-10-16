#include "stdafx.h"
#include "tcm_driver.h"
#include "tcm_library.h"
#include "tcm_Invoker.h"
#include "tcm_xml.h"

namespace tcm
{
	using rapidxml::xml_node;
	using rapidxml::xml_document;
	using std::wstring;

	int Library::_Count = 0;

	Library::Library()
	{
		_Dir = NULL;
		_LibId = NULL;
	}

	Library::~Library()
	{
		Clear(_Dir);
		Clear(_LibId);
	}

	Library* Library::Load(PCWSTR path)
	{
		PCSTR data = NULL;
		xml_document<>* xdoc = (xml_document<>*)xml::Load(path, data);
		if(xdoc == NULL) return NULL;

		xml_node<>* xeroot = xdoc->first_node("root");
		DriverHub* drv_hub = DriverHub::GetInstance();
		PCSTR drv_id = xml::ValueA(xeroot->first_node("runtime"));
		Driver* driver = drv_hub->GetDriver(drv_id);
		delete drv_id;
		if(driver == NULL)
		{
			Clear(data);
			return NULL;
		}
		Library* lib = driver->CreateLibrary();
		lib->_LibId = xml::ValueW(xeroot->first_node("id"));
		lib->_Dir = GetDirPath(path, true);
		delete data;
		return lib;
	}

	int Library::GetCount()
	{
		return Library::_Count;
	}

	PCWSTR Library::GetLibraryId()
	{
		return _LibId;
	}

	Envelope* Library::CreateEnvelope(int fid)
	{
		wstring str = _Dir;
		str += _LibId;
		str += L".tcm.xml";
		Envelope* env = Envelope::Load(str.c_str(), fid);
		return env;
	}

	Invoker* Library::CreateInvoker(int fid)
	{
		DriverHub* drv_hub = DriverHub::GetInstance();
		Driver* driver = drv_hub->GetDriver(GetRuntimeId());
		if(driver == NULL) return NULL;
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