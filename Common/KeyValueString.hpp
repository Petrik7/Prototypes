
#pragma once

#include <string>
#include <limits>

#include "NumStrHelpers.hpp"

#undef max

using std::basic_string;

template<typename T>
class KeyValueString
{
public:
	class Iterator;

	static inline wchar_t	EOS(wchar_t)	{	return L'\0'; }
	static inline char		EOS(char)		{	return '\0';  }
	static inline wchar_t	DefaultPairSeparator(wchar_t)		{	return L';'; }
	static inline char		DefaultPairSeparator(char)			{	return ';';  }
	static inline wchar_t	DefaultKeyValueSeparator(wchar_t)	{	return L'='; }
	static inline char		DefaultKeyValueSeparator(char)		{	return '=';  }

	KeyValueString(const T keyValueSeparator = DefaultKeyValueSeparator(T()), const T pairSeparator = DefaultPairSeparator(T()));
	~KeyValueString(void);

	// AddRawKeyValuesString allows to add already prepared key-value pairs string
	// which_has_a_proper formating to KeyValueString
	void AddRawKeyValuesString(const basic_string<T> & rawString);
	void AddRawKeyValuesStringRemoveUnnecessarySemicolons(const T * optionsSubString, size_t inputLen);
	void Add(const basic_string<T> & key, const basic_string<T> & value);
	void Add(const basic_string<T> & key, int value);
	typename KeyValueString<T>::Iterator Begin() const;
	typename KeyValueString<T>::Iterator End() const;
	basic_string<T> GetStringCopy() const;
	void SetString(const basic_string<T> & keyValueString);

	class Iterator
	{
	public:
		Iterator(const KeyValueString & kvString, const basic_string<T> & pair, size_t position = 0):
		  _kvString(kvString),
		  _keyValuePair(pair),
		  _position(position)
		{
		}

		static inline size_t EndOfStream()
		{
			static const size_t endOfStream = std::numeric_limits<size_t>::max();
			return endOfStream;
		}

		bool operator != (const Iterator & other) const
		{
			VerifyOtherIterator(other);
			return _position != other._position;
		}

		bool operator < (const Iterator & other) const
		{
			VerifyOtherIterator(other);
			return _position < other._position;
		}
		
		Iterator & operator++()
		{
			if(_position == EndOfStream())
				throw std::exception ("Iterator already points to the end of the stream");
			
			_keyValuePair.clear();
			_position = _kvString.FindNextPair(_position, _keyValuePair);

			return *this;
		}

		basic_string<T> operator *() const
		{
			return _keyValuePair;
		}

	private:

		void VerifyOtherIterator(const Iterator & other) const
		{
			if(&_kvString != &(other._kvString))
				throw std::exception("Iterators do not belong to the same container");
		}

		const KeyValueString & _kvString;
		basic_string<T> _keyValuePair;
		size_t _position;
	};

private:
	const T _keyValueSeparator;
	const T _pairSeparator;
	basic_string<T> _kvstring;

	size_t FindNextPair(size_t beginPosition, basic_string<T> & resultPair) const;
	static basic_string<T> Encode(const basic_string<T> & sourceData, const T separator);
	static basic_string<T> Decode(const basic_string<T> & encodedPair, const T separator);
};

// Implementation

template<typename T>
KeyValueString<T>::KeyValueString(const T keyValueSeparator, const T pairSeparator):
	_keyValueSeparator(keyValueSeparator), 
	_pairSeparator(pairSeparator)
{
}

template<typename T>
KeyValueString<T>::~KeyValueString(void)
{
}

template<typename T>
void KeyValueString<T>::AddRawKeyValuesString(const basic_string<T> & rawString)
{
	_kvstring += rawString.c_str();
	if(rawString[rawString.length() - 1] != _pairSeparator)
		_kvstring += _pairSeparator;
}

// Assumption: a value should not contain a separator. More than one separator in succession
// means the key-value pair was not added for some reason but separator was.
// Example: format=MPEG;;com=;
//         skiped pair ^
template<typename T>
void KeyValueString<T>::AddRawKeyValuesStringRemoveUnnecessarySemicolons(const T * optionsSubString, size_t inputLen)
{
	if(0 == inputLen)
		return;

	basic_string<T> cleanSubOptions;
	
	cleanSubOptions.reserve(inputLen);

	if(_pairSeparator != optionsSubString[0])
		cleanSubOptions += optionsSubString[0];

	for(size_t i = 1; i < inputLen; ++i)
	{
		if(_pairSeparator != optionsSubString[i] || _pairSeparator != optionsSubString[i - 1])
			cleanSubOptions += optionsSubString[i];
	}
	AddRawKeyValuesString(cleanSubOptions);
}

template<typename T>
void KeyValueString<T>::Add(const basic_string<T> & key, const basic_string<T> & value)
{
	_kvstring += key + _keyValueSeparator +  Encode(value, _pairSeparator);
}

template<typename T>
void KeyValueString<T>::Add(const basic_string<T> & key, int value)
{
	_kvstring += key + _keyValueSeparator + NumStrHelpers::ToString<T>(value) + _pairSeparator;
}

template<typename T>
typename KeyValueString<T>::Iterator KeyValueString<T>::Begin() const
{
	basic_string<T> pair;
	size_t nextPairPosition = FindNextPair(0, pair);
	if(nextPairPosition == Iterator::EndOfStream())
		return End();
	return Iterator(*this, pair, nextPairPosition);
}

template<typename T>
typename KeyValueString<T>::Iterator KeyValueString<T>::End() const
{
	return Iterator(*this, basic_string<T>(), Iterator::EndOfStream());
}

template<typename T>
basic_string<T> KeyValueString<T>::GetStringCopy() const
{
	return _kvstring;
}

template<typename T>
void KeyValueString<T>::SetString(const basic_string<T> & keyValueString)
{
	_kvstring = keyValueString;
}

template<typename T>
size_t KeyValueString<T>::FindNextPair(size_t beginPosition, basic_string<T> & resultPair) const
{
	if(beginPosition >= _kvstring.length())
		return Iterator::EndOfStream();
	
	basic_string<T> encodedPair;
	size_t separatorCounter = 0;
	size_t i = beginPosition;
	for(; i < _kvstring.length(); ++i)
		if(_pairSeparator != _kvstring[i])
			encodedPair += _kvstring[i];
		else
		{   // if it's the last separator in the stream return the detected pair
			if(i + 1 == _kvstring.length()) // key=value>;<EOS
				break;                      //    we are ^ here
			
			if(_pairSeparator != _kvstring[i + 1])
			{	// if previous and next characters are not separator it's a stand alone separator so a pair is found
				if(_pairSeparator != _kvstring[i -1 ]) // key1=value1>;<key2=value2...
					break;                             //      we are ^ here
				// odd number of separators is found - this is actually the separator, a pair is found
				if(NumStrHelpers::IsOdd(separatorCounter + 1))  // key1=value1;;>;<key2=value2...
					break;                                      //        we are ^ here
				else
				{	// we found even number of separators - they are separators from original value string + stuffed characters
					encodedPair += _kvstring[i]; // key1=some_value_with_semicolons;>;<on_the_middle;key2=value2...
					separatorCounter = 0;        //                           we are ^ here
				}
			}
			else
			{	//next character is also separator - copy current separator to output and increase separatorCounter
				encodedPair += _kvstring[i];// key1=some_value_with_semicolons>;;;<;on_the_middle;key2=value2...
				++separatorCounter;         //                          we are ^^^ here
			}
		}

	resultPair = Decode(encodedPair, _pairSeparator);
	return i+1;
}

template<typename T>
basic_string<T> KeyValueString<T>::Encode(const basic_string<T> & sourceData, const T separator)
{
	basic_string<T> result;
	result.reserve(sourceData.length() + 8);
	size_t i = 0;
	const T terminatingNull = EOS(T());
	while(terminatingNull != sourceData[i])
	{
		result += sourceData[i];
		if(sourceData[i] == separator)
			result += separator;
		++i;
	}
	result += separator;
	
	return result;
}

template<typename T>
basic_string<T> KeyValueString<T>::Decode(const basic_string<T> & encodedPair, const T separator)
{
	basic_string<T> result;
	result.reserve(encodedPair.length());
	size_t i = 0;
	const T terminatingNull = EOS(T());
	while(terminatingNull != encodedPair[i])
	{
		if(separator != encodedPair[i])
			result += encodedPair[i];
		else if(separator == encodedPair[i + 1])
	    {
			result += separator;
			++i;
		}
		++i;
	}
	return result;
}
