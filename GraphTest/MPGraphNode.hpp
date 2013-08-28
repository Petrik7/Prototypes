#pragma once

#include <map>
#include <algorithm>
#include "../Common/ContainerHelpers.hpp"

class ConnectionProperties
{
public:

	ConnectionProperties():
	  Weight(0)
	{
	}

	ConnectionProperties(int weight):
	  Weight(weight)
	{
	}

	int Weight;

};

template<typename Tpayload, typename Tkey>
class MPGraphNode
{
public:

	typedef MPGraphNode<Tpayload, Tkey> Tnode;

	MPGraphNode(){};

	MPGraphNode(const Tpayload & payload, const Tkey & id):
	  _payload(payload),
	  _id(id)
	{}

	~MPGraphNode(void){};

	Tpayload GetPayload() const
	{
		return _payload;
	}

	void GetPayload(Tpayload * result) const
	{
		 *result = _payload;
	}

	void AddConnection(MPGraphNode<Tpayload, Tkey> * nodeToConnect)
	{
		_connections[nodeToConnect] = ConnectionProperties();
	}

	void AddConnection(MPGraphNode<Tpayload, Tkey> * nodeToConnect, int weight)
	{
		_connections[nodeToConnect] = ConnectionProperties(weight);
	}

	void GetConnections(std::list<const MPGraphNode<Tpayload, Tkey> * > & connections) const
	{
		std::for_each(
			_connections.begin(), 
			_connections.end(), 
			std::bind2nd(std::ptr_fun(ContainerHelpers::AddKeyOfPairToList<const Tnode *, ConnectionProperties>), connections));
	}

	Tkey ID() const
	{
		return _id;
	}

	ConnectionProperties PropertiesOfConnectionWithNode(const Tnode * node) const
	{
		return _connections.at(node); 
	}

private:
	Tpayload _payload;
	Tkey  _id;

	std::map<const Tnode *, ConnectionProperties> _connections;
};
