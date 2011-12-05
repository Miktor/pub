#ifndef CAR_H
#define CAR_H

#ifndef _PRECOMP
#define _PRECOMP
#endif 

#ifndef CUBE_HALF_EXTENTS
#define CUBE_HALF_EXTENTS 1
#endif

#include "Ogre.h"
#include "AppManager.h"
#include <OgreBulletDynamics.h>
#include "Physics.h"
#include "CarEngine.h"

static float	gMaxEngineForce = 3000.f;

static float	gSteeringIncrement = 0.01f;
static float	gSteeringClamp = 0.5f;

static float	gWheelRadius = 0.5f;
static float	gWheelWidth = 0.4f;

static float	gWheelFriction = 10.f;//1000;//1e30f;
static float	gSuspensionStiffness = 20.f;
static float	gSuspensionDamping = 2.3f;
static float	gSuspensionCompression = 4.4f;

static float	gRollInfluence = 0.1f;//1.0f;
static float    gSuspensionRestLength = 0.6;
static float    gMaxSuspensionTravelCm = 500.0;
static float    gFrictionSlip = 1.5;

static const Ogre::Vector3   CarPosition             = Ogre::Vector3(15, 3,-15);

class CarEngine;

class car
{
public:
	car(Physics *_physics);
	~car()
	{
		if
	}

	void AddCar();

	void getInfo2(btTypedConstraint::btConstraintInfo2* info);

	void update(Real time);

	void steerLeft();
	void steerRight();
	void stopSteering();
	void Acselerate();
	void Brake();
	void stopAcselerate();
	void stopBrake();

	float mEngineForce;

	CarEngine* engine;
private:
	size_t mNumEntitiesInstanced; 

	AppManager *AppMngr;
	Ogre::SceneManager* mSceneMgr;

	Physics* mPhysics;
	DynamicsWorld* mWorld;

	OgreBulletDynamics::WheeledRigidBody        *mCarChassis;
	OgreBulletDynamics::VehicleTuning	        *mTuning;
	OgreBulletDynamics::VehicleRayCaster	    *mVehicleRayCaster;
	OgreBulletDynamics::RaycastVehicle	        *mVehicle;

	Ogre::Entity    *mChassis;
	Ogre::Entity    *mWheels[4];
	Ogre::SceneNode *mWheelNodes[4];


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

	bool mAcselerate;
	bool mBrake;
};

#endif