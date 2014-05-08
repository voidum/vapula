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
		static Lock* _CtorLock;
		static Runtime* _Instance;

	public:
		static Runtime* Instance();

	private:
		list<Driver*> _Drivers;
		list<Aspect*> _Aspects;
		list<Library*> _Libraries;
		list<Stack*> _Stacks;

	public:
		//start runtime
		void Start();

		//stop runtime
		void Stop();

	public:
		//reach frame
		void Reach(pcstr frame);

	public:
		Driver* GetDriver(pcstr id);
		Aspect* GetAspect(pcstr id);
		Library* GetLibrary(pcstr id);
		Stack* GetStack(uint32 id);

		void Link(Driver* driver);
		void Link(Aspect* aspect);
		void Link(Library* library);
		void Link(Stack* stack);

		void Kick(Driver* driver);
		void Kick(Aspect* aspect);
		void Kick(Library* library);
		void Kick(Stack* stack);

	public:
		//get process name
		pcstr GetProcessName();

		//get process directory
		pcstr GetProcessDir();

		//get runtime directory
		pcstr GetRuntimeDir();

		//get Vapula core version
		pcstr GetVersion();

	public:
		//create local unique id
		pcstr NewLUID();
	};
}