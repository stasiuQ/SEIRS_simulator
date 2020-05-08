#pragma once

#define _USE_MATH_DEFINES

#include <cmath>
#include <vector>
#include <fstream>
#include "agent.h"

using namespace std;

class Simulation {
private:
	int size;   // size of a system matrix
	double concentration;
	int numberOfAgents;
	int initialInfected;
	vector<Agent> agents;
	vector<double> simulationParameters;
	fstream outputFile;

	// Some statistical parameters
	int step = 0;
	int no_S;
	int no_E;
	int no_I;
	int no_R;


public:
	Simulation(double concentration, int m_size, int numberOfInfected);
	~Simulation();

	void simulate(int numberOfSteps);
	bool detectContact(Agent* a1, Agent* a2);
	void updateStatistics();
	void printStatistics();
};