#pragma once

extern "C" {
	__declspec(dllexport) void send(bool* instr, double* parameters, double* simState);
}