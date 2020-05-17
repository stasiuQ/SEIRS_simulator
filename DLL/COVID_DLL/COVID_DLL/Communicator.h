#pragma once
#include "simulation.h"
#include "debugger.h"
#include <string>
#include <vector>

class Communicator {
private:
	static Simulation* currentSimulation;
public:
	static std::vector<std::vector<double>> read(int internalInstructions, std::vector<double>& internalParameters);
	static void loadParameters(std::vector<double>& internalParameters);
};
