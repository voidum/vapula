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
		Lock* _Lock;
		list<Driver*> _Drivers;
		list<Library*> _Libraries;
		list<Stack*> _Stacks;
		list<Aspect*> _Aspects;

	private:
		pcstr IndexOfObject(uint8 type, raw target);

	public:
		int CountObjects(uint8 type);

		raw SelectObject(uint8 type, pcstr id);

		void LinkObject(uint8 type, raw target);
		
		void KickObject(uint8 type, pcstr id);

		void KickAllObjects(uint8 type);

	public:
		//activate runtime
		void Activate();

		//deactivate runtime
		void Deactivate();
		
		//reach frame
		void Reach(pcstr frame);

	public:
		//get process name
		pcstr GetProcessName();

		//get process directory
		pcstr GetProcessDir();

		//get runtime directory
		pcstr GetRuntimeDir();

		//get vapula core version
		pcstr GetVersion();

		//create local unique id
		pcstr NewLUID();
	};
}