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
	public:
		friend class LibraryHub;
	protected:
		Library();
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
		//get instance of LibraryHub
		static LibraryHub* GetInstance();
	private:
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