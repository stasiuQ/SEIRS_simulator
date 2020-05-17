#include "agents.h"
#include "simulation.h"
#include "globalParameters.h"
#include "randomizer.h"

using namespace std;

int main()
{
	Randomizer::init();
	GlobalParameters::load("ini_file.txt");
	Simulation my_simulation(GlobalParameters::get_concentration(), GlobalParameters::get_size(), 2);

	my_simulation.simulate(2000);

	return 0;
}