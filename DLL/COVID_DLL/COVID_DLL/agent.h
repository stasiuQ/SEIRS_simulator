#pragma once
#include <vector>

using namespace std;

enum SEIRS_type { S, E, I, R };

class Agent {

private:
	int m_size;  // size of the square system matrix
	double i;       // coordinates of the agents
	double j;

	SEIRS_type type;
	double mobility;   // number from 0 to 1
	double radius;   // radius of agent (infectious) circular area
	vector<double> parameters;  // set of coefficients [4] : beta, epsilon, mu , rho

public:
	Agent(SEIRS_type state, int size, double r, double mob, vector<double> param);
	Agent();
	~Agent();

	// getters
	double get_i();
	double get_j();
	double get_radius();
	SEIRS_type get_type();

	void move();
	void interaction(Agent* that);
	void update();  // corresponds to only time dependant proccesses


};