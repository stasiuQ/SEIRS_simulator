#include "home.h"

Home::Home(double i, double j, double radius, double minHomeDistance, double blockTime, double maxCapacity, double mobilityModifier)
{
	this->radius = radius;
	this->blockTime = blockTime;
	this->elapsedTime = 0;
	this->maxCapacity = maxCapacity;
	this->capacity = 0;
	this->mobilityModifier = mobilityModifier;
	this->minHomeDistance = radius + minHomeDistance;
}

bool Home::isHomeHere(Home *home)
{
	return false;
}

bool Home::canAgentEnter(double dt)
{
	if (capacity < maxCapacity)	/// Additonaly isAgentInside has to be added to make sure we do not block agents inside.
								/// This could be possible if we had too many agents in home initially.
	{
		this->elapsedTime += dt;
		if (elapsedTime >= blockTime)
			return true;
		else
			return false;
	}
	else
		return false;
}

double Home::get_i()
{
	return this->i;
}

double Home::get_j()
{
	return this->j;
}

double Home::get_mobilityModifier()
{
	return this->mobilityModifier;
}

void Home::update(Agent * agent)
{
	double distance = pow(this->i - agent->get_i(), 2.) + pow(this->j - agent->get_j(), 2.);
	if (distance < this->radius)
		capacity += 1;
}