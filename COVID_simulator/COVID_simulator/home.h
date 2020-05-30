#pragma once

#include "agent.h"

using namespace std;

class Home
{
private:
	double i;				/// Home's i coordinate;
	double j;				/// Home's j cooridnate;
	double radius;			/// Home's radius.
	double blockTime;		/// How many time steps has to pass before a new agent can enter the home,
							/// in case home is not full.
	double elapsedTime;		/// Time elapsed after it is possible to enter for a new agent to home.
	int maxCapacity;		/// How many agents can be in the same home at the same time.
	double capacity;
	double mobilityModifier;

	bool canAgentEnter(double dt);

	/// Getters
	double get_i();
	double get_j();
	double get_mobilityModifier();

	void update(Agent * agent);
public:
	Home();
	Home(double i, double j, double radius, double blockTime = 5., int maxCapacity = 5, double mobilityModifier = 0.1); /// TBD if these parameters should be in the init file.
};