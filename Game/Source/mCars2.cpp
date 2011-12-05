#include "Physics.h"
#include "mCars.h"
#include "CarEngine.h"
#include "AppManager.h"
#include "TireTracks.h"
#include "OgreMaxScene.hpp"
#include "tinyxml.h"

btRaycastVehicle* mCar::getBulletVehicle(){ return mVehicle->getBulletVehicle();}

mCar::mCar(DynamicsWorld* _mWorld, SceneManager* _mSceneMgr, AppManager* mngr)
{ 
	mNumEntitiesInstanced = 0;

	mWorld = _mWorld;
	mSceneMgr = _mSceneMgr;
	AppMngr = mngr;

	wheelInfo[0] = false;
	wheelInfo[1] = false;
	wheelInfo[2] = true;
	wheelInfo[3] = true;

	mBrake = false;
}

mCar::~mCar()
{
	delete tDecal;
	for(int i = 0; i < 4; i++)
		if(mWheelNodes[i])
		{
			mSceneMgr->destroyEntity(mWheels[i]);
			mWheelNodes[i]->removeAndDestroyAllChildren();	
		}
	mSceneMgr->destroySceneNode(mChassis);
	mWorld->getBulletDynamicsWorld()->removeRigidBody(mVehicle->getBulletVehicle()->getRigidBody());
	mWorld->getBulletDynamicsWorld()->removeVehicle(mVehicle->getBulletVehicle());
	delete engine;
	delete mVehicle->getBulletVehicle();
}

void mCar::loadTuning()
{

}
void mCar::loadWheels()
{
	mHandBrake = false;

	for (int i = 0; i < 4; i++)
	{
		mWheelsEngine[i] = 0;
		mWheelsSteerable[i] = 0;
	}
	mWheelsEngineCount = 2;
	mWheelsEngine[0] = 0;
	mWheelsEngine[1] = 1;
	mWheelsEngine[2] = 2;
	mWheelsEngine[3] = 3;

	mWheelsSteerableCount = 2;
	mWheelsSteerable[0] = 2;
	mWheelsSteerable[1] = 3;

	mWheelEngineStyle = 0;
	mWheelSteeringStyle = 0;

	mSteeringLeft = false;
	mSteeringRight = false;

	mSteering = 0;

	Ogre::Vector3 wheelDirectionCS0(0, -1, 0);
	Ogre::Vector3 wheelAxleCS(-1, 0, 0);

/*	mWheelNodes[0] = scene->GetSceneNode("leftfront", false);		
	mWheelNodes[1] = scene->GetSceneNode("rightfront", false);	
	mWheelNodes[2] = scene->GetSceneNode("leftback", false);		
	mWheelNodes[3] = scene->GetSceneNode("rightback", false);	*/

	using namespace OgreMax;
	OgreMaxScene* scene = new OgreMaxScene;

	for (size_t i = 0; i < 2; i++)
	{
		mWheelNodes[i] = mSceneMgr->getRootSceneNode()->createChildSceneNode ("bmw_m_fWheel_" + StringConverter::toString(i));
		scene->SetNamePrefix("bmw_m_fWheel_" + StringConverter::toString(i));
		scene->Load("../../Media/Wheels/lfWheel.scene", AppMngr->mRenderWindow, 0, mSceneMgr, mWheelNodes[i]);	
	}
	
	for (size_t i = 2; i < 4; i++)
	{
		mWheelNodes[i] = mSceneMgr->getRootSceneNode ()->createChildSceneNode ("bmw_m_bWheel_" + StringConverter::toString(i));		
		scene->SetNamePrefix("bmw_m_fWheel_" + StringConverter::toString(i));
		scene->Load("../../Media/Wheels/lbWheel.scene", AppMngr->mRenderWindow, 0, mSceneMgr, mWheelNodes[i]);	
	}
}
void mCar::loadChassis()
{
	using namespace OgreMax;
	const Ogre::Vector3 chassisShift(0, 1.0, 0);
	float connectionHeight = 0.7f;

	SceneNode *node = mSceneMgr->getRootSceneNode()->createChildSceneNode("Car");
	mChassis = node->createChildSceneNode("CarBody");

	OgreMaxScene* scene = new OgreMaxScene;
	scene->Load("../../Media/BMW 5/bmw_5.scene", AppMngr->mRenderWindow, 0, mSceneMgr, mChassis);

	BoxCollisionShape* chassisShape = new BoxCollisionShape(Ogre::Vector3(1.855/2, 1.374/4, 4.82/2));

	CompoundCollisionShape* compound = new CompoundCollisionShape();
	compound->addChildShape(chassisShape);
	mChassis->setPosition(0, -1.374/2, 0);

	mCarChassis = new WheeledRigidBody("carChassis", mWorld);

	mCarChassis->setShape (node, compound, 0.6, 0.6, 1600.f);
	mCarChassis->setDamping(0.2, 0.2);

	mCarChassis->disableDeactivation ();

	mTuning = new VehicleTuning(
		gSuspensionStiffness,
		gSuspensionCompression,
		gSuspensionDamping,
		gMaxSuspensionTravelCm,
		gFrictionSlip);

	mVehicleRayCaster = new VehicleRayCaster(mWorld);
	mVehicle = new RaycastVehicle(mCarChassis, mTuning, mVehicleRayCaster);

	int rightIndex = 0;
	int upIndex = 1;
	int forwardIndex = 2;

	mVehicle->setCoordinateSystem(0, 1, 2);
}


void mCar::AddCar()
{
	loadChassis();
	loadWheels();
	{
		bool isFrontWheel = false;
		Ogre::Vector3 wheelDirectionCS0(0, -1, 0);
		Ogre::Vector3 wheelAxleCS(-1, 0, 0);
		Ogre::Vector3 connectionPointCS0 (-0.754934, -0.2,-1.31589);

		mVehicle->addWheel(
			mWheelNodes[0],
			connectionPointCS0,
			wheelDirectionCS0,
			wheelAxleCS,
			gSuspensionRestLength,
			gWheelRadius,
			isFrontWheel, gWheelFriction, gRollInfluence);

		connectionPointCS0 = Ogre::Vector3(0.74964, -0.2,-1.31589);

		mVehicle->addWheel(
			mWheelNodes[1],
			connectionPointCS0,
			wheelDirectionCS0,
			wheelAxleCS,
			gSuspensionRestLength,
			gWheelRadius,
			isFrontWheel, gWheelFriction, gRollInfluence);

		connectionPointCS0 = Ogre::Vector3(-0.76963, -0.2, 1.55731);

		isFrontWheel = true;
		mVehicle->addWheel(
			mWheelNodes[2],
			connectionPointCS0,
			wheelDirectionCS0,
			wheelAxleCS,
			gSuspensionRestLength,
			gWheelRadius,
			isFrontWheel, gWheelFriction, gRollInfluence);

		connectionPointCS0 = Ogre::Vector3(0.76756, -0.2, 1.55731);

		mVehicle->addWheel(
			mWheelNodes[3],
			connectionPointCS0,
			wheelDirectionCS0,
			wheelAxleCS,
			gSuspensionRestLength,
			gWheelRadius,
			isFrontWheel, gWheelFriction, gRollInfluence);
	}

	engine = new CarEngine(mVehicle);
	tDecal = new TierTracks(mSceneMgr);
}

void mCar::update(Real time)
{
	engine->update(time);

	for(int i = 0; i < 4; i++)
	{		
		if(mBrake)
			mVehicle->getBulletVehicle()->setBrake(btScalar(50), i);
		else
			mVehicle->getBulletVehicle()->setBrake(btScalar(0), i);
	}
	if(mHandBrake)
		for(int i = 2; i < 4; i++)
  			mVehicle->getBulletVehicle()->getWheelInfo(i).m_deltaRotation = 0;
		
	if (mSteeringLeft)	
		mSteering += gSteeringClamp * time;	
	else if (mSteeringRight)
		mSteering -= gSteeringClamp * time;
	else if (mSteering < gSteeringIncrement && mSteering > -gSteeringIncrement)
		mSteering = 0;	
	else if(mSteering > 0)
		mSteering -= gSteeringIncrement;
	else if(mSteering < 0)
		mSteering += gSteeringIncrement;

	if (mSteering > gSteeringClamp)
		mSteering = gSteeringClamp;
	else if (mSteering < -gSteeringClamp)
		mSteering = -gSteeringClamp;		

	for (int i = 0; i < 4; i++)
	{
		if (mVehicle->getBulletVehicle()->getWheelInfo(i).m_bIsFrontWheel == true)
			mVehicle->setSteeringValue (mSteering, i);			
	}	
	tDecal->update(mVehicle->getBulletVehicle());
}

void mCar::steerLeft()		{mSteeringLeft = true;}
void mCar::steerRight()		{mSteeringRight = true;}
void mCar::stopSteering()	{mSteeringRight = false; mSteeringLeft = false;}
void mCar::Acselerate()		{engine->setThrottle(1.);}
void mCar::stopAcselerate()	{engine->setThrottle(0.);}
void mCar::Brake()			{mBrake = true;}
void mCar::stopBrake()		{mBrake = false;}
void mCar::HandeBrake()		{mHandBrake = true;}
void mCar::stopHandeBrake()	{mHandBrake = false;}