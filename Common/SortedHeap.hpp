#pragma once

#include <deque>
#include <map>
#include <algorithm>

#include "NumStrHelpers.hpp"
#include "../Common/ContainerHelpers.hpp"

using std::pair;
using std::multimap;
using std::deque;

template<typename Tk, typename Tv>
class SortedHeap
{
public:
	SortedHeap()
	{}

	void Insert(Tk key, Tv value);

	pair<Tk, Tv> PeekMaxItem() const;
	void PopMaxItem();

	void ChangePriority(Tv value, Tk newPriority);
	
	size_t Size() const
	{
		return _queue.size();
	}

	bool IsEmpty()
	{
		return _queue.empty();
	}

	pair<Tk, Tv> operator[](size_t position) const
	{
		return _queue[position];
	}

protected:
	//Diagnostic
	void PrintItemsInLine() const
	{
		std::for_each(_queue.begin(), _queue.end(), ContainerHelpers::PrintItemsInLine<Tk, Tv>);
	}
	bool	VerifyIndex() const;

private:
	std::deque<pair<Tk, Tv> > _queue;
	std::multimap<Tv, size_t> _index;

	size_t	GetParentIndex(size_t childIndex) const;
	size_t	IndexOfMaxChild(size_t indexChild1, size_t indexChild2) const;
	void	UpdateItemPositionInIndex(const Tv & value, size_t oldPozitionInQueue, size_t newPositionInQueue);
	void	DeleteItemFromIndex(const Tv & value, size_t pozitionInQueue);	

	void SwapElemnts(size_t index1, size_t index2)
	{
		pair<Tk, Tv> temp = _queue[index1];
		
		_queue[index1] = _queue[index2];
		UpdateItemPositionInIndex(_queue[index1].second, index2, index1);

		_queue[index2] = temp;		
		UpdateItemPositionInIndex(_queue[index2].second, index1, index2);
	}


};


// v Public methods

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::Insert(Tk key, Tv value)
{
	_queue.push_back(pair<Tk, Tv>(key, value));
	size_t i = _queue.size() - 1;
	_index.insert(pair<Tv, size_t>(value, i));
	size_t parent_i = 0;

	while(i > 0)
	{
		parent_i = GetParentIndex(i);
		if(_queue[parent_i].first < _queue[i].first)
		{
			SwapElemnts(parent_i, i);
			i = parent_i;
		}
		else
			break;
	}
}

template<typename Tk, typename Tv>
pair<Tk, Tv> SortedHeap<Tk, Tv>::PeekMaxItem() const
{
	return _queue.front();
}

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::PopMaxItem()
{
	// Remove item from index first (while item is still available)
	DeleteItemFromIndex(_queue[0].second, 0);
	_queue[0] = _queue[_queue.size() - 1];
	UpdateItemPositionInIndex(_queue[0].second, _queue.size() - 1, 0);
	_queue.pop_back();

	size_t i = 0;
	while(i < _queue.size())
	{
		size_t child_1 = 2*i + 1;
		size_t child_2 = child_1 + 1;
		
		size_t indexOfChildToCheck = 0;
		if((child_1 < _queue.size()) && (child_2 < _queue.size()))
		{
			indexOfChildToCheck = IndexOfMaxChild(child_1, child_2);
		}
		else if(child_1 < _queue.size())
		{
			indexOfChildToCheck = child_1;
		}
		else
		{
			return;
		}
		
		if(_queue[indexOfChildToCheck].first > _queue[i].first)
		{
			SwapElemnts(indexOfChildToCheck, i);
			i = indexOfChildToCheck;
		}
		else
			return;
	}
}

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::ChangePriority(Tv value, Tk newPriority)
{
}

// ^ Public methods

template<typename Tk, typename Tv>
size_t SortedHeap<Tk, Tv>::GetParentIndex(size_t childIndex) const
{
	assert(childIndex > 0);

	if(NumStrHelpers::IsOdd(childIndex))
		return childIndex/2;
	else
		return childIndex/2 -1;
}

template<typename Tk, typename Tv>
size_t SortedHeap<Tk, Tv>::IndexOfMaxChild(size_t indexChild1, size_t indexChild2) const
{
	if(_queue[indexChild1].first < _queue[indexChild2].first)
	{
		return indexChild2;
	}
	else
	{
		return indexChild1;
	}
}

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::UpdateItemPositionInIndex(const Tv & value, size_t oldPozitionInQueue, size_t newPositionInQueue)
{
	pair<multimap<Tv, size_t>::iterator, multimap<Tv, size_t>::iterator> rangeOfItems;
	rangeOfItems = _index.equal_range(value);
	for (multimap<Tv, size_t>::iterator it= rangeOfItems.first; it != rangeOfItems.second; ++it)
	{
		if(it->second == oldPozitionInQueue)
		{	
			it->second = newPositionInQueue;
			return;
		}
	}
	assert("SortedHeap<Tk, Tv>::UpdateItemPositionInIndex - Oops, no index?!"); 
}

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::DeleteItemFromIndex(const Tv & value, size_t pozitionInQueue)
{
	pair<multimap<Tv, size_t>::iterator, multimap<Tv, size_t>::iterator> rangeOfItems;
	rangeOfItems = _index.equal_range(value);
	for (multimap<Tv, size_t>::iterator it= rangeOfItems.first; it != rangeOfItems.second; ++it)
	{
		if(it->second == pozitionInQueue)
		{	
			_index.erase(it);
			return;
		}
	}
	assert("SortedHeap<Tk, Tv>::UpdateItemPositionInIndex - Oops, no index?!"); 
}

template<typename Tk, typename Tv>
bool SortedHeap<Tk, Tv>::VerifyIndex() const
{
	//clock_t duration, startTime = clock();
	
	if(_index.size() != _queue.size())
		return false;

	for(size_t i = 0; i < _queue.size(); ++i)
	{
		pair<multimap<Tv, size_t>::const_iterator, multimap<Tv, size_t>::const_iterator> rangeOfItems;
		rangeOfItems = _index.equal_range(_queue[i].second);

		bool itemIsFound = false;
		for (multimap<Tv, size_t>::const_iterator it= rangeOfItems.first; it != rangeOfItems.second; ++it)
		{
			if(it->second == i)
			{	
				itemIsFound = true;
				break;
			}
		}

		if(!itemIsFound)
		{
			//duration = clock() - startTime;
			//OutputDebugString((L"VerifyIndex took: " + NumStrHelpers::ToString<wchar_t>(duration) + L"\n").c_str());
			return false;
		}
	}

	//duration = clock() - startTime;
	//OutputDebugString((L"VerifyIndex took: " + NumStrHelpers::ToString<wchar_t>(duration) + L"\n").c_str());
	return true;
}

