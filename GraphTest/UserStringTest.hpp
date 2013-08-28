#pragma once

#include <string>
#include <fstream>
#include <assert.h>

#include "MPGraph.hpp"
#include "ShortestPath.hpp"
#include "User.hpp"
#include "GraphBuilder.hpp"

using std::string;
using std::ifstream;

size_t User<string>::_created = 0;
size_t User<string>::_deleted = 0;

class UserStringTest
{
public:
	UserStringTest(void);
	~UserStringTest(void);

	static void Run()
	{
	{
		MPGraph<User<string>, string> userGraph;

		string graph_description;

		//ifstream graph_file ("graph.txt");
		ifstream graph_file ("graph_weight.txt");
		if (graph_file.is_open())
		{
			while ( graph_file.good() )
			{
				getline (graph_file, graph_description);
				//GraphBuilder<User<string>, string>::Build(graph_description, userGraph);
				GraphBuilder<User<string>, string>::BuildWithWeight(graph_description, userGraph);
			}
			graph_file.close();
		}

		MPGraph<User<string>, string> :: Iterator nodeIter = userGraph.Begin();
		MPGraph<User<string>, string> :: Iterator end = userGraph.End();

		while(nodeIter != end)
		{
			nodeIter++;
		}

		User<string> user;
		assert(userGraph.GetItem("E", &user));
		assert(!userGraph.GetItem("[", &user));

		{
			size_t numOfLevels = 3;
			std::vector<std::list<User<string> > > firstNconnections;
			firstNconnections.reserve(numOfLevels);
		
			userGraph.GetNodeConnectionsFirstNLevels("A", firstNconnections, numOfLevels);
			PrintLayers(firstNconnections);
		}

		{
			size_t numOfLevels = 4;
			std::vector<std::list<User<string> > > firstNconnections;
			firstNconnections.reserve(numOfLevels);
		
			userGraph.GetNodeConnectionsFirstNLevels("B", firstNconnections, numOfLevels);
			PrintLayers(firstNconnections);
		}

		{	
			size_t numOfLevels = 4;
			std::vector<std::list<User<string> > > firstNconnections;
			firstNconnections.reserve(numOfLevels);
		
			userGraph.GetNodeConnectionsFirstNLevels("D", firstNconnections, numOfLevels);
			PrintLayers(firstNconnections);
		}

		{
			string fromNode = "A";
			string toNode = "E";
			std::list<User<string> > AtoEpath;

			int AtoEWeight = 0;
			if(ShortestPath::Dijkstra(userGraph, fromNode, toNode, AtoEpath, AtoEWeight))
			{
				std::cout << "Path from " << fromNode << " to " << toNode << std::endl;
				std::for_each(AtoEpath.begin(), AtoEpath.end(), ContainerHelpers::PrintItemsInLine<User<string> >);
				std::cout << std::endl;
			}
			else
			{
				std::cout << "No Path from " << fromNode << " to " << toNode << std::endl;
			}
		}
	}

	assert(User<string>::_created == User<string>::_deleted);

	std::cout << "_created  : " << User<string>::_created << std::endl;
	std::cout << "_deleted  : " << User<string>::_deleted << std::endl;

	}

	static void PrintLayers(const std::vector<std::list<User<string> > > & connectionsByLayers)
	{
		size_t level = 0;
		for(std::vector<std::list<User<string> > > :: const_iterator levelI = connectionsByLayers.begin();
			levelI != connectionsByLayers.end();
			++levelI)
		{
			std::cout << "Level " << level++ << std::endl;
			for(std::list<User<string> > :: const_iterator userI = (*levelI).begin();
				userI != (*levelI).end();
				++userI)
			{
				std::cout << (*userI).GetId() << ", ";			
			}

			std::cout << std::endl;
		}
	
	}
};

