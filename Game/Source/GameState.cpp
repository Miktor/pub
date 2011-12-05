#include "GameState.h"
#include "AppManager.h"

#include "VehicleProperties.h"
#include "vehicleOptions.h"

#include "Physics.h"
#include "mCars.h"
#include "CarEngine.h"
#include "CarSpeedometer.h"
#include "DebugText.h"

#include "Camera.h"
#include "AppManager.h"

#include "Object.h"

namespace RacingGame
{
	GameState::GameState(AppManager* am) : wireframe(false)
	{
		AppMgr = am;
		StopScene = false;
		carCount = 0;
		myOptions = new mVehicleOptions();
	}
	GameState::~GameState()
	{
		AppMgr = NULL;
		if(myPhysics)
			delete myPhysics;
	}
	void GameState::enter()
	{
		AppMgr->mSceneMgr->setAmbientLight( ColourValue( 0.2f, 0.2f, 0.2f ) );
		mLight = AppMgr->mSceneMgr->createLight("mainLight");
		mLight->setType(Light::LightTypes::LT_POINT);
		mLight->setPosition(0, 30, 0);
		mLight->setDiffuseColour(1.0, 1.0, 1.0);
		mLight->setSpecularColour(1.0, 1.0, 1.0);

		AppMgr->mSceneMgr->setSkyBox(true, "SkyBox", 5000, false);

		myPhysics = new Physics(AppMgr->mSceneMgr);
		myPhysics->initWorld();

		vehicleSM = new VehicleSpeedometer();
		vehicleSM->init(AppMgr->mRenderWindow->getWidth() - 261, AppMgr->mRenderWindow->getHeight() - 261);

		createCar();
	}

	void GameState::createCar()
	{
		carList.push_back(new mCar(myPhysics->getWorld(), AppMgr->mSceneMgr, AppMgr));
		carList.back()->AddCar("BMWM3" + Ogre::StringConverter::toString(carCount++), myOptions);
		AppMgr->mCamera->setTrackObj(carList.back()->getVehicle(), carList.back()->getOgreChassis());
		AppMgr->addUpdObj(static_cast<UpdatableObject*>(AppMgr->mCamera));
	}

	void GameState::deleteCar()
	{
		if(carList.size() >= 1)
		{
			delete carList.back();
			carList.pop_back();
		}
		if(!carList.empty())
		{
			AppMgr->mCamera->setTrackObj(carList.back()->getVehicle(), carList.back()->getOgreChassis());;
		}
		else
		{
			AppMgr->mCamera->setTrackObj(NULL, NULL);
		}
	}
	void GameState::exit()
	{
		delete myPhysics;
		StopScene = true;
	}
	void GameState::pause(){}
	void GameState::resume(){}

	bool GameState::frameStarted(const Ogre::FrameEvent &evt)
	{
		if (AppMgr->mInfo)
		{
			static float time = 0;
			time += evt.timeSinceLastFrame;
			if (time > 1)
			{
				time -= 1;
				try
				{
					const Ogre::RenderTarget::FrameStats& stats = AppMgr->mRenderWindow->getStatistics();
					AppMgr->mInfo->change("FPS", stats.lastFPS);
					AppMgr->mInfo->change("triangle", stats.triangleCount);
					AppMgr->mInfo->change("speed", (int)carList.back()->getSpeed());
					AppMgr->mInfo->update();
				}
				catch (...)
				{
				}
			}
		}

		if(myPhysics!=NULL)
			myPhysics->frameStarted(evt);

		AppMgr->getGUI()->injectFrameEntered(evt.timeSinceLastFrame);

		for(int i = 0; i < carList.size(); i++)
			carList[i]->update(evt.timeSinceLastFrame);
		vehicleSM->update(carList.back()->getSpeed());

		return !StopScene;
	}
	bool GameState::frameRenderingQueued(const Ogre::FrameEvent &evt)
	{
		if(myPhysics!=NULL)
			myPhysics->frameRenderingQueued(evt);
		return !StopScene;
	}
	bool GameState::frameEnded(const Ogre::FrameEvent &evt)
	{
		if(myPhysics!=NULL)
			myPhysics->frameEnded(evt);
		return !StopScene;
	}

	bool GameState::mouseMoved(const OIS::MouseEvent &arg)
	{
		AppMgr->getGUI()->injectMouseMove(arg.state.X.abs, arg.state.Y.abs, arg.state.Z.abs);
		AppMgr->mCamera->updateCamOrig(arg.state.X.rel, arg.state.Y.rel, arg.state.Z.rel);
		return !StopScene;
	}
	bool GameState::mousePressed(const OIS::MouseEvent &arg, OIS::MouseButtonID id)
	{
		AppMgr->getGUI()->injectMousePress(arg.state.X.abs, arg.state.Y.abs, MyGUI::MouseButton::Enum(id));
		return !StopScene;
	}
	bool GameState::mouseReleased(const OIS::MouseEvent &arg, OIS::MouseButtonID id)
	{
		AppMgr->getGUI()->injectMouseRelease(arg.state.X.abs, arg.state.Y.abs, MyGUI::MouseButton::Enum(id));
		return !StopScene;
	}

	bool GameState::keyPressed(const OIS::KeyEvent &arg)
	{
		AppMgr->getGUI()->injectKeyPress(MyGUI::KeyCode::Enum(arg.key), arg.text);
		switch (arg.key)
		{
		case OIS::KC_C:
			createCar();
			break;
		case OIS::KC_D:
			deleteCar();
			break;
		case OIS::KC_R:
			deleteCar();
			createCar();			
			break;
		case OIS::KC_W:	
			wireframe = !wireframe;
			AppMgr->mSceneMgr->getRootSceneNode()->flipVisibility(!wireframe);
			myPhysics->setDebugType((wireframe == true) ? 1 : 0);
			break;
		case OIS::KC_UP:
			if(myPhysics && carList.size() > 0)
				carList.back()->Acselerate();
				break;
		case OIS::KC_DOWN:
			if(myPhysics && carList.size() > 0)
				carList.back()->Brake();
			break;
		case OIS::KC_LEFT:
			if(myPhysics && carList.size() > 0)
				carList.back()->steerLeft();
			break;
		case OIS::KC_RIGHT:
			if(myPhysics && carList.size() > 0)
				carList.back()->steerRight();
			break;
		case OIS::KC_SPACE:
			if(myPhysics && carList.size() > 0)
				carList.back()->HandeBrake();
			break;
		case OIS::KC_ESCAPE:
			StopScene = true;
			break;
		case OIS::KC_V:
			AppMgr->mCamera->changeCamType();
			break;
		case OIS::KC_F1:
			pause();
			AppMgr->_states.push_back(AppMgr->getState("VehicleProperties"));
			AppMgr->getEndState()->enter();
			static_cast<VehicleProperties*>(AppMgr->getEndState())->setProperties(&myOptions->floatOpt);
			break;
		}
		return !StopScene;
	}
	bool GameState::keyReleased(const OIS::KeyEvent &arg)
	{
		AppMgr->getGUI()->injectKeyRelease(MyGUI::KeyCode::Enum(arg.key));
		switch (arg.key)
		{
		case OIS::KC_UP:
			if(myPhysics && carList.size() > 0)
				carList.back()->stopAcselerate();
			break;
		case OIS::KC_DOWN:
			if(myPhysics && carList.size() > 0)
				carList.back()->stopBrake();
			break;
		case OIS::KC_LEFT:
			if(myPhysics && carList.size() > 0)
				carList.back()->stopSteering();
			break;
		case OIS::KC_RIGHT:
			if(myPhysics && carList.size() > 0)
				carList.back()->stopSteering();
			break;
		case OIS::KC_SPACE:
			if(myPhysics && carList.size() > 0)
				carList.back()->stopHandeBrake();
		}
		return !StopScene;
	}
}