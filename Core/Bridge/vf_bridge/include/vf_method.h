#pragma once

#include "vf_base.h"

namespace vapula
{
	class Envelope;
	class Library;

	//method
	class VAPULA_API Method
	{
	protected:
		Method();
	public:
		static Method* Parse(pcstr xml);
	public:
		virtual ~Method();

	protected:
		Library* _Library;

		//method id
		pcstr _Id;

		//protected flag
		bool _IsProtected;

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

		//get method id
		pcstr GetMethodId();

		//get protected flag
		bool IsProtected();

		//get envelope
		Envelope* GetEnvelope();

		//get process symbol
		pcstr GetProcessSym();

		//get rollback symbol
		pcstr GetRollbackSym();
	};
}