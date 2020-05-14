#include "pch.h"
#include "agent.h"
#include "randomizer.h"
#include "globalParameters.h"

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

Agent::Agent()
{
	this->type = SEIRS_type::S;
	this->m_size = 0;
	this->radius = 0.;
	this->mobility = 0.;
	this->i = 0.;
	this->j = 0.;
	this->parameters = vector<double>();
}

Agent::~Agent()
{
}

double Agent::get_i()
{
	return this->i;
}

double Agent::get_j()
{
	return this->j;
}

double Agent::get_radius()
{
	return this->radius;
}

SEIRS_type Agent::get_type()
{
	return this->type;
}

void Agent::move()
{
	double di = (Randomizer::randomize() - 0.5) * mobility * GlobalParameters::get_dt();
	double dj = (Randomizer::randomize() - 0.5) * mobility * GlobalParameters::get_dt();
	
	double new_i = this->i + di;  // Periodic boundary conditions
	double new_j = this->j + dj;
	if (new_i >= this->m_size)
	{
		new_i -= this->m_size;
	}
	else if (new_i < 0)
	{
		new_i += this->m_size;
	}

	if (new_j >= this->m_size)
	{
		new_j -= this->m_size;
	}
	else if (new_j < 0)
	{
		new_j += this->m_size;
	}
	
	this->i = new_i;
	this->j = new_j;

}

void Agent::interaction(Agent * that)   // S -> E interactions
{
	double rand;
	switch (this->type)
	{
	case SEIRS_type::S:
		switch (that->type)
		{
		case SEIRS_type::I:
			rand = Randomizer::randomize();
			if (rand < this->parameters[0])  // susceptible become exposed
			{
				this->type = SEIRS_type::E;
			}
			break;
		default:
			break;
		}
		break;

	case SEIRS_type::I:
		switch (that->type)
		{
		case SEIRS_type::S:
			rand = Randomizer::randomize();
			if (rand < that->parameters[0])  // susceptible become exposed
			{
				that->type = SEIRS_type::E;
			}
			break;
		default:
			break;
		}
		break;
	
	default:
		break;
	}
}

void Agent::update()   // only time dependent processes
{
	double rand;
	switch (this->type)
	{
	case SEIRS_type::E:
		rand = Randomizer::randomize();
		if (rand < this->parameters[1])  // E -> I
		{
			this->type = SEIRS_type::I;
		}
		break;

	case SEIRS_type::I:
		rand = Randomizer::randomize();
		if (rand < this->parameters[2])  // I -> R
		{
			this->type = SEIRS_type::R;
		}
		break;

	case SEIRS_type::R:
		rand = Randomizer::randomize();
		if (rand < this->parameters[3])  // R -> S
		{
			this->type = SEIRS_type::S;
		}
		break;

	default:
		break;
	}
}
