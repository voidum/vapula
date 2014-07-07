#pragma once

#include "vf_base.h"

namespace vapula
{
	class Library;

	class LibraryHub : Uncopiable
	{
	public:
		LibraryHub();
		~LibraryHub();

	private:
		Lock* _Lock;
		list<Library*> _Libraries;

	public:
		list<Library*>& GetInnerData();

	public:
		int Count();

		Library* Find(pcstr id);

		void Link(Library* library);

		void Kick(pcstr id);

		void KickAll();
	};
}