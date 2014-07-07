#pragma once

#include "vf_base.h"

namespace vapula
{
	class Driver;
	class Library;
	class Aspect;

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

	public:
		//start
		void Start();

		//stop
		void Stop();

	public:
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