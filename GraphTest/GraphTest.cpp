// GraphTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <windows.h>
#include <iostream>

#include "UserIntTest.hpp"
#include "UserStringTest.hpp"


int _tmain(int argc, _TCHAR* argv[])
{

	UserIntTest::Run();
	UserStringTest::Run();

	return 0;
}

