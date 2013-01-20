
#include "stdafx.h"

#include <string>

#include "gtest.h"

#include "../Common/NumStrHelpers.hpp"

// NumStrHelpers::TryStringToInt

TEST(NumStrHelpers_TryStringToInt, Returns_False_ForEmptryString)
{
	int result = 123;
	bool success = NumStrHelpers::TryStringToInt(L"", &result);
	ASSERT_FALSE(success);
	EXPECT_EQ(result, 123);
}

TEST(NumStrHelpers_TryStringToInt, Returns_False_ForNotInt)
{
	int result = 321;
	bool success = NumStrHelpers::TryStringToInt(L"8,88", &result);
	ASSERT_FALSE(success);
	EXPECT_EQ(result, 321);
}

TEST(NumStrHelpers_TryStringToInt, Returns_True_ForInt)
{
	int result = 0;
	bool success = NumStrHelpers::TryStringToInt(L"888", &result);
	ASSERT_TRUE(success);
	EXPECT_EQ(result, 888);
}

// NumStrHelpers::IsNumber

TEST(NumStrHelpers_IsNumber, Returns_False_ForEmptyString)
{
	ASSERT_FALSE(NumStrHelpers::IsNumber(L""));
}

TEST(NumStrHelpers_IsNumber, Returns_False_DueToCharachter)
{
	ASSERT_FALSE(NumStrHelpers::IsNumber(L"123a456"));
}

TEST(NumStrHelpers_IsNumber, Returns_False_DueToDot)
{
	ASSERT_FALSE(NumStrHelpers::IsNumber(L".123456"));
}

TEST(NumStrHelpers_IsNumber, Returns_False_DueToComma)
{
	ASSERT_FALSE(NumStrHelpers::IsNumber(L"-123456,"));
}

TEST(NumStrHelpers_IsNumber, Returns_False_DueToOverflow)
{
	ASSERT_FALSE(NumStrHelpers::IsNumber(L"123456789012"));
}

TEST(NumStrHelpers_IsNumber, Returns_True_ForPositiveNum)
{
	ASSERT_TRUE(NumStrHelpers::IsNumber(L"123456"));
}

TEST(NumStrHelpers_IsNumber, Returns_True_ForNegativeNum)
{
	ASSERT_TRUE(NumStrHelpers::IsNumber(L"-123456"));
}

// NumStrHelpers::IsOdd

TEST(NumStrHelpers_IsOdd, Returns_False_ForEvenNumber)
{
	ASSERT_FALSE(NumStrHelpers::IsOdd(8));
}

TEST(NumStrHelpers_IsOdd, Returns_True_ForOddNumber)
{
	ASSERT_TRUE(NumStrHelpers::IsOdd(7));
}


// NumStrHelpers::ToString(int)

TEST(NumStrHelpers_ToString_Int, DefaultFormating)
{
	EXPECT_EQ(NumStrHelpers::ToString<char>(-12345678), "-12345678");
}

TEST(NumStrHelpers_ToString_Int, HexFormating)
{
	EXPECT_EQ(NumStrHelpers::ToString<char>(12345678, "%x"), "bc614e");
}

// NumStrHelpers::ToString(wchar_t)

TEST(NumStrHelpers_ToString_Wchar, StrShorter128)
{
	std::wstring wideTestString = L"abcdef123456";
	EXPECT_EQ(NumStrHelpers::ToString(wideTestString.c_str(), wideTestString.length()), "abcdef123456");
}

#define STRING_LONGER_128 "0000000000111111111122222222223333333333444444444455555555556666666666777777777788888888889999999999\
								000000000011111111112222222222aaaaaaaaaabbbbbbbbbbccccccccccdddddddddd"

TEST(NumStrHelpers_ToString_Wchar, StrLonger128)
{
	std::wstring wideTestString = _T(STRING_LONGER_128);
	std::string expectedString = STRING_LONGER_128;

	EXPECT_EQ(NumStrHelpers::ToString(wideTestString.c_str(), wideTestString.length()), expectedString);
}
