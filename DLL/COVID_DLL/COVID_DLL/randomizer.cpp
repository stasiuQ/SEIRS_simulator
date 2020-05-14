#include "pch.h"
#include "randomizer.h"

mt19937 Randomizer::generator;
uniform_real_distribution<double> Randomizer::dis;

void Randomizer::init()
{
	generator = mt19937 (time(NULL));
	dis = uniform_real_distribution<double>(0.0, 1.0);
}

double Randomizer::randomize()
{
	return dis(generator);
}
