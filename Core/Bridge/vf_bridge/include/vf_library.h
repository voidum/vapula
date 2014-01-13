#pragma once

#include "vf_base.h"

namespace vapula
{
	class Envelope;
	class Invoker;
	class Driver;

	typedef Envelope* PtrEnvelope;

	//library {base}
	class VAPULA_API Library
	{
	protected:
		Library();
	public:
		static Library* Load(cstr8 path);
	public:
		virtual ~Library();

	protected:
		//driver
		Driver* _Driver;

		//library bin path
		cstr8 _Path;

		//library id
		cstr8 _Id; 

		//entry symbol
		cstr8 _EntrySym;

		//function total
		int _Total;

		//envelopes
		PtrEnvelope* _Envelopes;

	public:
		//get driver
		Driver* GetDriver();

		//get runtime id
		cstr8 GetRuntimeId();

		//get library id
		cstr8 GetLibraryId();

		//get entry symbol
		cstr8 GetEntrySym();

		//get envelope by id
		Envelope* GetEnvelope(int id);

		//create invoker
		Invoker* CreateInvoker(int id);

	public:
		//mount library
		virtual bool Mount() = 0;

		//unmount library
		virtual void Unmount() = 0;
	};
}