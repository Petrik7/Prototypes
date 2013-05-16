#pragma once

#include <string>

#include "..\Common\KeyValueString.hpp"
#include "..\Common\NumStrHelpers.hpp"

#include "MPGraph.hpp"

using std::string;


template<typename Tn, typename Tk>
class GraphBuilder
{
public:
	GraphBuilder(void);
	~GraphBuilder(void);

	static bool Build(const string & graphDescription, MPGraph<Tn, Tk> & graph)
	{
		KeyValueString<char> nodes_parcer(',', ' ');
		nodes_parcer.AddRawKeyValuesString(graphDescription);

		string nodeID1("");
		string nodeID2("");
		KeyValueString<char> :: Iterator nodeI = nodes_parcer.Begin();
		while(nodeI != nodes_parcer.End())
		{
			string nodesPair = *nodeI;
			
			if(nodes_parcer.GetKeyValueFromPair(nodesPair, nodeID1, nodeID2))
			{
				User<string> user_1(nodeID1);
				User<string> user_2(nodeID2);
				std::cout << "Added node pair: " << nodeID1 << " : " << nodeID2 << std::endl;
				graph.AddAndLinkNodes(user_1, user_1.GetId(), user_2, user_2.GetId());
			}
			++nodeI;
		}

		return true;
	}
};

