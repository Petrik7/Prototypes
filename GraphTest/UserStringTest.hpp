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

		size_t numOfLevels = 3;
		std::vector<std::list<User<string> > > firstNconnections;
		firstNconnections.reserve(numOfLevels);
		
		userGraph.GetNodeConnectionsFirstNLevels("A", firstNconnections, numOfLevels);
		
		size_t level = 0;
		for(std::vector<std::list<User<string> > > :: iterator levelI = firstNconnections.begin();
			levelI != firstNconnections.end();
			++levelI)
		{
			std::cout << "Level " << level++ << std::endl;
			for(std::list<User<string> > :: iterator userI = (*levelI).begin();
				userI != (*levelI).end();
				++userI)
			{
				std::cout << (*userI).GetId() << ", ";			
			}

			std::cout << std::endl;
		}
	}
};

