#pragma once

#include "MPGraph.hpp"
#include "User.hpp"

class UserIntTest
{
public:
	UserIntTest(void);
	~UserIntTest(void);

	static void Run()
	{
		int userId = 0;

		MPGraph<User<int>, int> userGraph;
	
		{
			User<int> user_1(++userId);
			bool expect_success1 = userGraph.AddNode(user_1, user_1.GetId());
			bool expect_fail = userGraph.AddNode(user_1, user_1.GetId());

			User<int> user_2(++userId);
			bool expect_success2 = userGraph.AddNode(user_2, user_2.GetId());
		}

		bool link_cussess = userGraph.LinkNodes(userId, userId - 1);

		{
			User<int> user_3(++userId);
			User<int> user_4(++userId);
			bool expect_success2 = userGraph.AddAndLinkNodes(user_3, user_3.GetId(), user_4, user_4.GetId());
		}

		{
			//User gotUser;
			//bool expect_success = userGraph.GetNode(userId, &gotUser);
			//std::cout << gotUser.GetId() << std::endl;
		}
	}


};

