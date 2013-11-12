#pragma once

#include <sstream>
#include <vector>
#include <iostream>
#include <ctime>
#include <random>
#include <stdexcept>

#include "windows.h" //#IFDEF

namespace tcm
{
	//using namespace
	using std::vector;
	using std::string;
	using std::wstring;
	using std::invalid_argument;

	//define type
	typedef void* object;
	typedef unsigned char byte;
	typedef char int8;
	typedef short int16;
	typedef int int32;
	typedef long long int64;
	typedef unsigned char uint8;
	typedef unsigned short uint16;
	typedef unsigned int uint32;
	typedef unsigned long long uint64;
	typedef float real32;
	typedef double real64;
	typedef const char* str;
	typedef const wchar_t* strw;

	#define null 0

	//define enum
	enum DataType
	{
		TCM_DATA_POINTER = 0,
		TCM_DATA_INT8 = 1,
		TCM_DATA_INT16 = 2,
		TCM_DATA_INT32 = 3,
		TCM_DATA_INT64 = 4,
		TCM_DATA_UINT8 = 5,
		TCM_DATA_UINT16 = 6,
		TCM_DATA_UINT32 = 7,
		TCM_DATA_UINT64 = 8,
		TCM_DATA_REAL32 = 10,
		TCM_DATA_REAL64 = 11,
		TCM_DATA_BOOL = 20,
		TCM_DATA_STRING = 21
	}; //envelope data type

	enum State
	{
		TCM_STATE_IDLE = 0,
		TCM_STATE_BUSY = 1,
		TCM_STATE_PAUSE = 2,
		TCM_STATE_UI = 3
	}; //context state

	enum CtrlCode
	{
		TCM_CTRL_NULL = 0,
		TCM_CTRL_PAUSE = 1,
		TCM_CTRL_RESUME = 2,
		TCM_CTRL_CANCEL = 3,
		TCM_CTRL_RESTART = 4
	}; //context control code

	enum ReturnCode
	{
		TCM_RETURN_ERROR = 0,
		TCM_RETURN_NORMAL = 1,
		TCM_RETURN_CANCELBYMSG = 2,
		TCM_RETURN_CANCELBYFORCE = 3,
		TCM_RETURN_NULLENTRY = 4,
		TCM_RETURN_NULLTASK = 5,
	}; //context return code

	//define error
	const str _tcm_err_0 = "invalid data type";
	const str _tcm_err_1 = "access null param";
	const str _tcm_err_2 = "write null value";
	const str _tcm_err_3 = "deliver between different types";
	const str _tcm_err_4 = "read string as multi-bytes";
}