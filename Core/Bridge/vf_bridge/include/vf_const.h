#pragma once

#include <list>
#include <queue>
#include <sstream>
#include <iostream>
#include <ctime>
#include <random>
#include <exception>

#include "windows.h"

#ifdef VAPULA_EXPORTS
#define VAPULA_API __declspec(dllexport)
#else
#define VAPULA_API
#endif

namespace vapula
{
	//using namespace
	using std::list;
	using std::queue;
	using std::string;
	using std::wstring;
	using std::ostringstream;

	//define null
	#define null 0

	//define type alias
	typedef void* raw;
	typedef unsigned char byte;

	typedef signed char int8;
	typedef short int16;
	typedef int int32;
	typedef long long int64;
	typedef unsigned char uint8;
	typedef unsigned short uint16;
	typedef unsigned int uint32;
	typedef unsigned long long uint64;
	typedef float real32;
	typedef double real64;

	typedef char* pstr;
	typedef wchar_t* pwstr;
	typedef const char* pcstr;
	typedef const wchar_t* pcwstr;

	//define enum
	enum AccessMode
	{
		VF_ACCESS_IN = 0,
		VF_ACCESS_OUT = 1,
		VF_ACCESS_INOUT = 2
	}; //access mode

	enum State
	{
		VF_STATE_IDLE = 0,
		VF_STATE_QUEUE = 1,
		VF_STATE_BUSY_BACK = 2,
		VF_STATE_BUSY_FRONT = 3,
		VF_STATE_ROLLBACK = 4,
		VF_STATE_PAUSE = 5
	}; //context state

	enum ControlCode
	{
		VF_CTRL_NULL = 0,
		VF_CTRL_PAUSE = 1,
		VF_CTRL_RESUME = 2,
		VF_CTRL_CANCEL = 3
	}; //context control code

	enum ReturnCode
	{
		VF_RETURN_ERROR = 0,
		VF_RETURN_NORMAL = 1,
		VF_RETURN_CANCEL = 2,
		VF_RETURN_TERMINATE = 3,
		VF_RETURN_NULLTASK = 4,
		VF_RETURN_UNHANDLED = 5
	}; //context return code

	//define error
	pcstr const _vf_error_1 = "invalid access";
	pcstr const _vf_error_2 = "invalid invoke";

	pcstr const _vf_bridge = "Vapula Bridge";
	pcstr const _vf_version = "2.1.4.1";
	pcstr const _vf_fatal = "!!! FATAL ERROR !!!";

	uint32 const _vf_cp_oem = CP_OEMCP;
	uint32 const _vf_cp_msg = CP_UTF8;

	uint32 const _vf_size_path = 1024;
}