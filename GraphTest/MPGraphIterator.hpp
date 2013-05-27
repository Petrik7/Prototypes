#pragma once

#include <iostream>
#include <queue>
#include <algorithm>
#include <unordered_map>

#include "MPGraphNode.hpp"

template<typename Tnode, typename Tkey>
class MPGraphIterator
{
public:
	MPGraphIterator(){};

	MPGraphIterator(MPGraphNode<Tnode, Tkey> * beginNode)
	{
		_nodesToVisit.push(beginNode);
	}

	MPGraphIterator & operator ++ ()
	{
		if(_nodesToVisit.empty())
			return *this;
		
		MPGraphNode<Tnode, Tkey> * currentNode = 0;
		
		while(!_nodesToVisit.empty())
		{
			currentNode = _nodesToVisit.front();;

			if(_visitedNodes.find(currentNode->ID()) == _visitedNodes.end())
				break;
			else
			{
				std::cout << "Iterator node " << currentNode->ID() << " has been already visited, skip" << std::endl;
				_nodesToVisit.pop();
				currentNode = 0;
			}
		}

		if(currentNode == 0)
			return *this;

		std::list<MPGraphNode<Tnode, Tkey> *> neighbours;
		currentNode->GetConnections(neighbours);
		
		_visitedNodes[currentNode->ID()] = currentNode;

		std::cout << "Iterator current node " << currentNode->ID() << " has connections: " << std::endl;
		std::for_each(
			neighbours.begin(), 
			neighbours.end(), 
			bind2nd(std::ptr_fun(MPGraphIterator::AddNodeToNodesToVisit), _nodesToVisit));
		std::cout << std::endl;
			
		_nodesToVisit.pop();
		return *this;
	}

	bool operator !=(const MPGraphIterator & otherIter) const
	{
		return !(this->operator==(otherIter));
	}

	bool operator ==(const MPGraphIterator & otherIter) const
	{
		if(_nodesToVisit.empty() && otherIter._nodesToVisit.empty())
			return true;

		if(_nodesToVisit.empty() && !otherIter._nodesToVisit.empty())
			return false;

		if(!_nodesToVisit.empty() && otherIter._nodesToVisit.empty())
			return false;

		return _nodesToVisit.front() == otherIter._nodesToVisit.front();
	}

private:

	std::unordered_map<Tkey, MPGraphNode<Tnode, Tkey> *> _visitedNodes;
	std::queue<MPGraphNode<Tnode, Tkey> *> _nodesToVisit;

	static void AddNodeToNodesToVisit(MPGraphNode<Tnode, Tkey> * nodeToVisit, const std::queue<MPGraphNode<Tnode, Tkey> *> & nodesToVisitCollection)
	{
		(const_cast<std::queue<MPGraphNode<Tnode, Tkey> *> &>(nodesToVisitCollection)).push(nodeToVisit);
		std::cout << nodeToVisit->ID() << ", ";
	}

};

