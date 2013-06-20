#pragma once

#include <deque>
#include <algorithm>

#include "NumStrHelpers.hpp"
#include "../Common/ContainerHelpers.hpp"

template<typename Tk, typename Tv>
class SortedHeap
{
public:
	SortedHeap()
	{}

	void Insert(Tk key, Tv value);

	Tv PeekMaxItem();
	void PopMaxItem();

	Tv PeekMinItem();
	void PopMinItem();

	void ChangePriority(Tv value, Tk newPriority);
	
	size_t Size() const
	{
		return _queue.size();
	}

	Tk KeyAt(size_t position) const
	{
		return _queue[position];
	}

	//Diagnostic
	void PrintItemsInLine() const
	{
		std::for_each(_queue.begin(), _queue.end(), ContainerHelpers::PrintItemsInLine<Tk>);
	}

private:
	std::deque<Tk> _queue;

	size_t GetParentIndex(size_t childIndex) const;
	void SwapElemnts(size_t index1, size_t index2)
	{
		Tk temp = _queue[index1];
		_queue[index1] = _queue[index2];
		_queue[index2] = temp;
	}
};

// v Public methods

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::Insert(Tk key, Tv value)
{
	_queue.push_back(key);
	size_t i = _queue.size() - 1;
	size_t parent_i = 0;

	while(i > 0)
	{
		parent_i = GetParentIndex(i);
		if(_queue[parent_i] < _queue[i])
		{
			SwapElemnts(parent_i, i);
			i = parent_i;
		}
		else
			break;
	}
}

template<typename Tk, typename Tv>
Tv SortedHeap<Tk, Tv>::PeekMaxItem()
{
}

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::PopMaxItem()
{
}

template<typename Tk, typename Tv>
Tv SortedHeap<Tk, Tv>::PeekMinItem()
{
}

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::PopMinItem()
{
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
