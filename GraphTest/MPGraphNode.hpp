#pragma once

#include <map>
#include <algorithm>
#include "ContainerHelpers.hpp"

class ConnectionProperties
{
public:

	ConnectionProperties():
	  Wight(0)
	{
	}

	ConnectionProperties(int weight):
	  Wight(weight)
	{
	}

	int Wight;

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

	Tpayload GetPayload()
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

	void GetConnections(std::list<MPGraphNode<Tpayload, Tkey> * > & connections) const
	{
		std::for_each(
			_connections.begin(), 
			_connections.end(), 
			std::bind2nd(std::ptr_fun(ContainerHelpers::AddKeyOfPairToList<Tnode *, ConnectionProperties>), connections));
	}

	Tkey ID()
	{
		return _id;
	}

private:
	Tpayload _payload;
	Tkey  _id;

	std::map<Tnode *, ConnectionProperties> _connections;
};
