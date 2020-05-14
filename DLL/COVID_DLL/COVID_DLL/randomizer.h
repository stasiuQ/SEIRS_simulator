#pragma once

#include <random>
#include <ctime>

using namespace std;

class Randomizer {
	
private:
	static mt19937 generator;  // Mersene twister PRN generator
	static uniform_real_distribution<double> dis;

public:
	static void init();
	static double randomize();
};
