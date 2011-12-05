#ifndef GameState_H
#define GameState_H

#include "stdafx.h"

namespace RacingGame
{
	class Physics;
	class mCar;
	class VehicleSpeedometer;
	class DebugCarText;
	class mVehicleOptions;

	class GameState : public AppState
	{
	public:
		GameState(AppManager *am);
		~GameState();

		void enter();
		void exit();

		void pause();
		void resume();

		bool frameStarted(const Ogre::FrameEvent &evt);
		bool frameRenderingQueued(const Ogre::FrameEvent &evt);
		bool frameEnded(const Ogre::FrameEvent &evt);

		bool mouseMoved(const OIS::MouseEvent &arg);
		bool mousePressed(const OIS::MouseEvent &arg, OIS::MouseButtonID id);
		bool mouseReleased(const OIS::MouseEvent &arg, OIS::MouseButtonID id);

		bool keyPressed(const OIS::KeyEvent &arg);
		bool keyReleased(const OIS::KeyEvent &arg);

		void createCar();
		void deleteCar();

		mVehicleOptions* myOptions;
	private:

		Physics* myPhysics;
		bool StopScene;

		std::vector<mCar*> carList;
		size_t carCount;

		VehicleSpeedometer* vehicleSM;
		Ogre::Light* mLight;

		bool wireframe;
	};
}
#endif