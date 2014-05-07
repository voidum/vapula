#include "vf_error.h"
#include "vf_stack.h"

namespace vapula
{
	Error::Error(int what)
	{
		_What = what;
	}

	Error::~Error() { }

	int Error::What()
	{
		return _What;
	}

	void Error::Throw(int what)
	{
		Error* err = new Error(what);
		Stack* stack = Stack::Instance();
		if(stack != null)
			stack->SetError(err);
		throw err;
	}
}