#pragma once

#define _USE_MATH_DEFINES

#include <cmath>
#include <vector>
#include <fstream>
#include "agent.h"
#include "home.h"

using namespace std;

class Simulation {
private:
	int size;   // size of a system matrix
	double concentration;
	int numberOfAgents;
	int initialInfected;
	double zoneSize;
	double maxHomeRadius;
	double minHomeRadius;

	vector<Agent> agents;
	vector<Home> homes;
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

	void wearMasks();
	void initializeCouriers();
	void initializeHomes();

	void simulate(int numberOfSteps);
	void simulateWithHomes(int numberOfSteps);
	bool detectContact(Agent* a1, Agent* a2);
	bool detectHome(double i, double j);
	void updateStatistics();
	void printStatistics();
	vector<vector<double>> outputInterface();

	int ijTo_k(int i, int j);
};