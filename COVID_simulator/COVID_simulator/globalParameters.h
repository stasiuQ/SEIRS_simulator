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
	
	// Epidemiological parameters
	static double beta; // S -> E
	static double epsilon;  // E -> I
	static double mu;  // I -> R
	static double rho; // R -> S

	//*  ADDITIONAL FEATURES *//
	static int linearZonesDensity;
	// Probabilities
	static double wearingMaskProbability;
	static double beingCourierProbability;
	static double homeInZoneProbability;

	// Modifiers
	static double mobilityModifier;
	static double radiusModifier;
	static double spreadingModifier;

public:
	static void load(string name);
	static void load(vector<double>& parameters);
	static double get_dt();
	static double get_radius();
	static double get_mobility();
	static double get_concentration();
	static int get_size();
	static int get_initialInfected();

	static int get_linearZonesDensity();
	static double get_wearingMaskProbability();
	static double get_beingCourierProbability();
	static double get_homeInZoneProbability();
	static double get_mobilityModifier();
	static double get_radiusModifier();
	static double get_spreadingModifier();
	static vector<double> get_sim_parameters();

};
