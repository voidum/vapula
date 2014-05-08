#pragma once

#include "vf_base.h"

namespace vapula
{
	class Driver;
	class Aspect;
	class Library;
	class Stack;

	//runtime
	class VAPULA_API Runtime : Uncopiable
	{
	private:
		Runtime();
	public:
		~Runtime();

	private:
		static Runtime* _Instance;

	public:
		static Runtime* Instance();

	private:
		list<Driver*> _Drivers;
		list<Library*> _Libraries;
		list<Stack*> _Stacks;
		list<Aspect*> _Aspects;

	public:
		//start runtime
		void Start();

		//stop runtime
		void Stop();

	public:
		//reach frame
		void Reach(pcstr frame);

	public:
		//get count of drivers
		int CountDriver();

		//get driver by id
		Driver* GetDriver(pcstr id);

		//link driver
		void LinkDriver(Driver* driver);

		//kick driver
		void KickDriver(pcstr id);

		//kick all drivers
		void KickAllDrivers();

	public:
		//get count of libraries
		int CountLibrary();

		//get library by id
		Library* GetLibrary(pcstr id);

		//link library
		void LinkLibrary(Library* library);

		//kick library
		void KickLibrary(Library* library);

		//kick all libraries
		void KickAllLibraries();

	public:
		//get count of stacks
		int CountStack();

		//get stack by id
		Stack* GetStack(uint32 id);

		//link stack
		void LinkStack(Stack* stack);

		//kick stack
		void KickStack(Stack* stack);

		//kick all stacks
		void KickAllStacks();

	public:
		//get count of aspects
		int CountAspect();

		//get aspect by id
		Aspect* GetAspect(pcstr id);

		//link aspect
		void LinkAspect(Aspect* aspect);

		//kick aspect
		void KickAspect(Aspect* aspect);

		//kick all aspects
		void KickAllAspects();

	public:
		//get process name
		pcstr GetProcessName();

		//get process directory
		pcstr GetProcessDir();

		//get runtime directory
		pcstr GetRuntimeDir();

		//get vapula core version
		pcstr GetVersion();

	public:
		//create local unique id
		pcstr NewLUID();
	};
}