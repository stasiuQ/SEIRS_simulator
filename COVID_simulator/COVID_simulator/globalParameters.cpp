#include "globalParameters.h"

double GlobalParameters::dt;
double GlobalParameters::size;
double GlobalParameters::concentration;

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
		while (!ini_file.eof() && (iterator < 9))
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

		beta = paramTable[5];
		epsilon = paramTable[6];
		mu = paramTable[7];
		rho = paramTable[8];


		ini_file.close();
	}
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

vector<double> GlobalParameters::get_sim_parameters()
{
	vector<double> sim_params;

	sim_params.push_back(beta);
	sim_params.push_back(epsilon);
	sim_params.push_back(mu);
	sim_params.push_back(rho);
	
	return sim_params;
}
