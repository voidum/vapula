#include "vf_runtime.h"
#include "vf_driver.h"
#include "vf_library.h"
#include "vf_stack.h"
#include "vf_context.h"
#include "vf_aspect.h"
#include "vf_worker.h"
#include "vf_weaver.h"

namespace vapula
{
	Runtime* Runtime::_Instance = null;

	Runtime* Runtime::Instance()
	{
		if (Runtime::_Instance == null)
		{
			Lock* lock = Lock::GetCtorLock();
			lock->Enter();
			if (Runtime::_Instance == null)
				Runtime::_Instance = new Runtime();
			lock->Leave();
		}
		return Runtime::_Instance;
	}

	Runtime::Runtime()
	{
	}

	Runtime::~Runtime()
	{
		Worker* worker = Worker::Instance();
		worker->Offline();
	}

	int Runtime::CountObjects(uint8 type)
	{
		switch (type)
		{
		case VF_CORE_DRIVER:
			return _Drivers.size();
		case VF_CORE_LIBRARY:
			return _Libraries.size();
		case VF_CORE_STACK:
			return _Stacks.size();
		case VF_CORE_ASPECT:
			return _Aspects.size();
		default:
			return 0;
		}
	}

	raw Runtime::SelectObject(uint8 type, pcstr id)
	{
		switch (type)
		{
		case VF_CORE_DRIVER:
			typedef list<Driver*>::iterator iter1;
			for (iter1 i = _Drivers.begin(); i != _Drivers.end(); i++)
			{
				Driver* entity = *i;
				if (strcmp(id, entity->GetRuntimeId()) == 0)
					return entity;
			}
			return null;
		case VF_CORE_LIBRARY:
			typedef list<Library*>::iterator iter2;
			for (iter2 i = _Libraries.begin(); i != _Libraries.end(); i++)
			{
				Library* entity = *i;
				if (strcmp(id, entity->GetLibraryId()) == 0)
					return entity;
			}
			return null;
		case VF_CORE_STACK:
			typedef list<Stack*>::iterator iter3;
			for (iter3 i = _Stacks.begin(); i != _Stacks.end(); i++)
			{
				Stack* entity = *i;
				if (strcmp(id, entity->GetStackId()) == 0)
					return entity;
			}
			return null;
		case VF_CORE_ASPECT:
			typedef list<Aspect*>::iterator iter4;
			for (iter4 i = _Aspects.begin(); i != _Aspects.end(); i++)
			{
				Aspect* entity = *i;
				if (strcmp(id, entity->GetAspectId()) == 0)
					return entity;
			}
			return null;
		default:
			return null;
		}
	}

	void Runtime::LinkObject(uint8 type, raw target)
	{
		pcstr id = IndexOfObject(type, target);
		raw object = SelectObject(type, id);
		if (object != null)
			return;
		switch (type)
		{
		case VF_CORE_DRIVER:
			_Drivers.push_back((Driver*)target);
			break;
		case VF_CORE_LIBRARY:
			_Libraries.push_back((Library*)target);
			break;
		case VF_CORE_STACK:
			_Stacks.push_back((Stack*)target);
			break;
		case VF_CORE_ASPECT:
			_Aspects.push_back((Aspect*)target);
			break;
		default:
			break;
		}
	}

	void Runtime::KickObject(uint8 type, pcstr id)
	{
		raw object = SelectObject(type, id);
		if (object == null)
			return;
		switch (type)
		{
		case VF_CORE_DRIVER:
			_Drivers.remove((Driver*)object);
			break;
		case VF_CORE_LIBRARY:
			_Libraries.remove((Library*)object);
			break;
		case VF_CORE_STACK:
			_Stacks.remove((Stack*)object);
			break;
		case VF_CORE_ASPECT:
			_Aspects.remove((Aspect*)object);
			break;
		default:
			break;
		}
		Clear(object);
	}

	void Runtime::KickAllObjects(uint8 type)
	{
		switch (type)
		{
		case VF_CORE_DRIVER:
			typedef list<Driver*>::iterator iter1;
			for (iter1 i = _Drivers.begin(); i != _Drivers.end(); i++)
				Clear(*i);
			_Drivers.clear();
			break;
		case VF_CORE_LIBRARY:
			typedef list<Library*>::iterator iter2;
			for (iter2 i = _Libraries.begin(); i != _Libraries.end(); i++)
				Clear(*i);
			_Libraries.clear();
			break;
		case VF_CORE_STACK:
			typedef list<Stack*>::iterator iter3;
			for (iter3 i = _Stacks.begin(); i != _Stacks.end(); i++)
				Clear(*i);
			_Stacks.clear();
			break;
		case VF_CORE_ASPECT:
			typedef list<Aspect*>::iterator iter4;
			for (iter4 i = _Aspects.begin(); i != _Aspects.end(); i++)
				Clear(*i);
			_Aspects.clear();
			break;
		default:
			break;
		}
	}

	void Runtime::Activate()
	{
		Worker* worker = Worker::Instance();
		worker->Offline();
	}

	void Runtime::Deactivate()
	{
		Worker* worker = Worker::Instance();
		worker->Offline();
	}

	void Runtime::Reach(pcstr frame)
	{
		Stack* stack = Stack::Instance();
		Context* context = stack->GetContext();
		context->SetKeyFrame(frame);

		Weaver* weaver = Weaver::Instance();

		typedef list<Aspect*>::iterator iter;
		list<Aspect*> joins;
		for (iter i = _Aspects.begin(); i != _Aspects.end(); i++)
		{
			Aspect* aspect = *i;
			if (aspect->TryMatch(frame))
			{
				weaver->Invoke(aspect);
				if (!aspect->IsAsync())
					joins.push_back(aspect);
			}
		}
		for (iter i = joins.begin(); i != joins.end(); i++)
			weaver->Join(*i);
	}

	pcstr Runtime::GetRuntimeDir()
	{
		HMODULE hmod = GetModuleHandle(L"vf_bridge");
		pwstr s16_path = new wchar_t[_vf_path_len];
		GetModuleFileName(hmod, s16_path, _vf_path_len);
		pcstr cs8_path = str::ToStr(s16_path);
		delete s16_path;
		Scoped autop((raw)cs8_path);
		return GetPathDir(cs8_path, true);
	}

	pcstr Runtime::GetProcessDir()
	{
		pwstr s16_path = new wchar_t[_vf_path_len];
		GetModuleFileName(null, s16_path, _vf_path_len);
		pcstr cs8_path = str::ToStr(s16_path);
		delete s16_path;
		Scoped autop1((raw)cs8_path);
		return GetPathDir(cs8_path, true);
	}

	pcstr Runtime::GetProcessName()
	{
		pwstr s16_path = new wchar_t[_vf_path_len];
		GetModuleFileName(null, s16_path, _vf_path_len);
		pcstr cs8_path = str::ToStr(s16_path);
		delete s16_path;
		Scoped autop1((raw)cs8_path);
		string str_full = cs8_path;
		string str_fix = str_full.substr(str_full.rfind(L'\\') + 1);
		pcstr ret = str::Copy(str_fix.c_str());
		return ret;
	}

	pcstr Runtime::GetVersion()
	{
		return _vf_version;
	}

	pcstr Runtime::NewLUID()
	{
		std::ostringstream oss;
		oss.imbue(std::locale("C"));
		const time_t t = time(null);
		oss << "VAPULA_";
		oss << t << "_";
		srand((uint32)time(null));
		for (uint8 i = 0; i < 16; i++)
		{
			int rnd = rand() % 10;
			oss << rnd;
		}
		return str::Copy(oss.str().c_str());
	}
}