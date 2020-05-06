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
	double paramTable[8];    // Update when no parameter in Global changes!
	ini_file.open(name, ios::in);
	if (!ini_file.good()) {
		cout << "Inicialization file corrupted!" << endl;
		ini_file.close();
	}
	else
	{
		int iterator = 0;
		while (!ini_file.eof() && (iterator < 8))
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
		concentration = paramTable[3];

		beta = paramTable[4];
		epsilon = paramTable[5];
		mu = paramTable[6];
		rho = paramTable[7];


		ini_file.close();
	}
}

double GlobalParameters::get_dt()
{
	return dt;
}
