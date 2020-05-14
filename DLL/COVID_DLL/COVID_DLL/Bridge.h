#pragma once

extern "C" {
	__declspec(dllexport) void send(bool* commonInstr, double* commonParams, double* commonAgentsState, int* commonStats);
}