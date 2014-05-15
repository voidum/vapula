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
	}

	raw Runtime::List(int8 type)
	{
		switch (type)
		{
		case VF_CORE_DRIVER:
			return &_Drivers;
		case VF_CORE_LIBRARY:
			return &_Libraries;
		case VF_CORE_STACK:
			return &_Stacks;
		case VF_CORE_ASPECT:
			return &_Aspects;
		default:
			throw std::exception(_vf_err_2);
		}
	}

	pcstr Runtime::IndexOf(raw target, int8 type)
	{
		switch (type)
		{
		case VF_CORE_DRIVER:
			return ((Driver*)target)->GetRuntimeId();
		case VF_CORE_LIBRARY:
			return ((Library*)target)->GetLibraryId();
		case VF_CORE_STACK:
			return ((Stack*)target)->GetStackId();
		case VF_CORE_ASPECT:
			return ((Aspect*)target)->GetAspectId();
		default:
			throw std::exception(_vf_err_2);
		}
	}

	void Runtime::Activate()
	{
		Worker::Instance()->Online();
	}

	void Runtime::Deactivate()
	{
		Worker::Instance()->Offline();
		KickAll<Driver>();
		KickAll<Library>();
		KickAll<Stack>();
		KickAll<Aspect>();
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