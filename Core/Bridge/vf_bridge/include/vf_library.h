#pragma once

#include "vf_base.h"

namespace vapula
{
	class Envelope;
	class Invoker;
	class Driver;

	//library {base}
	class VAPULA_API Library
	{
	protected:
		Library();
	public:
		virtual ~Library();
	protected:
		//library directory
		cstr8 _Dir; 

		//library id
		cstr8 _LibId; 

		//entry descriptor
		cstr8 _EntryDpt; 

		//function descriptor
		cstr8 _FuncDpt; 
	protected:
		static int _Count;
	public:
		//load library by path
		static Library* Load(cstr8 path);
		
		//get count of mounted libraries
		static int GetCount();
	public:
		//get library id
		cstr8 GetLibraryId();

		//get entry descriptor
		cstr8 GetEntryDpt();

		//create envelope
		Envelope* CreateEnvelope(int fid);

		//create invoker
		Invoker* CreateInvoker(int fid);
	public:
		//get runtime id
		virtual cstr8 GetRuntimeId() = 0;

		//get library file extension name
		//begin with "."
		virtual cstr8 GetBinExt() = 0;

		//mount library
		virtual bool Mount();

		//unmount library
		virtual void Unmount();
	};
}