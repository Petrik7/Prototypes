#pragma once

#include <list>

template<typename Tnode, typename Tkey>
class MPGraphNode
{
public:
	MPGraphNode(const Tnode & payload, const Tkey & id):
	  _payload(payload),
	  _id(id)
	{}

	~MPGraphNode(void){};

	Tnode GetPayload()
	{
		return _payload;
	}

	void GetPayload(Tnode * result) const
	{
		 *result = _payload;
	}

	void AddConnection(MPGraphNode<Tnode, Tkey> * nodeToConnect)
	{
		_connections.push_back(nodeToConnect);
	}

	void GetConnections(std::list<MPGraphNode<Tnode, Tkey> * > & connections) const
	{
		connections.assign(_connections.begin(), _connections.end());
	}

	Tkey ID()
	{
		return _id;
	}

private:
	Tnode _payload;
	Tkey  _id;

	std::list<MPGraphNode<Tnode, Tkey> * > _connections;
};
