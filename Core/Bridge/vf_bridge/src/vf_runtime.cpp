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
		_Lock = new Lock();
	}

	Runtime::~Runtime()
	{
		Deactivate();
		Clear(_Lock);
	}

	pcstr Runtime::IndexOfObject(Core* target)
	{
		return target->GetCoreId();
	}

	list<Core*>* Runtime::ListObjects(uint8 type)
	{
		switch (type)
		{
		case VF_CORE_DRIVER:
			return (list<Core*>*)(&_Drivers);
		case VF_CORE_LIBRARY:
			return (list<Core*>*)(&_Libraries);
		case VF_CORE_ASPECT:
			return (list<Core*>*)(&_Aspects);
		default:
			return null;
		}
	}

	int Runtime::CountObjects(uint8 type)
	{
		_Lock->Enter();
		list<Core*>* cores = ListObjects(type);
		int count = cores->size();
		_Lock->Leave();
		return count;
	}

	Core* Runtime::SelectObject(uint8 type, pcstr id)
	{
		_Lock->Enter();
		Core* object = null;
		if (id != null)
		{
			typedef list<Core*>::iterator iter;
			list<Core*>* cores = ListObjects(type);
			for (iter i = cores->begin(); i != cores->end(); i++)
			{
				pcstr cur_id = IndexOfObject(*i);
				if (cur_id == null)
					continue;
				if (strcmp(id, cur_id) == 0)
				{
					object = *i;
					break;
				}
			}
		}
		_Lock->Leave();
		return object;
	}

	void Runtime::LinkObject(Core* target)
	{
		pcstr id = IndexOfObject(target);
		raw object = SelectObject(target->GetType(), id);
		if (object != null)
			return;
		_Lock->Enter();
		list<Core*>* cores = ListObjects(target->GetType());
		cores->push_back(target);
		_Lock->Leave();
	}

	void Runtime::KickObject(uint8 type, pcstr id)
	{
		Core* object = SelectObject(type, id);
		if (object == null)
			return;
		_Lock->Enter();
		list<Core*>* cores = ListObjects(type);
		cores->remove(object);
		_Lock->Leave();
	}

	void Runtime::KickAllObjects(uint8 type)
	{
		_Lock->Enter();
		typedef list<Core*>::iterator iter;
		list<Core*>* cores = (list<Core*>*)ListObjects(type);
		for (iter i = cores->begin(); i != cores->end(); i++)
			Clear(*i);
		cores->clear();
		_Lock->Leave();
	}

	void Runtime::Activate()
	{
		Worker* worker = Worker::Instance();
		worker->Online();
	}

	void Runtime::Deactivate()
	{
		Worker* worker = Worker::Instance();
		worker->Offline();
		KickAllObjects(VF_CORE_DRIVER);
		KickAllObjects(VF_CORE_LIBRARY);
		KickAllObjects(VF_CORE_ASPECT);
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
			Aspect* aspect = (Aspect*)(*i);
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
		pwstr s16_path = new wchar_t[_vf_size_path];
		GetModuleFileName(hmod, s16_path, _vf_size_path);
		pcstr cs8_path = str::ToStr(s16_path);
		delete s16_path;
		Scoped autop((raw)cs8_path);
		return GetPathDir(cs8_path, true);
	}

	pcstr Runtime::GetProcessDir()
	{
		pwstr s16_path = new wchar_t[_vf_size_path];
		GetModuleFileName(null, s16_path, _vf_size_path);
		pcstr cs8_path = str::ToStr(s16_path);
		delete s16_path;
		Scoped autop1((raw)cs8_path);
		return GetPathDir(cs8_path, true);
	}

	pcstr Runtime::GetProcessName()
	{
		pwstr s16_path = new wchar_t[_vf_size_path];
		GetModuleFileName(null, s16_path, _vf_size_path);
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