#include "stdafx.h"

#include "gtest.h"
#include "../Common/CompileTimeAssert.hpp"

TEST(StaticAssert_Test1, JustRun)
{
	int array_1 [15];
	int array_2 [15]; // change to != 15 to get error

	CompileTimeAssert< (_countof(array_1) == _countof(array_2)) > Arrays_are_different_in_size;
}
