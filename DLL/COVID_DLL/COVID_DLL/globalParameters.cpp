#include "pch.h"
#include "globalParameters.h"

double GlobalParameters::dt;
double GlobalParameters::size;
double GlobalParameters::radius;
double GlobalParameters::mobility;
double GlobalParameters::concentration;
int GlobalParameters::initialInfected;

double GlobalParameters::beta; // S -> E
double GlobalParameters::epsilon;  // E -> I
double GlobalParameters::mu;  // I -> R
double GlobalParameters::rho; // R -> S

void GlobalParameters::load(string name)
{
	fstream ini_file;
	double paramTable[9];    // Update when no parameter in Global changes!
	ini_file.open(name, ios::in);
	if (!ini_file.good()) {
		cout << "Inicialization file corrupted!" << endl;
		ini_file.close();
	}
	else
	{
		int iterator = 0;
		while (!ini_file.eof() && (iterator < 10))
		{
			string my_string;
			getline(ini_file, my_string);
			for (int i = 0; i < my_string.length(); i++) {
				if (my_string[i] == ' ') {
					stringstream ss(my_string.substr(0, i));
					ss >> paramTable[iterator];
					break;
				}
			}
			iterator++;
		}
		dt = paramTable[0];
		size = paramTable[1];
		radius = paramTable[2];
		mobility = paramTable[3];
		concentration = paramTable[4];
		initialInfected = static_cast<int>(paramTable[5]);

		beta = paramTable[6];
		epsilon = paramTable[7];
		mu = paramTable[8];
		rho = paramTable[9];


		ini_file.close();
	}
}

void GlobalParameters::load(vector<double>& parameters)
{
	dt = parameters[0];
	size = parameters[1];
	radius = parameters[2];
	mobility = parameters[3];
	concentration = parameters[4];
	initialInfected = static_cast<int>(parameters[5]);

	beta = parameters[6];
	epsilon = parameters[7];
	mu = parameters[8];
	rho = parameters[9];
}

double GlobalParameters::get_dt()
{
	return dt;
}

double GlobalParameters::get_radius()
{
	return radius;
}

double GlobalParameters::get_mobility()
{
	return mobility;
}

double GlobalParameters::get_concentration()
{
	return concentration;
}

int GlobalParameters::get_size()
{
	return static_cast<int>(size);
}

int GlobalParameters::get_initialInfected()
{
	return initialInfected;
}

vector<double> GlobalParameters::get_sim_parameters()
{
	vector<double> sim_params;

	sim_params.push_back(beta);
	sim_params.push_back(epsilon);
	sim_params.push_back(mu);
	sim_params.push_back(rho);
	
	return sim_params;
}
