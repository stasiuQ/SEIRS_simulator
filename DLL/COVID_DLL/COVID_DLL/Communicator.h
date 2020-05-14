#pragma once
#include "simulation.h"
#include <string>
#include <vector>

class Communicator {
private:
	static Simulation* currentSimulation;
public:
	static std::vector<std::vector<double>> read(std::vector<bool>& internalInstruction, std::vector<double>& internalParameters);
	static void loadParameters(std::vector<double>& internalParameters);
};
