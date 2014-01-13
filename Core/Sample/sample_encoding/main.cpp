#include "vf_base.h"
#include "vf_setting.h"
#include "vf_token.h"

using namespace vapula;
using std::cout;
using std::endl;
using std::wcout;

int main()
{
	Token* token = new Token();
	uint8 key = token->Lock();
	cout<<(token->IsLock() ? "lock" : "unlock")<<endl;
	token->Unlock(key);
	cout<<(token->IsLock() ? "lock" : "unlock")<<endl;

	LARGE_INTEGER freq, t1, t2;
	QueryPerformanceFrequency(&freq);
	QueryPerformanceCounter(&t1);
	for (int i=0; i<100000; i++)
	{
		uint8 key = token->Lock();
		token->Unlock(key);
	}
	QueryPerformanceCounter(&t2);
	cout<<(t2.QuadPart - t1.QuadPart) * 1000.0f / freq.QuadPart<<endl;

	system("pause");
	return 0;
}