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

	Library* Library::Load(pcstr path)
	{
		XML* xml = XML::Load(path);
		Handle autop_xml(xml);
		if(xml == null)
			return null;

		object xdoc = xml->GetEntity();
		object xe_lib = XML::XElem(xdoc, "library");

		DriverHub* drv_hub = DriverHub::GetInstance();
		pcstr cs8_rt = XML::ValStr(XML::XElem(xe_lib, "runtime"));
		Handle autop1((object)cs8_rt);
		if(!drv_hub->Link(cs8_rt))
			return null;
		Driver* driver = drv_hub->GetDriver(cs8_rt);

		Library* lib = driver->CreateLibrary();
		lib->_Driver = driver;

		object xe_lib_id = XML::XElem(xe_lib, "id");
		lib->_Id = XML::ValStr(xe_lib_id);

		pcstr path_dir = GetDirPath(path, true);
		Handle autop2((object)path_dir);
		ostringstream oss;
		oss<<path_dir<<lib->_Id<<"."<<driver->GetBinExt();
		lib->_Path = str::Copy(oss.str().c_str());

		object xe_func = XML::XPath(xe_lib, 2, "functions", "function");
		while(xe_func != null)
		{
			pcstr cs8_func = XML::Print(xe_func);
			Function* func = Function::Parse(cs8_func);
			func->SetLibrary(lib);
			lib->_Functions.push_back(func);
			xe_func = XML::Next(xe_func);
			delete cs8_func;
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

	pcstr Library::GetLibraryId()
	{
		return _Id;
	}

	Function* Library::GetFunction(pcstr id)
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

	Invoker* Library::CreateInvoker(pcstr id)
	{
		Invoker* inv = _Driver->CreateInvoker();
		Function* func = GetFunction(id);
		inv->Initialize(func);
		return inv;
	}
}