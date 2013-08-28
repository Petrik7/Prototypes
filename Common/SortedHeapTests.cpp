#include "stdafx.h"

#include "gtest.h"

#include <string>

#include <windows.h>
#include <stdlib.h>     /* srand, rand */
#include <time.h>       /* time */

//#include <algorithm>

//#include <vector> // tmp test

#include "../Common/SortedHeap.hpp"

#undef max
#undef min

using namespace Heap;

template<typename Tk, typename Tv>
class SortedHeapTest : public SortedHeap<Tk, Tv>
{
public:
	SortedHeapTest(Heap::Type heapType = Heap::MaxHeap):SortedHeap<Tk, Tv>(heapType)
	{}

	void PrintItemsInLine() const
	{
		SortedHeap::PrintItemsInLine();
	}

	bool VerifyIndex() const
	{
		return SortedHeap::VerifyIndex();
	}

	long VerifyHeap() const
	{
		return SortedHeap::VerifyHeap();
	}
};

TEST(SortedHeapTest1, TestInsert1)
{
	OutputDebugString(L"TestInsert1\n");
	
	std::vector<int> testData;
	testData.push_back(15);
	testData.push_back(9);
	testData.push_back(7);
	testData.push_back(4);
	testData.push_back(6);
	testData.push_back(5);
	testData.push_back(3);
	testData.push_back(17);

	SortedHeapTest<int, std::string> sortedHeap;
	for(std::vector<int> :: iterator i = testData.begin();
		i < testData.end();
		++i)
	{
		sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
	}

	ASSERT_TRUE(sortedHeap.Size() == testData.size());
	ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
	ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);
	
	std::vector<int> expectedOrder;
	expectedOrder.push_back(17);
	expectedOrder.push_back(15);
	expectedOrder.push_back(7);
	expectedOrder.push_back(9);
	expectedOrder.push_back(6);
	expectedOrder.push_back(5);
	expectedOrder.push_back(3);
	expectedOrder.push_back(4);

	sortedHeap.PrintItemsInLine();
	std::cout << std::endl;

	for(size_t i = 0; i < expectedOrder.size(); ++i)
	{
		ASSERT_TRUE(sortedHeap[i].first == expectedOrder[i]);
	}

	int prevKey = std::numeric_limits<int>::max(); 
	while(!sortedHeap.IsEmpty())
	{
		std::pair<int, std::string> item = sortedHeap.Top();
		sortedHeap.Pop();
		if(prevKey < item.first)
			int k = 0;
		ASSERT_TRUE(prevKey > item.first);
		ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
		ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);
		prevKey = item.first;
	}
}

TEST(SortedHeapTest1, TestInsert2)
{
	OutputDebugString(L"TestInsert2\n");

	std::vector<int> testData;
	testData.push_back(9);
	testData.push_back(10);
	testData.push_back(5);
	testData.push_back(4);
	testData.push_back(15);
	testData.push_back(15);
	testData.push_back(4);
	testData.push_back(11);
	testData.push_back(14);
	testData.push_back(11);

	SortedHeapTest<int, std::string> sortedHeap;
	for(std::vector<int> :: iterator i = testData.begin();
		i < testData.end();
		++i)
	{
		sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
	}

	ASSERT_TRUE(sortedHeap.Size() == testData.size());
	ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
	ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

	sortedHeap.PrintItemsInLine();
	std::cout << std::endl;

	int prevKey = std::numeric_limits<int>::max(); 
	while(!sortedHeap.IsEmpty())
	{
		std::pair<int, std::string> item = sortedHeap.Top();
		sortedHeap.Pop();
		if(prevKey < item.first)
			int k = 0;
		ASSERT_TRUE(prevKey >= item.first);
		ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
		ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);
		prevKey = item.first;
	}
}

TEST(SortedHeapTest1, TestInsert3)
{
	OutputDebugString(L"TestInsert3\n");
	std::vector<int> testData;
	testData.push_back(12);
	testData.push_back(19);
	testData.push_back(10);
	testData.push_back(7);
	testData.push_back(13);
	testData.push_back(13);
	testData.push_back(16);
	testData.push_back(3);
	testData.push_back(14);
	testData.push_back(5);

	SortedHeapTest<int, std::string> sortedHeap;
	for(std::vector<int> :: iterator i = testData.begin();
		i < testData.end();
		++i)
	{
		sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
	}

	ASSERT_TRUE(sortedHeap.Size() == testData.size());
	ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
	ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

	sortedHeap.PrintItemsInLine();
	std::cout << std::endl;

	int prevKey = std::numeric_limits<int>::max(); 
	while(!sortedHeap.IsEmpty())
	{
		std::pair<int, std::string> item = sortedHeap.Top();
		sortedHeap.Pop();
		if(prevKey < item.first)
			int k = 0;
		ASSERT_TRUE(prevKey >= item.first);
		ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
		ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);
		prevKey = item.first;
	}
}

TEST(SortedHeapTest1, TestChangePriority)
{
	OutputDebugString(L"TestChangePriority\n");
	std::vector<int> testData;
	testData.push_back(12);
	testData.push_back(19);
	testData.push_back(10);
	testData.push_back(7);
	testData.push_back(13);
	testData.push_back(13);
	testData.push_back(16);
	testData.push_back(3);
	testData.push_back(14);
	testData.push_back(5);

	SortedHeapTest<int, std::string> sortedHeap;
	for(std::vector<int> :: iterator i = testData.begin();
		i < testData.end();
		++i)
	{
		sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
	}

	ASSERT_TRUE(sortedHeap.Size() == testData.size());
	ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
	ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

	sortedHeap.PrintItemsInLine();

	sortedHeap.ChangePriority("5", 50);
	sortedHeap.ChangePriority("19", 9);
	sortedHeap.ChangePriority("13", 130);
	sortedHeap.ChangePriority("10", 1);
	sortedHeap.ChangePriority("3", 30);
	sortedHeap.ChangePriority("14", 28);

	sortedHeap.Insert(29, "29");
	sortedHeap.Insert(15, "15");

	std::cout << std::endl;

	ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
	ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

	int prevKey = std::numeric_limits<int>::max(); 
	while(!sortedHeap.IsEmpty())
	{
		std::pair<int, std::string> item = sortedHeap.Top();
		std::cout << NumStrHelpers::ToString<char>(item.first) << ", " << item.second << " ; ";
		sortedHeap.Pop();
		if(prevKey < item.first)
			int k = 0;
		ASSERT_TRUE(prevKey >= item.first);
		ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
		prevKey = item.first;
	}

	std::cout << std::endl;
}

TEST(SortedHeapTest1, TestMinHeapChangePriority)
{
	OutputDebugString(L"TestMinHeapChangePriority\n");
	std::vector<int> testData;
	testData.push_back(12);
	testData.push_back(19);
	testData.push_back(10);
	testData.push_back(7);
	testData.push_back(13);
	testData.push_back(13);
	testData.push_back(16);
	testData.push_back(3);
	testData.push_back(14);
	testData.push_back(5);

	SortedHeapTest<int, std::string> sortedHeap(Heap::MinHeap);
	for(std::vector<int> :: iterator i = testData.begin();
		i < testData.end();
		++i)
	{
		sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
	}

	ASSERT_TRUE(sortedHeap.Size() == testData.size());
	ASSERT_TRUE(sortedHeap.VerifyIndex() == true);

	sortedHeap.PrintItemsInLine();

	sortedHeap.ChangePriority("5", 50);
	sortedHeap.ChangePriority("19", 9);
	sortedHeap.ChangePriority("13", 130);
	sortedHeap.ChangePriority("10", 1);
	sortedHeap.ChangePriority("3", 30);
	sortedHeap.ChangePriority("14", 28);

	ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
	ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

	sortedHeap.Insert(29, "29");
	sortedHeap.Insert(15, "15");

	std::cout << std::endl;

	ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
	ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

	int prevKey = std::numeric_limits<int>::min(); 
	while(!sortedHeap.IsEmpty())
	{
		std::pair<int, std::string> item = sortedHeap.Top();
		std::cout << NumStrHelpers::ToString<char>(item.first) << ", " << item.second << " ; ";
		sortedHeap.Pop();
		if(prevKey > item.first)
			int k = 0;
		ASSERT_TRUE(prevKey <= item.first);
		ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
		ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

		prevKey = item.first;
	}

	std::cout << std::endl;
}

TEST(SortedHeapTest1, TestRandomMaxHeap)
{
	OutputDebugString(L"TestRandomMaxHeap\n");
	std::cout << "cilcle:" << std::endl;
	for(size_t nt = 0; nt < 5; ++nt)
	{
		std::cout << nt << ", ";

		std::vector<int> testData;

		srand (time(NULL));
		const size_t numOfItems = 500;
		for(size_t i = 0; i < numOfItems; ++i)
		{
			int number = rand() % 1000;
			testData.push_back(number);
		}

		SortedHeapTest<int, std::string> sortedHeap;
		for(std::vector<int> :: iterator i = testData.begin();
			i < testData.end();
			++i)
		{
			sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
		}

		for(std::vector<int> :: iterator i = testData.begin();
			i < testData.end();
			++i)
		{
			int newpriority = rand() % 1000;
			sortedHeap.ChangePriority(NumStrHelpers::ToString<char>(*i), newpriority);
			ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);
		}

		ASSERT_TRUE(sortedHeap.Size() == testData.size());
		ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
		ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

		std::vector<int> prioritizedData;
	
		int prevKey = std::numeric_limits<int>::max(); 
		while(!sortedHeap.IsEmpty())
		{
			std::pair<int, std::string> item = sortedHeap.Top();
			prioritizedData.push_back(item.first);
			sortedHeap.Pop();
			if(prevKey < item.first)
				std::for_each(prioritizedData.begin(), prioritizedData.end(), ContainerHelpers::PrintItemsInLine<int>);
			ASSERT_TRUE(prevKey >= item.first);
			ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
			ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

			prevKey = item.first;
		}
	}

	std::cout << "done" << std::endl;
}

TEST(SortedHeapTest1, TestRandomMinHeap)
{
	OutputDebugString(L"TestRandomMinHeap\n");
	std::cout << "cilcle:" << std::endl;
	for(size_t nt = 0; nt < 5; ++nt)
	{
		std::cout << nt << ", ";

		std::vector<int> testData;

		srand (time(NULL));
		const size_t numOfItems = 500;
		for(size_t i = 0; i < numOfItems; ++i)
		{
			int number = rand() % 1000;
			testData.push_back(number);
		}

		SortedHeapTest<int, std::string> sortedHeap(Heap::MinHeap);
		for(std::vector<int> :: iterator i = testData.begin();
			i < testData.end();
			++i)
		{
			sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
		}

		for(std::vector<int> :: iterator i = testData.begin();
			i < testData.end();
			++i)
		{
			int newpriority = rand() % 1000;
			sortedHeap.ChangePriority(NumStrHelpers::ToString<char>(*i), newpriority);
			ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);
		}

		ASSERT_TRUE(sortedHeap.Size() == testData.size());
		ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
		ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

		std::vector<int> prioritizedData;
	
		int prevKey = std::numeric_limits<int>::min(); 
		while(!sortedHeap.IsEmpty())
		{
			std::pair<int, std::string> item = sortedHeap.Top();
			prioritizedData.push_back(item.first);
			sortedHeap.Pop();
			if(prevKey > item.first)
				std::for_each(prioritizedData.begin(), prioritizedData.end(), ContainerHelpers::PrintItemsInLine<int>);
			ASSERT_TRUE(prevKey <= item.first);
			ASSERT_TRUE(sortedHeap.VerifyIndex() == true);
			ASSERT_TRUE(sortedHeap.VerifyHeap() == -1);

			prevKey = item.first;
		}
	}

	std::cout << "done" << std::endl;
}
