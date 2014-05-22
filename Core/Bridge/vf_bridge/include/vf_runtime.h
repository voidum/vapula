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

	private:
		raw List(int8 type);

		pcstr IndexOf(raw target, int8 type);

		template<typename T>
		list<T*> List()
		{
			Traits<T>* traits = new Traits<T>();
			Scoped autop(traits);
			return *((list<T*>*)List(traits->TypeId));
		}

		template<typename T>
		pcstr IndexOf(T* target)
		{
			Traits<T>* traits = new Traits<T>();
			Scoped autop(traits);
			return IndexOf(target, traits->TypeId);
		}

	public:
		template<typename T>
		int Count() 
		{
			list<T*> list = List<T>();
			return list->size();
		}

		template<typename T>
		T* Select(pcstr id) 
		{
			typedef list<T*>::iterator iter;
			list<T*> list = List<T>();
			for (iter i = list.begin(); i != list.end(); i++)
			{
				T* entity = *i;
				if (strcmp(id, IndexOf(entity)) == 0)
					return entity;
			}
			return null;
		}

		template<typename T>
		void Link(T* target) 
		{
			T* entity = Select<T>(IndexOf(target));
			if (entity == null)
			{
				list<T*> list = List<T>();
				list.push_back(target);
			}
		}
		
		template<typename T>
		void Kick(pcstr id)
		{
			typedef list<T*>::iterator iter;
			list<T*> list = List<T>();
			for (iter i = list.begin(); i != list.end(); i++)
			{
				T* entity = *i;
				if (strcmp(id, IndexOf(entity)) == 0)
				{
					list.erase(i);
					Clear(entity);
					break;
				}
			}
		}

		template<typename T>
		void KickAll()
		{
			typedef list<T*>::iterator iter;
			list<T*> list = List<T>();
			for (iter i = list.begin(); i != list.end(); i++)
				Clear(*i);
			list.clear();
		}

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