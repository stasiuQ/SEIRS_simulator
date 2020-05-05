#pragma once

#include <vector>
#include "agent.h"

using namespace std;

class Simulation {
private:
	int size;   // size of a system matrix
	int numberOfAgents;
	int initialInfected;
	vector<Agent> agents;
	vector<double> simulationParameters;

public:
	Simulation(double concentration, int m_size, int numberOfInfected);
	~Simulation();

	bool detectContact(Agent* a1, Agent* a2);
};