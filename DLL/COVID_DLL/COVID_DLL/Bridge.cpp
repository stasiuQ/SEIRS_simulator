#include "pch.h"
#include "Bridge.h"
 
/*

Every common tab has a structure like this:
1st element - size of a tab
and others - partcular values

*/

extern "C" {
	__declspec(dllexport) void send(bool* commonInstr, double* commonParams, double* commonAgentsState, int* commonStats) {
		std::vector<bool> instructions;
		std::vector<double> parameters;
		for (int i = 1; i < 5; i++) {   // reading instructions from common memory
			instructions.push_back(commonInstr[i]);
		}
		for (int i = 1; i < commonParams[0]; i++) {   // reading parameters from common memory
			instructions.push_back(commonParams[i]);
		}
		std::vector<std::vector<double>> simulationState = Communicator::read(instructions, parameters);  //contains each agents state (i, j, type) and the last element provides statistics
		
		if (simulationState.size() > 1)   // if it is = 0;1 then we dont have to update anything in the shared memory (deletion and changing parameters case) 
		{
			int iterator = 1;
			for (int i = 0; i < (simulationState.size() - 1); i++) {  // updating commonAgentsState in shared memory (without last element of a vector)
				commonAgentsState[iterator] = simulationState[i][0];
				commonAgentsState[iterator + 1] = simulationState[i][1];
				commonAgentsState[iterator + 2] = simulationState[i][2];

				iterator += 3;
			}
			for (int i = 1; i < commonStats[0]; i++) {  // updating commonStats in shared memory (last element of simulationStatistics)
				commonStats[i] = static_cast<int>(simulationState[simulationState.size() - 1][i - 1]);
			}
		}

	}
}