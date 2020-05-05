#include "agent.h"
#include "randomizer.h"

Agent::Agent(SEIRS_type state, int size, double r, double mob, vector<double> param)
{
	this->type = state;
	this->m_size = size;
	this->radius = r;
	this->mobility = mob;
	this->i = Randomizer::randomize() * size;
	this->j = Randomizer::randomize() * size;
	this->parameters = param;
}

Agent::~Agent()
{
}

void Agent::move()
{
	double di = (Randomizer::randomize() - 0.5) * mobility * dt;
	double dj = (Randomizer::randomize() - 0.5) * mobility * dt;
	this->i += di;
	this->j += dj;

}

void Agent::interaction(Agent * that)
{
}
