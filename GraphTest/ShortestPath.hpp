#pragma once

#include <list>
#include <queue>
#include <set>
#include <limits>

#include "MPGraphNode.hpp"
#include "MPGraph.hpp"
#include "..\Common\SortedHeap.hpp"

using std::ptr_fun;
using std::bind2nd;
using namespace Heap;

#undef max

class ShortestPath
{
public:

	template<typename Tpayload, typename Tkey>
	static bool Dijkstra(const MPGraph<Tpayload, Tkey> & graph, const Tkey & nodeFrom, const Tkey & nodeTo, std::list<Tpayload> & path, int & pathWeight)
	{

		DijkstraContext<Tpayload, Tkey> dijkstraContext;
		
		dijkstraContext._nodeToID = nodeTo;
		MPGraphNode<Tpayload, Tkey> * srcNode = graph.GetNode(nodeFrom);
		MPGraphNode<Tpayload, Tkey> * destNode = graph.GetNode(nodeTo);
		dijkstraContext._unvisitedNodes.Insert(0, graph.GetNode(nodeFrom));

		std::list<const MPGraphNode<Tpayload, Tkey> * >  nodeConnections;
		while(!dijkstraContext._unvisitedNodes.IsEmpty())
		{
			std::pair<int, const MPGraphNode<Tpayload, Tkey>* > unvisitedNode = dijkstraContext._unvisitedNodes.Top(); // we need to keep entire node, not just ID
			if(unvisitedNode.second->ID() == nodeTo)
			{
				dijkstraContext._unvisitedNodes.Pop();			
				continue;
			}
			
			dijkstraContext._pathWaightToCurrentNode = unvisitedNode.first;
			dijkstraContext._currentNode = unvisitedNode.second;
			dijkstraContext._unvisitedNodes.Pop();			
			dijkstraContext.AddVisitedNode(dijkstraContext._currentNode, dijkstraContext._pathWaightToCurrentNode);

			nodeConnections.clear();
			dijkstraContext._currentNode->GetConnections(nodeConnections);

			std::for_each(
				nodeConnections.begin(), 
				nodeConnections.end(), 
				bind2nd(ptr_fun(UpdatePathWeightAddToUnvisitedNodes<Tpayload, Tkey>), dijkstraContext));
	
		}

		if(dijkstraContext._minWeightPath != std::numeric_limits<int>::max())
		{
			//Trace back to find path
			pathWeight = dijkstraContext._minWeightPath;
			return dijkstraContext.TraceBackToGetPath(srcNode, destNode, path);
		}

		return false;
	}

private:

	template<typename Tpayload, typename Tkey>
	struct DijkstraContext
	{	
		DijkstraContext():
			_unvisitedNodes(Heap::MinHeap)
			,_minWeightPath(std::numeric_limits<int>::max())
		{}
		
		void AddVisitedNode(const MPGraphNode<Tpayload, Tkey> * visitedNode, int weight)
		{
			_visitedNodes.insert(pair<const MPGraphNode<Tpayload, Tkey> *, int >(visitedNode, weight));
		}

		bool IsNodeVisited(const MPGraphNode<Tpayload, Tkey> * nodeToCheck) const
		{
			return _visitedNodes.find(nodeToCheck) != _visitedNodes.end();
		}

		bool TraceBackToGetPath(
			MPGraphNode<Tpayload, Tkey> * sourceNode, 
			MPGraphNode<Tpayload, Tkey> * destinationNode, 
			std::list<Tpayload> & resultPath)
		{
			const MPGraphNode<Tpayload, Tkey> * node = destinationNode;

			while(node)
			{
				resultPath.push_back(node->GetPayload());

				const MPGraphNode<Tpayload, Tkey> * nextNode = 0;
				int smallestWeight = std::numeric_limits<int>::max();
				std::list<const MPGraphNode<Tpayload, Tkey> * > connections;
				node->GetConnections(connections);
				for(std::list<const MPGraphNode<Tpayload, Tkey> * > :: iterator i = connections.begin();
					i != connections.end();
					++i)
				{
					std::map<const MPGraphNode<Tpayload, Tkey> *, int > :: iterator connectionNode = _visitedNodes.find(*i);
					if(connectionNode != _visitedNodes.end())
					{
						if(connectionNode->first == sourceNode)
						{
							resultPath.push_back(sourceNode->GetPayload());
							return true;
						}

						ConnectionProperties nodesConnectionProperies = node->PropertiesOfConnectionWithNode(connectionNode->first);
						int completeWeight = nodesConnectionProperies.Weight + connectionNode->second;
						if(completeWeight < smallestWeight)
						{
							smallestWeight = completeWeight ;
							nextNode = connectionNode->first;
						}
					}
				}
				node = nextNode;			
			}

			return false;
		}

		Tkey _nodeToID;

		std::map<const MPGraphNode<Tpayload, Tkey> *, int > _visitedNodes;
		Heap::SortedHeap<int, const MPGraphNode<Tpayload, Tkey> * > _unvisitedNodes;
		const MPGraphNode<Tpayload, Tkey> * _currentNode;
		int _pathWaightToCurrentNode;
		int _minWeightPath;
	};

	template<typename Tpayload, typename Tkey>
	static void UpdatePathWeightAddToUnvisitedNodes(const MPGraphNode<Tpayload, Tkey> * neighbourOfCurrentNode, const DijkstraContext<Tpayload, Tkey> & constDijkstraContext)
	{
		if(constDijkstraContext.IsNodeVisited(neighbourOfCurrentNode))
			return;

		ConnectionProperties neighbourToCurrentConnection =  neighbourOfCurrentNode->PropertiesOfConnectionWithNode(constDijkstraContext._currentNode);
		int fullPathWeightToNeighbour = constDijkstraContext._pathWaightToCurrentNode + neighbourToCurrentConnection.Weight;
		DijkstraContext<Tpayload, Tkey> & dijkstraContext = const_cast<DijkstraContext<Tpayload, Tkey>& >(constDijkstraContext);

		if(neighbourOfCurrentNode->ID() == constDijkstraContext._nodeToID)
		{
			if(dijkstraContext._minWeightPath > fullPathWeightToNeighbour)
				dijkstraContext._minWeightPath = fullPathWeightToNeighbour;			
			return;
		}

		if(dijkstraContext._unvisitedNodes.Contains(neighbourOfCurrentNode))
		{
			if(dijkstraContext._unvisitedNodes.KeyByValue(neighbourOfCurrentNode) > fullPathWeightToNeighbour)
			{
				dijkstraContext._unvisitedNodes.ChangePriority(neighbourOfCurrentNode, fullPathWeightToNeighbour);
			}
		}
		else
		{
			dijkstraContext._unvisitedNodes.Insert(fullPathWeightToNeighbour, neighbourOfCurrentNode);
		}
	}

};
