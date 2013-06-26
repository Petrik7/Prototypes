#pragma once

#include <deque>
#include <algorithm>

#include "NumStrHelpers.hpp"
#include "../Common/ContainerHelpers.hpp"

using std::pair;

template<typename Tk, typename Tv>
class SortedHeap
{
public:
	SortedHeap()
	{}

	void Insert(Tk key, Tv value);

	pair<Tk, Tv> PeekMaxItem() const;
	void PopMaxItem();

	pair<Tk, Tv> PeekMinItem() const;
	void PopMinItem();

	void ChangePriority(Tv value, Tk newPriority);
	
	size_t Size() const
	{
		return _queue.size();
	}

	pair<Tk, Tv> operator[](size_t position) const
	{
		return _queue[position];
	}

	//Diagnostic
	void PrintItemsInLine() const
	{
		std::for_each(_queue.begin(), _queue.end(), ContainerHelpers::PrintItemsInLine<Tk, Tv>);
	}

private:
	std::deque<pair<Tk, Tv> > _queue;

	size_t GetParentIndex(size_t childIndex) const;
	void SwapElemnts(size_t index1, size_t index2)
	{
		pair<Tk, Tv> temp = _queue[index1];
		_queue[index1] = _queue[index2];
		_queue[index2] = temp;
	}
};


// v Public methods

template<typename Tk, typename Tv>
void SortedHeap<Tk, Tv>::Insert(Tk key, Tv value)
{
	_queue.push_back(pair<Tk, Tv>(key, value));
	size_t i = _queue.size() - 1;
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

}

template<typename Tk, typename Tv>
pair<Tk, Tv> SortedHeap<Tk, Tv>::PeekMinItem() const
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
