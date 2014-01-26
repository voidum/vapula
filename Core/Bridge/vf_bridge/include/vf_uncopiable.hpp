#pragma once

namespace vapula
{
	//restrict copy
	class Uncopiable
	{
	protected:
		Uncopiable() { }
		~Uncopiable() { }
	private:
		Uncopiable(const Uncopiable&);
		Uncopiable& operator=(const Uncopiable&);
	};
}