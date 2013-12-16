#include "vf_base.h"
#include "vf_config.h"
#include "unicode/ucnv.h"

using namespace vapula;
using std::cout;
using std::endl;
using std::wcout;

int main()
{
	cout<<Config::GetInstance()->Test(12)<<endl;

	system("pause");
	return 0;
}