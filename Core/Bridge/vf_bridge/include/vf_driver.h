#pragma once

#include "vf_base.h"

namespace vapula
{
	class Library;
	class Task;

	//driver {base}
	class VAPULA_API Driver : public Core
	{
	private:
		HMODULE _Module;

	public:
		Driver();
		virtual ~Driver();

	public:
		//load driver by path
		static Driver* Load(pcstr path);

	public:
		//get runtime id
		virtual pcstr
			GetRuntimeId() = 0;

		//get bin extension
		//e.g. "dll"
		virtual pcstr
			GetBinExt() = 0;

		//create library
		virtual Library*
			CreateLibrary() = 0;

		//create invoker
		virtual Task*
			CreateTask() = 0;

	public:
		uint8 GetType();

		pcstr GetCoreId();
	};
}