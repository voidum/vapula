#include "vf_runtime.h"
#include "vf_stack.h"
#include "vf_context.h"
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

	void Runtime::Start()
	{
		Worker* worker = Worker::Instance();
		worker->Online();
	}

	void Runtime::Stop()
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
		weaver->OnReachFrame(frame);
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