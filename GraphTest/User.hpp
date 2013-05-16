#pragma once

#include <windows.h>

template<typename T>
class User
{
public:

	User():_id(-1)
	{
		OutputDebugString(L"User Default C-tor ->\n");
	}

	User(T id):_id(id)
	{
		OutputDebugString(L"User C-tor 1 par ->\n");
	}

	User(const User & other)
	{
		_id = other._id;
		std::cout << "User copy C-tor ->" << std::endl;
		OutputDebugString(L"User copy C-tor\n");
	}

	~User()
	{
		OutputDebugString(L"User De-tor <--- \n");
	}

	User & operator = (const User & other)
	{
		_id = other._id;
		std::cout << "User operator =" << std::endl;
		OutputDebugString(L"User operator =\n");
		return *this;
	}

	T GetId()
	{
		return _id;
	}

private:
	T _id;
};

//class User
//{
//public:
//
//	User():_id(-1)
//	{
//		OutputDebugString(L"User Default C-tor ->\n");
//	}
//
//	User(int id):_id(id)
//	{
//		OutputDebugString(L"User C-tor 1 par ->\n");
//	}
//
//	User(const User & other)
//	{
//		_id = other._id;
//		std::cout << "User copy C-tor ->" << std::endl;
//		OutputDebugString(L"User copy C-tor\n");
//	}
//
//	~User()
//	{
//		OutputDebugString(L"User De-tor <--- \n");
//	}
//
//	User & operator = (const User & other)
//	{
//		_id = other._id;
//		std::cout << "User operator =" << std::endl;
//		OutputDebugString(L"User operator =\n");
//		return *this;
//	}
//
//	int GetId()
//	{
//		return _id;
//	}
//
//private:
//	int _id;
//};



