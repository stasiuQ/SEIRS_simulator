#pragma once

#include <string>
#include <fstream>
#include <iostream>
#include <sstream>

using namespace std;

class GlobalParameters {
	
private:
	static double dt;
	static double size;
	static double concentration;
	
	static double beta; // S -> E
	static double epsilon;  // E -> I
	static double mu;  // I -> R
	static double rho; // R -> S

public:
	static void load(string name);

};
