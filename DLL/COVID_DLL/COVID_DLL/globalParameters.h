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
	static int initialInfected;
	
	//Epidemiological parameters
	static double beta; // S -> E
	static double epsilon;  // E -> I
	static double mu;  // I -> R
	static double rho; // R -> S

public:
	static void load(string name);
	static void load(vector<double>& parameters);
	static double get_dt();
	static double get_radius();
	static double get_mobility();
	static double get_concentration();
	static int get_size();
	static int get_initialInfected();
	static vector<double> get_sim_parameters();

};
