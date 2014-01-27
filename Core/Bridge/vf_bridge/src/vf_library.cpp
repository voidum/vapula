#include "vf_driver.h"
#include "vf_library.h"
#include "vf_function.h"
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

	Library* Library::Load(cstr8 path)
	{
		Scoped<XML> xml(XML::Load(path));
		if(xml.empty())
			return null;

		object xdoc = xml->GetEntity();
		object xe_lib = XML::XElem(xdoc, "library");

		DriverHub* drv_hub = DriverHub::GetInstance();
		astr8 s8_rt(XML::ValCh8(XML::XElem(xe_lib, "runtime")));
		if(!drv_hub->Link(s8_rt.get()))
			return null;
		Driver* driver = drv_hub->GetDriver(s8_rt.get());

		Library* lib = driver->CreateLibrary();
		lib->_Driver = driver;

		object xe_lib_id = XML::XElem(xe_lib, "id");
		lib->_Id = XML::ValCh8(xe_lib_id);

		astr8 path_dir(GetDirPath(path, true));

		ostringstream oss;
		oss<<path_dir.get()<<lib->_Id<<"."<<driver->GetBinExt();
		lib->_Path = str::Copy(oss.str().c_str());

		object xe_func = XML::XPath(xe_lib, 2, "functions", "function");
		while(xe_func != null)
		{
			astr8 s8_func(XML::Print(xe_func));
			Function* func = Function::Parse(s8_func.get());
			func->SetLibrary(lib);
			lib->_Functions.push_back(func);
			xe_func = XML::Next(xe_func);
		}
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