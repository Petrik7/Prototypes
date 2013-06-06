#pragma once

#include <windows.h>

template<typename T>
class	User
{
public:

	static size_t _created;
	static size_t _deleted;

	User():_id(T())
	{
		++_created;
		OutputDebugString(L"User Default C-tor ->\n");
	}

	User(T id):_id(id)
	{
		++_created;
		OutputDebugString(L"User C-tor 1 par ->\n");
	}

	User(const User & other)
	{
		++_created;
		_id = other._id;
		//std::cout << "User copy C-tor ->" << std::endl;
		OutputDebugString(L"User copy C-tor\n");
	}

	~User()
	{
		++_deleted;
		OutputDebugString(L"User De-tor <--- \n");
	}

	User & operator = (const User & other)
	{
		_id = other._id;
		//std::cout << "User operator =" << std::endl;
		OutputDebugString(L"User operator =\n");
		return *this;
	}

	T GetId() const
	{
		return _id;
	}

private:
	T _id;
};

template<typename T>
std::ostream & operator << (std::ostream & outStream, const User<T> & user)
{
	return outStream << user.GetId();
}
