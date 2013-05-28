#pragma once

#include <list>

template<typename Tpayload, typename Tkey>
class MPGraphNode
{
public:
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
		_connections.push_back(nodeToConnect);
	}

	void GetConnections(std::list<MPGraphNode<Tpayload, Tkey> * > & connections) const
	{
		connections.assign(_connections.begin(), _connections.end());
	}

	Tkey ID()
	{
		return _id;
	}

private:
	Tpayload _payload;
	Tkey  _id;

	std::list<MPGraphNode<Tpayload, Tkey> * > _connections;
};
