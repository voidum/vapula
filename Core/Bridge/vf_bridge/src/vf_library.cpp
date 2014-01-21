#include "vf_driver.h"
#include "vf_library.h"
#include "vf_function.h"
#include "vf_invoker.h"
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
	}

	Library::~Library()
	{
		Clear(_Id);
		ClearAll();
	}

	Library* Library::Load(cstr8 path)
	{
		cstr8 data = null;
		xml_document<>* xdoc 
			= (xml_document<>*)xml::Load(path, data);
		if(xdoc == null) 
			return null;

		xml_node<>* xe_lib = xdoc->first_node("library");

		DriverHub* drv_hub = DriverHub::GetInstance();
		cstr8 runtime = xml::ValueCh8(xe_lib->first_node("runtime"));
		if(!drv_hub->Link(runtime))
		{
			delete runtime;
			delete data;
			return null;
		}
		Driver* driver = drv_hub->GetDriver(runtime);
		delete runtime;

		Library* lib = driver->CreateLibrary();
		lib->_Driver = driver;
		lib->_Id = xml::ValueCh8(xe_lib->first_node("id"));

		cstr8 path_dir = GetDirPath(path, true);
		ostringstream oss;
		oss<<path_dir<<lib->_Id<<"."<<driver->GetBinExt();
		lib->_Path = str::Copy(oss.str().c_str());
		delete path_dir;

		xml_node<>* xe_func = 
			(xml_node<>*)xml::Path(xe_lib, 2, "functions", "function");
		while(xe_func != null)
		{
			cstr8 s8_func = xml::Print(xe_func);
			Function* func = Function::Parse(s8_func);
			func->SetLibrary(lib);
			lib->_Functions.push_back(func);
			delete s8_func;
			xe_func = xe_func->next_sibling();
		}

		delete data;
		return lib;
	}

	void Library::ClearAll()
	{
		typedef vector<Function*>::iterator iter;
		for(iter i=_Functions.begin(); i!=_Functions.end(); i++)
			Clear(*i);
		_Functions.clear();
	}

	Driver* Library::GetDriver()
	{
		return _Driver;
	}

	cstr8 Library::GetLibraryId()
	{
		return _Id;
	}

	Function* Library::GetFunction(cstr8 id)
	{
		typedef vector<Function*>::iterator iter;
		for(iter i=_Functions.begin(); i!=_Functions.end(); i++)
		{
			Function* func = *i;
			if(strcmp(func->GetFunctionId(), id) == 0)
				return func;
		}
		return null;
	}

	Invoker* Library::CreateInvoker(cstr8 id)
	{
		Invoker* inv = _Driver->CreateInvoker();
		Function* func = GetFunction(id);
		inv->Initialize(func);
		return inv;
	}
}