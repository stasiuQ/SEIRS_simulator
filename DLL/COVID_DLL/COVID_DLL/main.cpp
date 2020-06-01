#include "pch.h"
#include "agents.h"
#include "simulation.h"
#include "globalParameters.h"
#include "randomizer.h"

using namespace std;

int main()
{
	int inst = 0;
	vector<double> parameters = vector<double> { 1, 100, 2, 0.75, 0.4, 2, 0.9, 0.05, 0.005, 0, 5, 0.5, 0.05, 0.2, 3, 0.5, 0  };

	vector<vector<double>> out = Communicator::read(inst, parameters);

	inst = 2;
	out = Communicator::read(inst, parameters);

	inst = 1;
	out = Communicator::read(inst, parameters);

	return 0;
}