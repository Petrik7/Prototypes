#pragma once

#include <algorithm>
#include <locale>
#include "ArrayAutoPtr.hpp"

class NumStrHelpers
{
public:

	static bool TryStringToInt(std::wstring string, int * number)
	{
		if(!IsNumber(string) || number == 0)
			return false;
		*number = _wtoi(string.c_str());
		return true;
	}

	static bool IsNumber(std::wstring string)
	{
		// It's not a 100% protection from overflow but at least something.
		if(string.length() == 0 || string.length() > 11)
			return false;
		else if(*(string.begin()) == L'-' && string.length() == 1)
			return false;
		else if(*(string.begin()) == L'-')
			return std::find_if(string.begin() + 1, string.end(), WCharIsNotDigit) == string.end();
		
		 return std::find_if(string.begin(), string.end(), WCharIsNotDigit) == string.end();
	}

	static bool IsOdd(size_t number)
	{
		return number & 1;
	}

	static bool WCharIsDigit(wchar_t character)
	{
		std::locale loc;
		return std::isdigit(character, loc);
	}

	static bool WCharIsNotDigit(wchar_t character)
	{
 		return !WCharIsDigit(character);
	}

	static inline std::wstring DefaultIntFormating(wchar_t) {	return L"%d"; }
	static inline std::string DefaultIntFormating(char)		{	return "%d";  }
	static inline std::wstring DefaultWideStrFormating(wchar_t) {	return L"%ws"; }
	static inline std::string DefaultWideStrFormating(char)		{	return "%ws";  }

	template<typename T>
	static std::basic_string<T> ToString(int number, std::basic_string<T> format = DefaultIntFormating(T()))
	{
		const size_t buffSize = 24;
		T buffer[buffSize] = {0};
		SprintfT<int>(buffer, buffSize, format.c_str(), number);

		return std::basic_string<T>(buffer);
	}

protected:

	template<typename T>
	static int SprintfT(char * destination, size_t count, const char * format, T value)
	{
		return _snprintf(destination, count, format, value);
	}

	template<typename T>
	static int SprintfT(wchar_t * destination, size_t count, const wchar_t * format, T value)
	{
		return _snwprintf(destination, count, format, value);
	}

public:

	static inline std::string ToString(const std::wstring & input)
	{
		return ToString(input.c_str(), input.length());
	}

	static std::string ToString(const wchar_t * input, const size_t length)
	{
		const size_t staticBuffSize = 128;
		char * buffer = 0;
		size_t buffLen = staticBuffSize;
		char staticBuffer[staticBuffSize ] = {0};
		ArrayAutoPtr<char> dynamicBufferPtr;
		if(length < staticBuffSize)
			buffer = staticBuffer;
		else
		{
			buffLen = length + 1;
			dynamicBufferPtr.Reset(new char[buffLen]);
			buffer = dynamicBufferPtr.GetRawPointer();
		}
		
		_snprintf(buffer, buffLen, "%ws", input);

		return std::string(buffer);
	}

};
