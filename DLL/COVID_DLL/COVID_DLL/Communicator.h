#pragma once
#ifndef COMMUNICATOR_H
#define COMMUNICATOR_H

#include <string>
#include <vector>

class Communicator {
public:
	static std::vector<std::vector<double>> read(std::vector<bool>* internalInstruction, std::vector<double>* internalParameters);
};

#endif
