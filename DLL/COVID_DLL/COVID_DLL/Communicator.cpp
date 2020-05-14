#include "pch.h"
#include "Communicator.h"

std::vector<std::vector<double>> Communicator::read(std::vector<bool>& internalInstruction, std::vector<double>& internalParameters) {
	/* Reading instructions for simulation */
	if (internalInstruction[0] == true) {  // creating new simulation
		loadParameters(internalParameters);
		currentSimulation = new Simulation(GlobalParameters::get_concentration(), GlobalParameters::get_size(), GlobalParameters::get_initialInfected());
	}
	return std::vector<std::vector<double>>();
}

void Communicator::loadParameters(std::vector<double>& internalParameters)
{
	GlobalParameters::load(internalParameters);
}
