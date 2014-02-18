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

		object xe_mt = XML::XPath(xe_lib, 2, "methods", "method");
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
}