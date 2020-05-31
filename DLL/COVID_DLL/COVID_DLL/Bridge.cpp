#include "pch.h"
#include "Bridge.h"
#include "debugger.h"
 
/*
Instruction for int instructions:
0 - creating a simulation
1 - deleting a simulation
2 - proceeding a simulation
3 - changing parameters
4 - initialize masks
5 - initiazlize couriers
6 - initiazlize homes
7 - proceeding a simulation with homes


Every common tab has a structure like this:
1st element - size of a tab
and others - partcular values

*/

extern "C" {
	
	__declspec(dllexport) void send(int commonInstr, double* commonParams, double* commonAgentsState, int* commonStats, double* commonHomes) {
		std::vector<double> parameters;

		for (int i = 1; i < static_cast<int>(commonParams[0]); i++) {   // reading parameters from common memory
			parameters.push_back(commonParams[i]);
		}

		std::vector<std::vector<double>> simulationState = Communicator::read(commonInstr, parameters);  //contains each agents state (i, j, type) and the last element provides statistics
		
		if (simulationState.size() > 1)   // if it is = 0;1 then we dont have to update anything in the shared memory (deletion and changing parameters case) 
		{
			int iterator = 1;
			for (int i = 0; i < (simulationState.size() - 1); i++) {  // updating commonAgentsState in shared memory (without last element of a vector)
				commonAgentsState[iterator] = simulationState[i][0];
				commonAgentsState[iterator + 1] = simulationState[i][1];
				commonAgentsState[iterator + 2] = simulationState[i][2];

				iterator += 3;
			}
			for (int i = 1; i < commonStats[0]; i++) {  // updating commonStats in shared memory (second last element of simulationStatistics)
				commonStats[i] = static_cast<int>(simulationState[simulationState.size() - 2][i - 1]);
			}
			for (int i = 1; i < commonHomes[0]; i++) {  // updating commonHomes in shared memory (last element of simulationStatistics)
				commonHomes[i] = simulationState[simulationState.size() - 1][i - 1];
			}
		}

	}


}