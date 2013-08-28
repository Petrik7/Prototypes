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


	typedef enum ParcerState {Node_1, Node_2, Weight} TParcerState;
	static bool BuildWithWeight(const string & graphDescription, MPGraph<Tn, Tk> & graph)
	{
		TParcerState parcerState = Node_1;
		string item;
		string node_1_id;
		string node_2_id;
		string strWeight;
		for(size_t i = 0; i < graphDescription.length(); ++i)
		{
			switch(parcerState)
			{
			case Node_1:
				if(graphDescription[i] == ',')
				{
					parcerState = Node_2;
				}
				else
				{
					node_1_id += graphDescription[i];
				}
				break;
			case Node_2:
				if(graphDescription[i] == ',')
				{
					parcerState = Weight;
				}
				else if(graphDescription[i] == ' ')
				{
					parcerState = Node_1;
					User<string> user_1(node_1_id);
					User<string> user_2(node_2_id);
					std::cout << "Added node pair: " << node_1_id << " : " << node_2_id << std::endl;
					graph.AddAndLinkNodes(user_1, user_1.GetId(), user_2, user_2.GetId());
					node_1_id.clear();
					node_2_id.clear();
				}
				else
				{
					node_2_id += graphDescription[i];
				}
				break;
			case Weight:
				if(graphDescription[i] == ' ')
				{
					parcerState = Node_1;
					User<string> user_1(node_1_id);
					User<string> user_2(node_2_id);
					std::cout << "Added node pair: " << node_1_id << " : " << node_2_id << " w=" << strWeight <<std::endl;
					int weight = 0;
					NumStrHelpers::TryStringToInt(strWeight, &weight);
					graph.AddAndLinkNodes(user_1, user_1.GetId(), user_2, user_2.GetId(), weight);
					node_1_id.clear();
					node_2_id.clear();
					strWeight.clear();
				}
				else
				{
					strWeight += graphDescription[i];
				}
				break;
			
			}		
		}

		User<string> user_1(node_1_id);
		User<string> user_2(node_2_id);
		std::cout << "Added node pair: " << node_1_id << " : " << node_2_id << " w=" << strWeight <<std::endl;
		int weight = 0;
		NumStrHelpers::TryStringToInt(strWeight, &weight);
		graph.AddAndLinkNodes(user_1, user_1.GetId(), user_2, user_2.GetId(), weight);
		return true;
	}

};

