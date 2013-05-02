#pragma once

#include <windows.h>
#include "ThreadSafeQueue.hpp"

template<typename Tworker, typename Ttask>
class WorkerThread
{
public:
	typedef void (Tworker::*WorkerMemberMethod) (Ttask & task);
	typedef void (*WorkerStaticMethod) (Ttask & task);

	WorkerThread(Tworker * workerInstance, WorkerMemberMethod workerMethod):
		_workerInstance(workerInstance),
		_workerMemberMethod(workerMethod),
		_workerStaticMethod(0),
		_thread(0)
	{
	}

	WorkerThread(WorkerStaticMethod workerStaticMethod):
		_workerInstance(0),
		_workerMemberMethod(0),
		_workerStaticMethod(workerStaticMethod),
		_thread(0)
	{
	}

	~WorkerThread(){};

	//If the function succeeds, the return value is true
	bool Start()
	{
		if(!_thread)
			CreateThreadAndInit();

		return ResumeThread(_thread) != -1;
	}

	void Enqueue(const Ttask & task)
	{
		OutputDebugString(L"Enqueue task\n");
		_tasksQueue.Enqueue(task);
		SetEvent(_newTaskArrived);
	}

	// Finish all tasks in the queue and exit
	void Stop()
	{
		OutputDebugString(L"WorkerThread Stop requested\n");
		if(!_thread || _stopped)
			return;

		_stopped = true;
		SetEvent(_newTaskArrived);
		CloseHandle(_newTaskArrived);
		_newTaskArrived = NULL;
	}

	// Finish a current task and exit ignoring the rest of tasks in the queue
	void TerminateGracefully()
	{
		_terminateRequested = true;
	}

	//The Terminate calls the TerminateThread which is a dangerous function that should only be used in the most extreme cases.
	void Terminate()
	{
		OutputDebugString(L"WorkerThread Terminating...\n");
		TerminateThread(_thread, 18);
		CloseHandle(_thread);
		_thread = NULL;
	}

	bool IsStopped()
	{
		return _stopped;
	}

	void Join()
	{
		OutputDebugString(L"Joining...\n");
		if(_thread)
			WaitForSingleObject(_thread, INFINITE);
	}	

	DWORD  Join(int timeout)
	{
		OutputDebugString(L"Joining with timeout...\n");
		if(_thread)
			return WaitForSingleObject(_thread, timeout);
		else
			return WAIT_OBJECT_0;
	}

private:

	void CreateThreadAndInit()
	{
		_stopped = false;
		_terminateRequested = false;
		_newTaskArrived = CreateEvent(NULL, false, true, NULL);
		_thread = CreateThread(NULL, 0, ThreadMethod, this, CREATE_SUSPENDED, NULL);
	}

	static DWORD WINAPI ThreadMethod(LPVOID arg)
	{
		WorkerThread * workerThread = ((WorkerThread*)arg);
		assert(workerThread);

		Ttask task;
		while(!workerThread->IsStopped())
		{
			if(workerThread->_tasksQueue.IsEmpty())
			{
				WaitForSingleObject(workerThread->_newTaskArrived, INFINITE);
				OutputDebugString(L"WorkerThread Got signal!\n");
			}
			
			while(!workerThread->_terminateRequested && workerThread->_tasksQueue.Dequeue(&task))
			{
				workerThread->RunMethod(task);
			}
		}

		OutputDebugString(L"WorkerThread Exiting\n");
		return 0;
	}

	void RunMethod(Ttask & task)
	{
		if(_workerInstance)
			(_workerInstance->*_workerMemberMethod)(task);
		else
			(*_workerStaticMethod)(task);
	}

	Tworker * _workerInstance;
	WorkerMemberMethod _workerMemberMethod;
	WorkerStaticMethod _workerStaticMethod;
	volatile bool _stopped;
	volatile bool _terminateRequested;
	HANDLE _thread;
	HANDLE _newTaskArrived;
	ThreadSafeQueue<Ttask> _tasksQueue;


};
