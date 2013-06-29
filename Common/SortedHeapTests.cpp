#include "stdafx.h"

#include "gtest.h"

#include <string>

#include <windows.h>
#include <stdlib.h>     /* srand, rand */
#include <time.h>       /* time */

//#include <algorithm>

#include <vector> // tmp test

#include "../Common/SortedHeap.hpp"

#undef max

TEST(SortedHeapTest1, TestInsert1)
{
	std::vector<int> testData;
	testData.push_back(15);
	testData.push_back(9);
	testData.push_back(7);
	testData.push_back(4);
	testData.push_back(6);
	testData.push_back(5);
	testData.push_back(3);
	testData.push_back(17);

	SortedHeap<int, std::string> sortedHeap;
	for(std::vector<int> :: iterator i = testData.begin();
		i < testData.end();
		++i)
	{
		sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
	}

	ASSERT_TRUE(sortedHeap.Size() == testData.size());

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

	for(size_t i = 0; i < expectedOrder.size(); ++i)
	{
		ASSERT_TRUE(sortedHeap[i].first == expectedOrder[i]);
	}

	int prevKey = std::numeric_limits<int>::max(); 
	while(!sortedHeap.IsEmpty())
	{
		std::pair<int, std::string> item = sortedHeap.PeekMaxItem();
		sortedHeap.PopMaxItem();
		if(prevKey < item.first)
			int k = 0;
		ASSERT_TRUE(prevKey > item.first);
		prevKey = item.first;
	}
}

TEST(SortedHeapTest1, TestInsert2)
{
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

	SortedHeap<int, std::string> sortedHeap;
	for(std::vector<int> :: iterator i = testData.begin();
		i < testData.end();
		++i)
	{
		sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
	}

	ASSERT_TRUE(sortedHeap.Size() == testData.size());

	sortedHeap.PrintItemsInLine();

	int prevKey = std::numeric_limits<int>::max(); 
	while(!sortedHeap.IsEmpty())
	{
		std::pair<int, std::string> item = sortedHeap.PeekMaxItem();
		sortedHeap.PopMaxItem();
		if(prevKey < item.first)
			int k = 0;
		ASSERT_TRUE(prevKey >= item.first);
		prevKey = item.first;
	}
}

TEST(SortedHeapTest1, TestInsert3)
{
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

	SortedHeap<int, std::string> sortedHeap;
	for(std::vector<int> :: iterator i = testData.begin();
		i < testData.end();
		++i)
	{
		sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
	}

	ASSERT_TRUE(sortedHeap.Size() == testData.size());

	sortedHeap.PrintItemsInLine();

	int prevKey = std::numeric_limits<int>::max(); 
	while(!sortedHeap.IsEmpty())
	{
		std::pair<int, std::string> item = sortedHeap.PeekMaxItem();
		sortedHeap.PopMaxItem();
		if(prevKey < item.first)
			int k = 0;
		ASSERT_TRUE(prevKey >= item.first);
		prevKey = item.first;
	}
}


TEST(SortedHeapTest1, TestRandom)
{
	std::cout << "cilcle:" << std::endl;
	for(size_t nt = 0; nt < 10; ++nt)
	{
		std::cout << nt << ", ";

		std::vector<int> testData;

		srand (time(NULL));
		const size_t numOfItems = 3000;
		for(size_t i = 0; i < numOfItems; ++i)
		{
			int number = rand() % 1000;
			testData.push_back(number);
		}

		SortedHeap<int, std::string> sortedHeap;
		for(std::vector<int> :: iterator i = testData.begin();
			i < testData.end();
			++i)
		{
			sortedHeap.Insert(*i, NumStrHelpers::ToString<char>(*i));
		}

		ASSERT_TRUE(sortedHeap.Size() == testData.size());

		std::vector<int> prioritizedData;
	
		int prevKey = std::numeric_limits<int>::max(); 
		while(!sortedHeap.IsEmpty())
		{
			std::pair<int, std::string> item = sortedHeap.PeekMaxItem();
			prioritizedData.push_back(item.first);
			sortedHeap.PopMaxItem();
			if(prevKey < item.first)
				std::for_each(prioritizedData.begin(), prioritizedData.end(), ContainerHelpers::PrintItemsInLine<int>);
			ASSERT_TRUE(prevKey >= item.first);
			prevKey = item.first;
		}
	}

	std::cout << "done" << std::endl;
	//std::for_each(prioritizedData.begin(), prioritizedData.end(), ContainerHelpers::PrintItemsInLine<int>);
}
