#include "vf_base.h"
#include "vf_setting.h"
#include "vf_token.h"

using namespace vapula;
using std::cout;
using std::endl;
using std::wcout;

class A{};

int main()
{
	int* data = new int[1000];
	Clear(data, true);
	
	A* a = new A();
	Clear(a);

	system("pause");
	return 0;
}