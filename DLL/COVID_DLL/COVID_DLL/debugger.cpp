#include "pch.h"
#include "debugger.h"

fstream Debugger::console;

void Debugger::init()
{
	console.open("console.txt", ios::app);
}

void Debugger::close()
{
	console.close();
}


void Debugger::write(string message)
{
	console << message << endl;
}

void Debugger::write(int i)
{
	console << i;
}
