#pragma once

#include "agent.h"

using namespace std;

class Home
{
private:
	double i;				/// Home's i coordinate;
	double j;				/// Home's j cooridnate;
	double radius;			/// Home's radius.
	double minHomeDistance;	/// Minimum distance between homes.
	double blockTime;		/// How many time steps has to pass before a new agent can enter the home,
							/// in case home is not full.
	double elapsedTime;		/// Time elapsed after it is possible to enter for a new agent to home.
	int maxCapacity;		/// How many agents can be in the same home at the same time.
	double capacity;
	double mobilityModifier;

	Home(double i, double j, double radius, double minHomeDistance, double blockTime, double maxCapacity, double mobilityModifier);
	bool isHomeHere(Home *home);	/// Quite inefficient. It would be nice to create a 2D bool array with blocked / free zones.
	bool canAgentEnter(double dt);

	/// Getters
	double get_i();
	double get_j();
	double get_mobilityModifier();

	void update(Agent * agent);
};