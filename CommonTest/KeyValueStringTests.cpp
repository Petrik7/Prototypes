
#include "stdafx.h"

#include <set>
#include <string>

#include "gtest.h"

#include "WTypes.h"
#include "../Common/KeyValueString.hpp"
#include "../Common/NumStrHelpers.hpp"

using std::set;
using std::string;
using std::wstring;

class StrHelper
{
public:

	static inline string ToAppropriateStr(wstring str, char typeOfCharInReturnString)
	{
		return NumStrHelpers::ToString(str);
	}

	static inline wstring ToAppropriateStr(wstring str, wchar_t typeOfCharInReturnString)
	{
		return str;
	}

	static inline string ToAppropriateStr(string str, char typeOfCharInReturnString)
	{
		return str;
	}

	static inline wstring ToAppropriateStr(string str, wchar_t typeOfCharInReturnString)
	{
		const size_t MAX_OPTIONS_LENGTH = 8192;
		wchar_t wideString[MAX_OPTIONS_LENGTH] = {0};
		_snwprintf_s(wideString, MAX_OPTIONS_LENGTH, _TRUNCATE, L"%hs", str.c_str());
		return wstring(wideString);
	}

};

class TestScenario 
{
public:

	void PrepareTestData(wchar_t keyValueSeparator, wchar_t pairSeparator)
	{
		wstring wKeyValueSeparator;
		wKeyValueSeparator += keyValueSeparator;

		wstring wPS1;
		wPS1 += pairSeparator;
		wstring wPS2;
		wPS2 += pairSeparator;
		wPS2 += pairSeparator;
		wstring wPS3;
		wPS3 += pairSeparator;
		wPS3 += pairSeparator;
		wPS3 += pairSeparator;
		wstring wPS4;
		wPS4 += pairSeparator;
		wPS4 += pairSeparator;
		wPS4 += pairSeparator;
		wPS4 += pairSeparator;

		_wDeviceIdKey = L"DeviceId";
		_wCameraNameKey = L"CameraName";
		_wCameraNameValue = L"camera name";
		_wManufactureKey = L"Manufacture";
		_wManufactureValue = L"manufacture name";
		_wOptions1Key = L"Options1";
		_wOptions1Value = L"separator at the end" + wPS1;
		_wOptions2Key = L"Options2";
		_wOptions2Value = L"one separator on the "+ wPS1 + L" middle";
		_wOptions3Key = L"Options3";
		_wOptions3Value	= wPS1 + L"even number of separators on the "+ wPS4 + L"middle and odd at the end" + wPS3;
		_wOptions4Key = L"Options4";
		_wOptions4Value = wPS2 + L"odd number of separators on the " + wPS3 + L" middle and even at the end" + wPS2;

		_wKeyValueStr1 = L"key_1" + wKeyValueSeparator + L"<value_1>";
		_wKeyValueStr2 = L"key_2" + wKeyValueSeparator + L"<value_2>";
		_wKeyValueStr3 = L"key_3" + wKeyValueSeparator + L"<value_3>";
		_wAlreadyPreparedString = _wKeyValueStr1 + wPS1 + _wKeyValueStr2 + wPS1 + _wKeyValueStr3;

		_wKeyValueStr4 = L"key_4" + wKeyValueSeparator + L"<value_4>";
		_wKeyValueStr5 = L"key_5" + wKeyValueSeparator + L"<value_5>";
		_wKeyValueStr6 = L"key_6" + wKeyValueSeparator + L"<value_6>";
		_wKeyValueStr7 = L"key_7" + wKeyValueSeparator + L"<value_7>";
		_wStringWithUnnecessarySemicolons = _wKeyValueStr4 + wPS1 + _wKeyValueStr5 + wPS2 + _wKeyValueStr6 + wPS3 + _wKeyValueStr7 + wPS4;

	}

	template<typename Ts, typename Tr>
	void RunScenario_1(
		const Ts keyValueSeparator = KeyValueString<Ts>::DefaultKeyValueSeparator(Ts()),
		const Ts pairSeparator = KeyValueString<Ts>::DefaultPairSeparator(Ts()))
	{
		PrepareTestData(static_cast<wchar_t>(keyValueSeparator), static_cast<wchar_t>(pairSeparator));

		set<basic_string<Tr> > expectedData;
		KeyValueString<Ts> kvString(keyValueSeparator, pairSeparator);
		TestHelper<Ts, Tr> testHelper(kvString, expectedData, pairSeparator, keyValueSeparator);

		Ts SrcType(' ');
		Tr ResType(' ');

		testHelper.AddKeyValuePair(StrHelper::ToAppropriateStr(_wCameraNameKey, SrcType), StrHelper::ToAppropriateStr(_wCameraNameValue, SrcType));
		testHelper.AddKeyValuePair(StrHelper::ToAppropriateStr(_wDeviceIdKey, SrcType), NumStrHelpers::ToString<Ts>(123));
		testHelper.AddKeyValuePair(StrHelper::ToAppropriateStr(_wManufactureKey, SrcType), StrHelper::ToAppropriateStr(_wManufactureValue, SrcType));
		testHelper.AddKeyValuePair(StrHelper::ToAppropriateStr(_wOptions1Key, SrcType), StrHelper::ToAppropriateStr(_wOptions1Value, SrcType));
		testHelper.AddKeyValuePair(StrHelper::ToAppropriateStr(_wOptions2Key, SrcType), StrHelper::ToAppropriateStr(_wOptions2Value, SrcType));
		testHelper.AddKeyValuePair(StrHelper::ToAppropriateStr(_wDeviceIdKey, SrcType), NumStrHelpers::ToString<Ts>(-3816));
		testHelper.AddKeyValuePair(StrHelper::ToAppropriateStr(_wOptions3Key, SrcType), StrHelper::ToAppropriateStr(_wOptions3Value, SrcType));
		testHelper.AddKeyValuePair(StrHelper::ToAppropriateStr(_wOptions4Key, SrcType), StrHelper::ToAppropriateStr(_wOptions4Value, SrcType));

		kvString.AddRawKeyValuesString(StrHelper::ToAppropriateStr(_wAlreadyPreparedString, SrcType));
		expectedData.insert(StrHelper::ToAppropriateStr(_wKeyValueStr1, ResType));
		expectedData.insert(StrHelper::ToAppropriateStr(_wKeyValueStr2, ResType));
		expectedData.insert(StrHelper::ToAppropriateStr(_wKeyValueStr3, ResType));

		basic_string<Ts> stringWithUnnecessarySemicolons = StrHelper::ToAppropriateStr(_wStringWithUnnecessarySemicolons, SrcType);
		kvString.AddRawKeyValuesStringRemoveUnnecessarySemicolons(stringWithUnnecessarySemicolons.c_str(), stringWithUnnecessarySemicolons.length());
		expectedData.insert(StrHelper::ToAppropriateStr(_wKeyValueStr4, ResType));
		expectedData.insert(StrHelper::ToAppropriateStr(_wKeyValueStr5, ResType));
		expectedData.insert(StrHelper::ToAppropriateStr(_wKeyValueStr6, ResType));
		expectedData.insert(StrHelper::ToAppropriateStr(_wKeyValueStr7, ResType));

		testHelper.TestAll();
	}

private:

	wstring _wDeviceIdKey;
	wstring _wCameraNameKey;
	wstring _wCameraNameValue;
	wstring _wManufactureKey;
	wstring _wManufactureValue;
	wstring _wOptions1Key;
	wstring _wOptions1Value;
	wstring _wOptions2Key;
	wstring _wOptions2Value;
	wstring _wOptions3Key;
	wstring _wOptions3Value;
	wstring _wOptions4Key;
	wstring _wOptions4Value;

	wstring _wKeyValueStr1;
	wstring _wKeyValueStr2;
	wstring _wKeyValueStr3;
	wstring _wAlreadyPreparedString;

	wstring _wKeyValueStr4;
	wstring _wKeyValueStr5;
	wstring _wKeyValueStr6;
	wstring _wKeyValueStr7;
	wstring _wStringWithUnnecessarySemicolons;

};

template<typename Ts, typename Tr>
class TestHelper
{
public:
	TestHelper(KeyValueString<Ts> & kvString, set<basic_string<Tr> > & expectedData, Ts pairSeparator, Ts keyValueSeparator):
	  _kvString(kvString),
	  _expectedData(expectedData),
	  _pairSeparator(pairSeparator),
	  _keyValueSeparator(keyValueSeparator)
	{}

	void AddKeyValuePair(const basic_string<Ts> & key, const basic_string<Ts> & value)
	{
		_kvString.Add(key, value);

		_expectedData.insert(
			StrHelper::ToAppropriateStr(key, Tr()) + 
			static_cast<Tr>(_keyValueSeparator) +
			StrHelper::ToAppropriateStr(value, Tr()));
	}

	void TestAll()
	{
		basic_string<Ts> internalString = _kvString.GetStringCopy();
		basic_string<Tr> internalStringOfResultType(StrHelper::ToAppropriateStr(internalString, Tr()));

		KeyValueString<Tr> keyValueStringInstanceToReadFrom(_keyValueSeparator, _pairSeparator);
		keyValueStringInstanceToReadFrom.SetString(internalStringOfResultType);

		set<basic_string<Tr> >::iterator end = _expectedData.end();
		KeyValueString<Tr>::Iterator iEndOfString = keyValueStringInstanceToReadFrom.End();
		for(KeyValueString<Tr>::Iterator i = keyValueStringInstanceToReadFrom.Begin();
			i != iEndOfString;
			++i)
		{
			basic_string<Tr> keyValuePair = *i;
			set<basic_string<Tr> >::iterator f = _expectedData.find(keyValuePair);
			ASSERT_TRUE(end != _expectedData.find(keyValuePair));
		}
	}

private:
	KeyValueString<Ts> & _kvString;
	set<basic_string<Tr> > & _expectedData;
	const Ts _pairSeparator;
	const Ts _keyValueSeparator;
};

TEST(NumStrHelpers_KeyValueString, CreateString_ReadString)
{
	TestScenario testScenario;
	testScenario.RunScenario_1<char, char>();
}

TEST(NumStrHelpers_KeyValueString, CreateString_ReadString_ChangeSeparators)
{
	TestScenario testScenario;
	testScenario.RunScenario_1<char, char>(':', ',');
}

TEST(NumStrHelpers_KeyValueString, CreateWideString_ReadWideString)
{
	TestScenario testScenario;
	testScenario.RunScenario_1<wchar_t, wchar_t>();
}

TEST(NumStrHelpers_KeyValueString, CreateWideString_ReadWideString_ChangeSeparators)
{
	TestScenario testScenario;
	testScenario.RunScenario_1<wchar_t, wchar_t>('^', ']');
}

TEST(NumStrHelpers_KeyValueString, CreateWideString_ReadString)
{
	TestScenario testScenario;
	testScenario.RunScenario_1<wchar_t, char>();
}

TEST(NumStrHelpers_KeyValueString, CreateWideString_ReadString_ChangeSeparators)
{
	TestScenario testScenario;
	testScenario.RunScenario_1<wchar_t, char>('-', '|');
}

TEST(NumStrHelpers_KeyValueString, CreateString_ReadWideString)
{
	TestScenario testScenario;
	testScenario.RunScenario_1<char, wchar_t>();
}

TEST(NumStrHelpers_KeyValueString, CreateString_ReadWideString_ChangeSeparators)
{
	TestScenario testScenario;
	testScenario.RunScenario_1<char, wchar_t>('.', '&');
}