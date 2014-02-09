#pragma once

#include "vf_base.h"

namespace vapula
{
	class Envelope;
	class Library;

	//function
	class VAPULA_API Function
	{
	protected:
		Function();
	public:
		static Function* Parse(pcstr xml);
	public:
		virtual ~Function();

	protected:
		Library* _Library;

		//function id
		pcstr _Id;

		//entry symbol
		pcstr _RollbackSym;

		//process symbol
		pcstr _ProcessSym;

		//prototype envelope
		Envelope* _Envelope;

	public:
		//get library
		Library* GetLibrary();

		//set library
		void SetLibrary(Library* lib);

		//get function id
		pcstr GetFunctionId();

		//get envelope
		Envelope* GetEnvelope();

		//get process symbol
		pcstr GetProcessSym();

		//get rollback symbol
		pcstr GetRollbackSym();
	};
}