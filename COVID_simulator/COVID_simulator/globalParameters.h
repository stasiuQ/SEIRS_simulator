#pragma once

#include <string>
#include <fstream>
#include <iostream>
#include <sstream>
#include <vector>

using namespace std;

class GlobalParameters {
	
private:
	static double dt;
	static double size;
	static double radius;
	static double mobility;
	static double concentration;
	
	//Epidemiological parameters
	static double beta; // S -> E
	static double epsilon;  // E -> I
	static double mu;  // I -> R
	static double rho; // R -> S

public:
	static void load(string name);
	static double get_dt();
	static double get_radius();
	static double get_mobility();
	static vector<double> get_sim_parameters();

};
