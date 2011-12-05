#ifndef MYCARS_H
#define MYCARS_H

#ifndef _PRECOMP
#define _PRECOMP
#endif

#ifndef CUBE_HALF_EXTENTS
#define CUBE_HALF_EXTENTS 1
#endif

#include "stdafx.h"
#include "VehicleImprovement.h"

using namespace Ogre;

namespace RacingGame
{
	class mVehicleOptions;
	class CarEngine;
	class TierTracks;


	class mCar : public UpdatableObject
	{
	public:
		mCar(btDiscreteDynamicsWorld* _mWorld, SceneManager* _mSceneMgr, AppManager* mngr);
		~mCar();

		void AddCar(std::string namePrefix, mVehicleOptions* opt);

		void update(Real deltat);
		void steerLeft();
		void steerRight();
		void stopSteering();
		void Acselerate();
		void Brake();
		void stopAcselerate();
		void stopBrake();
		void HandeBrake();
		void stopHandeBrake();

		btRaycastVehicle* getVehicle() {return mVehicle;}

		CarEngine* getEngine(){ return engine;}
		btScalar getSpeed(){return mVehicle->getCurrentSpeedKmHour();}
		SceneNode* getOgreChassis(){ return mChassis;}

		mVehicleOptions* CarOptions;
	protected:

		void loadTuning();
		void loadWheels();
		void loadChassis();

		CarEngine* engine;
		size_t mNumEntitiesInstanced;

		TierTracks* tDecal;

		Ogre::SceneManager* mSceneMgr;
		btDiscreteDynamicsWorld* mWorld;
		AppManager* AppMngr;

		//OgreBulletDynamics::WheeledRigidBody        *mCarChassis;
		//OgreBulletDynamics::VehicleTuning	        *mTuning;
		//OgreBulletDynamics::VehicleRayCaster	    *mVehicleRayCaster;
		//OgreBulletDynamics::RaycastVehicle	        *mVehicle;

		Ogre::SceneNode *mChassis;
		Ogre::Entity    *mWheels[4];
		Ogre::SceneNode *mWheelNodes[4];
		Ogre::SceneNode *TireTracksNode;
		btQuaternion	mWheelAddRottation[4];

		btRigidBody* btChassis;
		btRaycastVehicle::btVehicleTuning	mTuning;
		btVehicleRaycaster*	mVehicleRayCaster;
		btRaycastVehicle*	mVehicle;
		btCollisionShape*	mWheelShape;

		MyFrictionConstraint* mFrictionConstraint;

		int mWheelsEngine[4];
		int mWheelsEngineCount;
		int mWheelsSteerable[4];
		int mWheelsSteerableCount;

		bool wheelInfo[4];

		float mSteering;

		int mWheelEngineStyle;
		int mWheelSteeringStyle;

		bool mSteeringLeft;
		bool mSteeringRight;

		bool mBrake;
		bool mHandBrake;

		btScalar steeringClamp;
		btScalar steeringIncrement;
		btScalar maxBreakingForce;
	};
}

#endif