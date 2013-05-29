#pragma once

#include <string>
#include <fstream>
#include <assert.h>

#include "MPGraph.hpp"
#include "User.hpp"
#include "GraphBuilder.hpp"

using std::string;
using std::ifstream;

class UserStringTest
{
public:
	UserStringTest(void);
	~UserStringTest(void);

	static void Run()
	{
		MPGraph<User<string>, string> userGraph;

		string graph_description;

		ifstream graph_file ("graph.txt");
		if (graph_file.is_open())
		{
			while ( graph_file.good() )
			{
				getline (graph_file, graph_description);
				GraphBuilder<User<string>, string>::Build(graph_description, userGraph);
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

