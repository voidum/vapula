#pragma once

#include "vf_base.h"

namespace vapula
{
	class Envelope;
	class Invoker;
	class Library;

	//function
	class VAPULA_API Function
	{
	protected:
		Function();
	public:
		static Function* Parse(cstr8 xml);
	public:
		virtual ~Function();

	protected:
		Library* _Library;

		//function id
		cstr8 _Id;

		//entry symbol
		cstr8 _EntrySym;

		//prototype envelope
		Envelope* _Envelope;

	public:
		//get library
		Library* GetLibrary();

		//set library
		void SetLibrary(Library* lib);

		//get function id
		cstr8 GetFunctionId();

		//get entry symbol
		cstr8 GetEntrySym();

		//get envelope
		Envelope* GetEnvelope();
	};
}