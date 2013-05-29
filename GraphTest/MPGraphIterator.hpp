#pragma once

#include <iostream>
#include <queue>
#include <algorithm>
#include <memory>
#include <unordered_map>

#include "MPGraphNode.hpp"

using std::tr1::shared_ptr;

template<typename Tpayload, typename Tkey>
class MPGraphIterator
{
public:
	MPGraphIterator():
		_isLastNodeOnLevel(true),
		_currentNode(0)
	{}

	MPGraphIterator(MPGraphNode<Tpayload, Tkey> * beginNode):
		_isLastNodeOnLevel(true),
		_currentNode(beginNode)
	{
		_visitedNodes[_currentNode->ID()] = _currentNode;
		AddNodeNeighboursToToVisitCollection(_currentNode);

		_levelSeparator.reset(new MPGraphNode<Tpayload, Tkey>());
		_nodesToVisit.push(_levelSeparator.get());
	}

	MPGraphIterator & operator ++ ()
	{
		if(_nodesToVisit.empty())
			return *this;
		
		_currentNode = 0;
		_isLastNodeOnLevel = false;
		
		while(!_nodesToVisit.empty())
		{
			_currentNode = _nodesToVisit.front();

			if(_currentNode == _levelSeparator.get())
			{
				_nodesToVisit.pop();
				_currentNode = 0;
				continue;
			}

			if(_visitedNodes.find(_currentNode->ID()) == _visitedNodes.end())
				break;
			else
			{
				//std::cout << "Iterator node " << _currentNode->ID() << " has been already visited, skip" << std::endl;
				_nodesToVisit.pop();
				_currentNode = 0;
			}
		}

		if(_currentNode == 0)
			return *this;

		AddNodeNeighboursToToVisitCollection(_currentNode);
		_nodesToVisit.pop();

		MPGraphNode<Tpayload, Tkey> * nextNode = _nodesToVisit.front(); 
		if(nextNode == _levelSeparator.get())
		{
			_isLastNodeOnLevel = true;
			_nodesToVisit.pop();
			_nodesToVisit.push(_levelSeparator.get());
		}

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

	Tpayload operator *()
	{
		return _currentNode->GetPayload();
	}

	bool IsLastNodeOnLevel()
	{
		return _isLastNodeOnLevel;
	}

private:

	std::unordered_map<Tkey, const MPGraphNode<Tpayload, Tkey> *> _visitedNodes;
	std::queue<MPGraphNode<Tpayload, Tkey> *> _nodesToVisit;
	bool _isLastNodeOnLevel;
	shared_ptr<MPGraphNode<Tpayload, Tkey> > _levelSeparator;
	MPGraphNode<Tpayload, Tkey> * _currentNode;


	void AddNodeNeighboursToToVisitCollection(const MPGraphNode<Tpayload, Tkey> * node)
	{
		_visitedNodes[_currentNode->ID()] = node;

		std::list<MPGraphNode<Tpayload, Tkey> *> neighbours;
		node->GetConnections(neighbours);

		//std::cout << "Iterator current node " << _currentNode->ID() << " has connections: " << std::endl;
		std::for_each(
			neighbours.begin(), 
			neighbours.end(), 
			bind2nd(std::ptr_fun(MPGraphIterator::AddNodeToNodesToVisit), _nodesToVisit));
		std::cout << std::endl;
	}

	static void AddNodeToNodesToVisit(MPGraphNode<Tpayload, Tkey> * nodeToVisit, const std::queue<MPGraphNode<Tpayload, Tkey> *> & nodesToVisitCollection)
	{
		(const_cast<std::queue<MPGraphNode<Tpayload, Tkey> *> &>(nodesToVisitCollection)).push(nodeToVisit);
		//std::cout << nodeToVisit->ID() << ", ";
	}

};

