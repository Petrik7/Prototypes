#pragma once

#include <map>
#include <memory>
#include <unordered_map>

#include "MPGraphNode.hpp"
#include "MPGraphIterator.hpp"

using std::tr1::shared_ptr;

	template<typename Tn, typename Tk>
	class MPGraphIterator;

template<typename Tnode, typename Tkey>
class MPGraph
{
public:
	MPGraph(void);
	~MPGraph(void);

	bool AddNode(const Tnode & node, const Tkey id);
	bool AddAndLinkNodes(Tnode node1, Tkey id1, Tnode node2, Tkey id2);
	bool LinkNodes(Tkey idNode1, Tkey idNode2);

	bool GetNode(const Tkey id, Tnode * result);
	void GetNodeConnections(const Tkey nodeID, std::list<Tnode> & connections);



	typedef MPGraphIterator<Tnode, Tkey> Iterator;

	typename Iterator Begin();
	typename Iterator End();

private:
	std::map<Tkey, shared_ptr<MPGraphNode<Tnode, Tkey> > > _nodes;

};

template<typename Tnode, typename Tkey>
MPGraph<Tnode, Tkey>::MPGraph(void)
{
}


template<typename Tnode, typename Tkey>
MPGraph<Tnode, Tkey>::~MPGraph(void)
{
}


template<typename Tnode, typename Tkey>
bool MPGraph<Tnode, Tkey>::AddNode(const Tnode & node, const Tkey id)
{
	std::map<Tkey, shared_ptr<MPGraphNode<Tnode, Tkey> > > :: iterator item = _nodes.find(id);
	if(item != _nodes.end())
		return false;

	//shared_ptr<MPGraphNode<Tnode, Tkey> > node_ptr(new MPGraphNode<Tnode, Tkey>(node));
	_nodes[id] = shared_ptr<MPGraphNode<Tnode, Tkey> > (new MPGraphNode<Tnode, Tkey>(node, id));

	return true;
}

template<typename Tnode, typename Tkey>
bool MPGraph<Tnode, Tkey>::AddAndLinkNodes(Tnode node1, Tkey id1, Tnode node2, Tkey id2)
{
	AddNode(node1, id1);
	AddNode(node2, id2);
	if(LinkNodes(id1, id2))
		return true;
	return false;	
}

template<typename Tnode, typename Tkey>
bool MPGraph<Tnode, Tkey>::LinkNodes(Tkey idNode1, Tkey idNode2)
{
	std::map<Tkey, shared_ptr<MPGraphNode<Tnode, Tkey> > > :: iterator item1 = _nodes.find(idNode1);
	if(item1 == _nodes.end())
		return false;

	std::map<Tkey, shared_ptr<MPGraphNode<Tnode, Tkey> > > :: iterator item2 = _nodes.find(idNode2);
	if(item2 == _nodes.end())
		return false;

	(*item1).second->AddConnection((*item2).second.get());
	(*item2).second->AddConnection((*item1).second.get());

	return true;
}

template<typename Tnode, typename Tkey>
bool MPGraph<Tnode, Tkey>::GetNode(const Tkey id, Tnode * result)
{
	std::map<Tkey, shared_ptr<MPGraphNode<Tnode, Tkey> > > :: iterator item = _nodes.find(id);
	if(item == _nodes.end())
		return false;

	(*item).second->GetPayload(result);
	return true;
}

template<typename Tnode, typename Tkey>
void MPGraph<Tnode, Tkey>::GetNodeConnections(const Tkey nodeID, std::list<Tnode> & connections)
{

}

template<typename Tnode, typename Tkey>
typename MPGraphIterator<Tnode, Tkey> MPGraph<Tnode, Tkey>::Begin()
{
	if(_nodes.empty())
		return Iterator();

	shared_ptr<MPGraphNode<Tnode, Tkey> > firstNode = _nodes.begin()->second;
	Iterator gi(firstNode.get());
	return gi;
}

template<typename Tnode, typename Tkey>
typename MPGraphIterator<Tnode, Tkey> MPGraph<Tnode, Tkey>::End()
{
	Iterator gi;
	return gi;
}