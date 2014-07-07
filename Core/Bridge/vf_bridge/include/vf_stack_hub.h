#pragma once

#include "vf_base.h"

namespace vapula
{
	class Stack;

	class StackHub : Uncopiable
	{
	public:
		StackHub();
		~StackHub();

	private:
		Lock* _Lock;
		list<Stack*> _Stacks;

	public:
		list<Stack*>& GetInnerData();

	public:
		int Count();

		Stack* Find(int id);

		void Link(Stack* stack);

		void Kick(Stack* stack);

		void KickAll();
	};
}