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

	}
};

