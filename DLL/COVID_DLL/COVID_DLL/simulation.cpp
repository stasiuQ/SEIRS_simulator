#include "pch.h"
#include "simulation.h"
#include "globalParameters.h"

using namespace std;

Simulation::Simulation(double concentration, int m_size, int numberOfInfected)
{
	this->size = m_size;
	this->concentration = concentration;
	this->initialInfected = numberOfInfected;
	this->numberOfAgents = static_cast<int>((m_size * m_size * concentration) / (M_PI * GlobalParameters::get_radius() * GlobalParameters::get_radius()));
	this->agents = vector<Agent>(this->numberOfAgents);
	this->simulationParameters = GlobalParameters::get_sim_parameters();
	
	for (int i = 0; i < numberOfInfected; i++) // creating agents, fisrt infected, then susceptible
	{
		Agent agent = Agent(SEIRS_type::I, m_size, GlobalParameters::get_radius(), GlobalParameters::get_mobility(), this->simulationParameters);
		this->agents[i] = agent;
	}
	for (int i = numberOfInfected; i < this->numberOfAgents; i++)
	{
		Agent agent = Agent(SEIRS_type::S, m_size, GlobalParameters::get_radius(), GlobalParameters::get_mobility(), this->simulationParameters);
		this->agents[i] = agent;
	}

	this->outputFile.open("output_data.txt", ios::out);
	this->outputFile << "S" << "	" << "E" << "	" << "I" << "	" << "R" << endl;
}

Simulation::~Simulation()
{
	this->outputFile.close();
}

void Simulation::simulate(int numberOfSteps)
{
	for (int i = 0; i < numberOfSteps; i++)
	{
		for (int j = 0; j < this->numberOfAgents; j++)  // moving all agents
		{
			agents[j].move();
		}

		for (int j = 0; j < this->numberOfAgents; j++) // iteration over all pairs in order to detect contact
		{
			for (int k = (j + 1); k < this->numberOfAgents; k++)
			{
				if (detectContact(&agents[j], &agents[k]))  // we have interaction
				{
					agents[j].interaction(&agents[k]);
				}
			}
		}

		for (int j = 0; j < this->numberOfAgents; j++)  // time only dependant processes for all agents
		{
			agents[j].update();
		}
		
		this->updateStatistics();
	}

}

bool Simulation::detectContact(Agent * a1, Agent * a2)
{
	double di = a1->get_i() - a2->get_i();
	double dj = a1->get_j() - a2->get_j();
	double distance = sqrt((di * di) + (dj * dj));

	if (distance < (a1->get_radius() + a2->get_radius()))
	{
		return true;
	}
	else
	{
		return false;
	}
}

void Simulation::updateStatistics()
{
	this->no_S = 0;
	this->no_E = 0;
	this->no_I = 0;
	this->no_R = 0;

	for (int i = 0; i < this->numberOfAgents; i++)
	{
		switch (agents[i].get_type())
		{
		case SEIRS_type::S:
			this->no_S++;
			break;
		
		case SEIRS_type::E:
			this->no_E++;
			break;
		
		case SEIRS_type::I:
			this->no_I++;
			break;
		
		case SEIRS_type::R:
			this->no_R++;
			break;
		
		default:
			break;
		}
	}

	this->printStatistics();
}

void Simulation::printStatistics()
{
	this->outputFile << no_S << "	" << no_E << "	" << no_I << "	" << no_R << endl;
}

vector<vector<double>> Simulation::outputInterface()
{
	vector<vector<double>> outputVector = vector<vector<double>>();
	for (int i = 0; i < this->numberOfAgents; i++) {
		vector<double> tempVector = vector<double>();
		tempVector.push_back(agents[i].get_i());   // firstly pushing coordinates
		tempVector.push_back(agents[i].get_j());

		switch (agents[i].get_type())
		{
		case SEIRS_type::S:
			tempVector.push_back(0.);
			break;

		case SEIRS_type::E:
			tempVector.push_back(1.);
			break;

		case SEIRS_type::I:
			tempVector.push_back(2.);
			break;

		case SEIRS_type::R:
			tempVector.push_back(3.);
			break;

		default:
			break;
		}

		outputVector.push_back(tempVector);
	}
	vector<double> tempVector = vector<double>();
	tempVector.push_back(no_S);
	tempVector.push_back(no_E);
	tempVector.push_back(no_I);
	tempVector.push_back(no_R);
	tempVector.push_back(step);
	outputVector.push_back(tempVector);

	return outputVector;
}
