// pch.h: wstępnie skompilowany plik nagłówka.
// Wymienione poniżej pliki są kompilowane tylko raz, co poprawia wydajność kompilacji dla przyszłych kompilacji.
// Ma to także wpływ na wydajność funkcji IntelliSense, w tym uzupełnianie kodu i wiele funkcji przeglądania kodu.
// Jednak WSZYSTKIE wymienione tutaj pliki będą ponownie kompilowane, jeśli którykolwiek z nich zostanie zaktualizowany między kompilacjami.
// Nie dodawaj tutaj plików, które będziesz często aktualizować (obniża to korzystny wpływ na wydajność).

#ifndef PCH_H
#define PCH_H
#define _USE_MATH_DEFINES

// w tym miejscu dodaj nagłówki, które mają być wstępnie kompilowane
#include "framework.h"
#include <string>
#include <vector>


#include "Bridge.h"
#include "Communicator.h"
#include "simulation.h"
#include "globalParameters.h"
#include "agents.h"
#include "randomizer.h"

#endif //PCH_H
