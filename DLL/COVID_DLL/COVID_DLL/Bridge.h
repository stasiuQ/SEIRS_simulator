#pragma once

extern "C" {
	__declspec(dllexport) void send(int commonInstr, double* commonParams, double* commonAgentsState, int* commonStats);
}