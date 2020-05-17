#pragma once
#include "pch.h"

class Debugger {
private:
	static fstream console;
public:
	static void init();
	static void close();
	static void write(string message);
	static void write(int i);
};
