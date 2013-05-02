#include "stdafx.h"

#include "gtest.h"

#include <string>
#include <memory>

#include "../Common/WorkerThread.hpp"
#include "../Common/NumStrHelpers.hpp"

class Task
{
public:
	Task():TaskId(88)
	{}

	Task(int id):TaskId(id)
	{
		/*std::wstring message(L"+++ Task C-tor ->");
		message += NumStrHelpers::ToString<wchar_t>(id) + L"\n";
		OutputDebugString(message.c_str());*/
	};

	~Task()
	{
		/*std::wstring message(L"+++ Task De-tor <=====");
		message += NumStrHelpers::ToString<wchar_t>(TaskId) + L"\n";
		OutputDebugString(message.c_str());*/
	};

	Task(const Task & other)
	{
		TaskId = other.TaskId;
		/*std::wstring message(L"+++ Task COPY C-tor ->");
		message += NumStrHelpers::ToString<wchar_t>(other.TaskId) + L"\n";
		OutputDebugString(message.c_str());*/
	}

	int TaskId;
};

class Worker
{
public:
	Worker():counter(0){};

	//void RunMethod(std::tr1::shared_ptr<Task> & task)
	static void RunStaticMethod(Task & task)
	{
		std::wstring message(L"+++ Static Fun Processing Task ");
		message += NumStrHelpers::ToString<wchar_t>(task.TaskId) + L"\n";
		OutputDebugString(message.c_str());
		//++counter;
	}

	void RunMemberMethod(Task & task)
	{
		std::wstring message(L"+++ Member Fun Processing Task ");
		message += NumStrHelpers::ToString<wchar_t>(task.TaskId) + L"\n";
		OutputDebugString(message.c_str());
		++counter;
	}

	int counter;
};


//void WorkerMethod (Task task)
//{
//	std::wstring message(L"+++ Processing non-member\n");
//	OutputDebugString(message.c_str());//("ODS");
//}

class TestWorker
{
public:
	
	TestWorker():workerThread(this, &TestWorker::RunMemberMethod)
	{
		workerThread.Start();
	};

	void RunMemberMethod(Task & task)
	{
		std::wstring message(L"+++ Member Fun Processing Task ");
		message += NumStrHelpers::ToString<wchar_t>(task.TaskId) + L"\n";
		OutputDebugString(message.c_str());
		++counter;
	}
	
	void Run()
	{
		Task task(123);
		workerThread.Enqueue(task);
	}

	WorkerThread<TestWorker, Task > workerThread;

	int counter;
};

TEST(WorkerThread_Test1, JustRun)
{

	int a[5] = {11, 22, 33, 44, 55};
	int b[5];

	int size = _countof(a);
	std::copy(&a[0], &a[_countof(a)], b);


	TestWorker testWorker;
	testWorker.Run();

	Worker worker;
	//WorkerThread<Worker, std::tr1::shared_ptr<Task> > workerThread(&worker, &Worker::RunMethod);
	WorkerThread<Worker, Task > workerThread(&worker, &Worker::RunMemberMethod);
	workerThread.Start();
	//WorkerThread<Worker, Task > workerThread(&Worker::RunStaticMethod);
	int numberOfTasks = 5;
	for(int i = 0; i < numberOfTasks; ++i)
	{
		Task task(i);
		//std::tr1::shared_ptr<Task> task(new Task(i));
		workerThread.Enqueue(task);
		//Sleep(1000);
	}
	
	//Sleep(3000);

	workerThread.Stop();
	workerThread.Stop();
	//workerThread.TerminateGracefully();
	workerThread.Join();
	/*DWORD result = workerThread.Join(4000);
	if(WAIT_OBJECT_0 != result)
		workerThread.Terminate();
*/

	//ASSERT_TRUE(worker.counter == numberOfTasks);
	EXPECT_EQ(worker.counter, numberOfTasks);
}
