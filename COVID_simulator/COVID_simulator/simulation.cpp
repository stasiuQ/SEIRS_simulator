#include "simulation.h"

Simulation::Simulation(double concentration, int m_size, int numberOfInfected)
{
	this->size = m_size;
	this->concentration = concentration;

}

Simulation::~Simulation()
{
}

bool Simulation::detectContact(Agent * a1, Agent * a2)
{
	return false;
}
