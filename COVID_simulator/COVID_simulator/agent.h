#pragma once
#include <vector>

using namespace std;

class Agent {

private:
	int m_size;  // size of the square system matrix
	double i;       // coordinates of the agents
	double j;

	enum SEIRS_type type;
	double mobility;   // number from 0 to 1
	double radius;   // radius of agent (infectious) circular area
	vector<double> parameters;

public:
	Agent(SEIRS_type state, int size, double r, double mob);
	~Agent();

	void move();
	void interaction(Agent* that);


};

enum SEIRS_type {S, E, I, R};
