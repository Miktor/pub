#include "stdafx.h"
#include "AppManager.h"
#include "MainMenu.h"
#include "GameState.h"
#include "VehicleProperties.h"

using namespace RacingGame;

INT WINAPI WinMain(HINSTANCE hInst, HINSTANCE, LPSTR strCmdLine, INT)
{
	AppManager* appManager = new AppManager();
	// инициализируе состояния
	//MainMenu* mainMenu = new MainMenu(appManager);
	GameState* gameState = new GameState(appManager);
	VehicleProperties* vehiclePropertiesState = new VehicleProperties(appManager);
	appManager->addState("VehicleProperties", vehiclePropertiesState);
	//appManager->addState("MainMenu", mainMenu);
	appManager->addState("GameState", gameState);
	// инициализируем и стартуем
	appManager->start(appManager->getState("GameState"), appManager);
	// шатдаун
	appManager->shutdown();

	delete appManager;
	return 0;
}