#pragma once

#include <queue>

class Lock
{
public:
	Lock(LPCRITICAL_SECTION criticalSection):_criticalSection(criticalSection)
	{
		EnterCriticalSection(criticalSection);
	}

	~Lock()
	{
		LeaveCriticalSection(_criticalSection);
	}
private:
	LPCRITICAL_SECTION _criticalSection;
};


template<typename T>
class ThreadSafeQueue
{
public:

	ThreadSafeQueue()
	{
		//InitializeCriticalSectionAndSpinCount(&_criticalSection, 0x00000400);
		InitializeCriticalSection(&_criticalSection);
	}

	~ThreadSafeQueue()
	{
		DeleteCriticalSection(&_criticalSection);
	}

	void Enqueue(const T & item)
	{
		Lock lock(&_criticalSection);
		_tasksQueue.push(item);
	}

	bool Dequeue(T * item)
	{
		Lock lock(&_criticalSection);
		if(IsEmpty())
			return false;
		else
		{
			*item = _tasksQueue.front();
			_tasksQueue.pop();
			return true;
		}
	}

	bool IsEmpty()
	{
		Lock lock(&_criticalSection);
		return _tasksQueue.empty();
	}

private:
	CRITICAL_SECTION _criticalSection;
	std::queue<T> _tasksQueue;
};