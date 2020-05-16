#pragma once

extern "C" {
	__declspec(dllexport) int send(bool* commonInstr, double* commonParams, double* commonAgentsState, int* commonStats);
}