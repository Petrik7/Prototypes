#pragma once

template<typename T>
class ArrayAutoPtr
{
public:
	ArrayAutoPtr():_array(0)
	{
	}

	ArrayAutoPtr(T * arrayToKeep):_array(arrayToKeep)
	{
	}

	~ArrayAutoPtr(void)
	{
		delete [] _array;
	}

	void Reset(T * arrayToKeep)
	{
		delete [] _array;
		_array = arrayToKeep;
	}

	// Do not delete this pointer manually ever. ArrayAutoPtr will delete it automatically.
	T * GetRawPointer()
	{
		return _array;
	}

private:
	ArrayAutoPtr(const ArrayAutoPtr & ){};
	ArrayAutoPtr & operator= (const ArrayAutoPtr & other){};

	T * _array;

};
