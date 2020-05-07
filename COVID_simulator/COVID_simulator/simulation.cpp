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

}

Simulation::~Simulation()
{
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