#include "agents.h"
#include "simulation.h"
#include "globalParameters.h"
#include "randomizer.h"

using namespace std;

int main()
{
	Randomizer::init();
	GlobalParameters::load("ini_file.txt");
	Simulation my_simulation(0.2, 100, 2);

	my_simulation.simulate(1000);

	return 0;
}