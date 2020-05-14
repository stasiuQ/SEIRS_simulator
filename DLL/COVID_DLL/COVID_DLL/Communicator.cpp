#include "pch.h"
#include "Communicator.h"

 Simulation* Communicator::currentSimulation;

std::vector<std::vector<double>> Communicator::read(std::vector<bool>& internalInstruction, std::vector<double>& internalParameters) {
	/* Reading instructions for simulation */
	if (internalInstruction[0] == true) {  // creating new simulation, have to load parameters too
		Randomizer::init();
		loadParameters(internalParameters);
		currentSimulation = new Simulation(GlobalParameters::get_concentration(), GlobalParameters::get_size(), GlobalParameters::get_initialInfected());
	}
	else if (internalInstruction[1] == true) {   // deleting an existing simulation , need to return an empty vector then
		delete currentSimulation;
		return std::vector<std::vector<double>>();
	}
	else if (internalInstruction[2] == true) {   //proceeding simulation
		currentSimulation->simulate(1);
	}
	else if (internalParameters[3] == true) {   // changing parameters, no need to change stats
		loadParameters(internalParameters);
		return std::vector<std::vector<double>>();
	}
	return currentSimulation->outputInterface();
}

void Communicator::loadParameters(std::vector<double>& internalParameters)
{
	GlobalParameters::load(internalParameters);
}
