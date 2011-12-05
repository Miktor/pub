#include "Camera.h"

namespace RacingGame
{
	void MyCamera::initCamera()
	{
		camOffset = btVector3(0, -2, 10);
		camVelocoty = btVector3(0, 0, 0);
		mCamera = mSceneMgr->createCamera("MainCamera");
		mCamera->setPosition (Ogre::Vector3(100,100,100));
		mCamera->lookAt (Ogre::Vector3(15, 3, -15));

		mCamera->setNearClipDistance(1);

		mViewport = mRenderWindow->addViewport(mCamera);
		mViewport->setBackgroundColour(ColourValue(0.0, 0.0, 0.0));

		camOrig = btVector3(1, 1, 1);
		camDistance = 10;
		camtype = -1;
	}
	// Setting mCamera->setAutoTracking(true, targer);
	void MyCamera::setTrackObj(btRaycastVehicle* _vehicle, Ogre::SceneNode* targer)
	{
		vehicle = _vehicle;
		camtype = 0;
		if(_vehicle != NULL && targer != NULL)
			mCamera->setAutoTracking(true, targer);
		else
		{
			mCamera->setAutoTracking(false);
			camtype = -1;
		}
	}
	void MyCamera::update(Ogre::Real deltat)
	{
		try
		{
			switch (camtype)
			{
			case 0:
				{
					btVector3 camOrig = - (vehicle->getRigidBody()->getLinearVelocity() + vehicle->getForwardVector()) / 25;					
					camOrig += vehicle->getChassisWorldTransform().getOrigin()  - quatRotate(vehicle->getChassisWorldTransform().getRotation(), camOffset);
					camOrig.setY(vehicle->getChassisWorldTransform().getOrigin().getY() - camOffset.getY());
					mCamera->setPosition(BtOgre::Convert::toOgre(camOrig));
					break;
				}
			case 1:
				{
					btTransform trans =	vehicle->getChassisWorldTransform();
					btVector3 newCamPos = trans.getOrigin() - camOrig * camDistance;
					mCamera->setPosition(BtOgre::Convert::toOgre(newCamPos));
					break;
				}
			}
		}
		catch(Ogre::Exception e)
		{
		}
	}
	void MyCamera::updateCamOrig(int x, int y, int z)
	{
		camOrig = camOrig.rotate(btVector3(0,1,0), x / (20 * SIMD_2_PI));
		camOrig = camOrig.rotate(btVector3(1,0,0), y / (20 * SIMD_2_PI));
		camDistance += - z / 60;
	}
}