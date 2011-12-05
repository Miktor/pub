#ifndef _CAMERA_H_
#define _CAMERA_H_

#include "stdafx.h"

using namespace Ogre;

#define CamTypeCount 1

namespace RacingGame
{
	class MyCamera : public UpdatableObject
	{
	public:
		MyCamera(SceneManager* mSM, RenderWindow* mRW)
		{
			mSceneMgr = mSM;
			mRenderWindow = mRW;

			camtype = 0;
		}
		~MyCamera()
		{
			mSceneMgr->destroyAllCameras();
		}

		void initCamera();
		void changeCamType()
		{
			camtype++;
			if (camtype > CamTypeCount + 1)
				camtype = 0;
		}
		void setTrackObj(btRaycastVehicle* _vehicle, Ogre::SceneNode* target);
		void update(Ogre::Real time);
		void updateCamOrig(int x, int y, int z);

	private:

		SceneManager* mSceneMgr;
		RenderWindow* mRenderWindow;

		Ogre::Camera* mCamera;

		Ogre::Viewport* mViewport;

		SceneNode* target;
		btRaycastVehicle* vehicle;

		btVector3 camOrig;
		int camDistance;
		btVector3 camVelocoty;

		int camtype;
		btVector3 camOffset;
		unsigned long distanse;
	};
}

#endif