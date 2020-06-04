#include "globalParameters.h"
#include "home.h"

Home::Home(double i, double j, double radius)
{
	this->i = i;
	this->j = j;
	this->radius = radius;
	this->mobilityModifier = 0.1;
}

Home::Home()
{
	this->i = 0.;
	this->j = 0.;
	this->radius = 0.;
	this->mobilityModifier = 1.;
}

/// ### COLLISIONS FEATURE: ###
/*
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
*/
/// ##########################

double Home::get_i()
{
	return this->i;
}

double Home::get_j()
{
	return this->j;
}

double Home::get_radius()
{
	return this->radius;
}

double Home::get_mobilityModifier()
{
	return this->mobilityModifier;
}