#include "stdafx.h"

#include "gtest.h"

#include <string>
//#include <algorithm>

#include <vector> // tmp test

#include "../Common/SortedHeap.hpp"


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
		ASSERT_TRUE(sortedHeap.KeyAt(i) == expectedOrder[i]);
	}
}

