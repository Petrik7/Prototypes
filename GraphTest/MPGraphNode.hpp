#pragma once

#include <list>
#include <memory>

using std::tr1::shared_ptr;

template<typename Tnode>
class MPGraphNode
{
public:
	MPGraphNode(const Tnode & payload):_payload(payload)
	{}

	~MPGraphNode(void){};

	Tnode GetPayload()
	{
		return _payload;
	}

	void GetPayload(Tnode * result)
	{
		 *result = _payload;
	}

	void AddConnection(MPGraphNode<Tnode> * nodeToConnect)
	{
		_connections.push_back(nodeToConnect);
	}

private:
	Tnode _payload;

	//std::list<shared_ptr<MPGraphNode<Tnode> > > _connections;
	std::list<MPGraphNode<Tnode> * > _connections;
};
