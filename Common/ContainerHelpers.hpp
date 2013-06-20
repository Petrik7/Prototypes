#pragma once

#include <list>
#include <iostream>

namespace ContainerHelpers
{
	//list is not const - const is required by bind2nd
	template<typename Tk, typename Tv>
	void AddKeyOfPairToList(std::pair<Tk, Tv> keyValuePair, const std::list<Tk> & list)
	{
		(const_cast<std::list<Tk> &>(list)).push_back(keyValuePair.first);
	}

	//std::for_each(collection.begin(), collection.end(), ContainerHelpers::PrintItemsInLine<ItemType>);
	template<typename T>
	void PrintItemsInLine(const T & item)
	{
		std::cout << item << ", ";
	}
};


