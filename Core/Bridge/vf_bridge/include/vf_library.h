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
	public:
		friend class LibraryHub;
	protected:
		Library();
	public:
		virtual ~Library();
	protected:
		//driver
		Driver* _Driver;

		//library directory
		cstr8 _Dir; 

		//library id
		cstr8 _Id; 

		//entry descriptor
		cstr8 _EntryDpt;

		//function descriptor
		cstr8 _FuncDpt;

	public:
		//get driver
		Driver* GetDriver();

		//get runtime id
		cstr8 GetRuntimeId();

		//get binary extension
		cstr8 GetBinExt();

		//get library id
		cstr8 GetLibraryId();

		//get entry descriptor
		cstr8 GetEntryDpt();

		//create envelope
		Envelope* CreateEnvelope(int fid);

		//create invoker
		Invoker* CreateInvoker(int fid);

	public:
		//mount library
		virtual bool Mount() = 0;

		//unmount library
		virtual void Unmount() = 0;
	};

	//hub for library
	class VAPULA_API LibraryHub
	{
	private:
		LibraryHub();
	public:
		~LibraryHub();
	private:
		static LibraryHub* _Instance;
	public:
		static LibraryHub* GetInstance();
	private:
		int _Count;
		vector<Library*> _Libraries;
	public:
		//get count of loaded libraries
		int GetCount();

		//get library by id
		Library* GetLibrary(cstr8 id);

		//load library by path
		Library* Load(cstr8 path);

		//unload library by id
		void Unload(cstr8 id);

		//unload all libraries
		void UnloadAll();
	};
}