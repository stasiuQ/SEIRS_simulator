#include "pch.h"
#include "Communicator.h"

 Simulation* Communicator::currentSimulation;

 void Communicator::clear_simulation()
 {
	 if (currentSimulation != nullptr) {
		 delete currentSimulation;
		 currentSimulation = nullptr;
	 }
 }

 std::vector<std::vector<double>> Communicator::read(int internalInstructions, std::vector<double>& internalParameters) {
	/* Reading instructions for simulation */
	switch (internalInstructions)
	{
	case 0:   // creating new simulation, have to load parameters too
		Randomizer::init();
		loadParameters(internalParameters);
		currentSimulation = new Simulation(GlobalParameters::get_concentration(), GlobalParameters::get_size(), GlobalParameters::get_initialInfected());
		break;
	case 1:  // deleting simulation
		clear_simulation();
		return std::vector<std::vector<double>>();
	case 2:   // proceeding simulation
		currentSimulation->simulate(1);
		break;
	case 3:     // changing parameters, no need to return statistics
		loadParameters(internalParameters);
		return std::vector<std::vector<double>>();
	case 4:     // initializing masks
		currentSimulation->wearMasks();
		break;
	case 5:     // initializing couriers
		currentSimulation->initializeCouriers();
		break;
	case 6:     // initializing homes
		currentSimulation->initializeHomes();
		break;
	case 7:     // proceeding a simulations with homes
		currentSimulation->simulateWithHomes(1);
		break;
	default:
		return std::vector<std::vector<double>>();
		break;
	}

	return currentSimulation->outputInterface();
}

void Communicator::loadParameters(std::vector<double>& internalParameters)
{
	GlobalParameters::load(internalParameters);
}
