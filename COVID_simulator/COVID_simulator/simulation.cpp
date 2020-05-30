#include "simulation.h"
#include "globalParameters.h"
#include "randomizer.h"

using namespace std;

Simulation::Simulation(double concentration, int m_size, int numberOfInfected)
{
	this->size = m_size;
	this->concentration = concentration;
	this->initialInfected = numberOfInfected;
	this->numberOfAgents = static_cast<int>((m_size * m_size * concentration) / (M_PI * GlobalParameters::get_radius() * GlobalParameters::get_radius()));
	this->agents = vector<Agent>(this->numberOfAgents);
	this->homes = vector<Home>(this->linearZonesDensity * this->linearZonesDensity);
	this->simulationParameters = GlobalParameters::get_sim_parameters();
	this->maxHomeRadius = zoneSize / 4.;
	this->minHomeRadius = maxHomeRadius / 4.;
	
	for (int i = 0; i < numberOfInfected; i++) // creating agents, first infected, then susceptible
	{
		Agent agent = Agent(SEIRS_type::I, m_size, GlobalParameters::get_radius(), GlobalParameters::get_mobility(), this->simulationParameters);
		this->agents[i] = agent;
	}
	for (int i = numberOfInfected; i < this->numberOfAgents; i++)
	{
		Agent agent = Agent(SEIRS_type::S, m_size, GlobalParameters::get_radius(), GlobalParameters::get_mobility(), this->simulationParameters);
		this->agents[i] = agent;
	}

	/// TESTING ONLY!!!
	wearMasks();
	initializeCouriers();
	initializeHomes();

	this->outputFile.open("output_data.txt", ios::out);
	this->outputFile << "S" << "	" << "E" << "	" << "I" << "	" << "R" << endl;
}

Simulation::~Simulation()
{
	this->outputFile.close();
}

void Simulation::wearMasks()
{
	for (int i = 0; i < this->numberOfAgents; i++)
	{
		double rand = Randomizer::randomize();
		if (rand < wearingMaskProbability)
		{
			this->agents[i].set_radius(radiusModifier * GlobalParameters::get_radius());
			this->agents[i].set_beta(spreadingModifier * this->simulationParameters[0]);
		}
	}
}

void Simulation::initializeCouriers()
{
	for (int i = 0; i < this->numberOfAgents; i++)
	{
		double rand = Randomizer::randomize();
		if (rand < beingCourierProbability)
			this->agents[i].set_mobility(mobilityModifier * GlobalParameters::get_mobility());
	}
}

void Simulation::initializeHomes()
{
	double perimeterDistance = this->maxHomeRadius * 1.1;
	for (int i = 0; i < linearZonesDensity; i++)
	{
		for (int j = 0; j < linearZonesDensity; j++)
		{
			double rand = Randomizer::randomize();
			if (rand < homeInZoneProbability)
			{
				/// Parameters below ensure that homes will not overlap.
				double iOfHomeInZone = (Randomizer::randomize() * (this->zoneSize - 2 * perimeterDistance)) + perimeterDistance;
				double jOfHomeInZone = (Randomizer::randomize() * (this->zoneSize - 2 * perimeterDistance)) + perimeterDistance;
				double iOfHome = i * this->zoneSize + iOfHomeInZone;
				double jOfHome = j * this->zoneSize + jOfHomeInZone;
				double homeRadius = (Randomizer::randomize() * (this->maxHomeRadius - this->minHomeRadius)) + this->minHomeRadius;
				homes[ijTo_k(i, j)] = Home(iOfHome, jOfHome, homeRadius);
			}
		}
	}
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
		return true;
	else
		return false;
}

bool Simulation::detectHome(double i, double j)
{
	int iIndexOfHome = i / zoneSize;
	int jIndexOfHome = j / zoneSize;
	if (homes[ijTo_k(iIndexOfHome, jIndexOfHome)].get_radius() != 0)
		return true;
	else
		return false;
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

int Simulation::ijTo_k(int i, int j)
{
	return i * linearZonesDensity + j;
}
