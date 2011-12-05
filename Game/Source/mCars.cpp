#include "mCars.h"
#include "TireTracks.h"
#include "CarEngine.h"
#include "AppManager.h"
#include "vehicleOptions.h"

namespace RacingGame
{
	btVector3 wheelDirectionCS0(0,-1,0);
	btVector3 wheelAxleCS(-1,0,0);

	mCar::mCar(btDiscreteDynamicsWorld* _mWorld, SceneManager* _mSceneMgr, AppManager* mngr) : mWorld(_mWorld), mSceneMgr(_mSceneMgr), AppMngr(mngr),
				mNumEntitiesInstanced(0), mBrake(false), mSteering(0), mWheelEngineStyle(0), mWheelSteeringStyle(0),	mSteeringLeft(false), mSteeringRight(false), mHandBrake(false)
	{
		wheelInfo[0] = false;
		wheelInfo[1] = false;
		wheelInfo[2] = true;
		wheelInfo[3] = true;
	}

	mCar::~mCar()
	{
		delete tDecal;
		for(int i = 0; i < 4; i++)
			if(mWheelNodes[i])
			{
				mWheelNodes[i]->removeAndDestroyAllChildren();
				mSceneMgr->destroySceneNode(mWheelNodes[i]);
			}
		mSceneMgr->destroySceneNode(mChassis);

		mWorld->removeRigidBody(btChassis);
		mWorld->removeVehicle(mVehicle);

		delete btChassis->getMotionState();

		delete engine;
		delete mVehicle;
		delete btChassis;
		delete mVehicleRayCaster;
		delete mWheelShape;
	}

	void mCar::AddCar(std::string namePrefix, mVehicleOptions* opt)
	{
		CarOptions = opt;

		steeringClamp = CarOptions->floatOpt["steeringClamp"];
		steeringIncrement = CarOptions->floatOpt["steeringIncrement"];
		maxBreakingForce = CarOptions->floatOpt["maxBreakingForce"];

		btCollisionShape* chassisShape = new btBoxShape(CarOptions->vectorOpt["extensons"] / 2);
		//m_collisionShapes.push_back(chassisShape);

		btCompoundShape* compound = new btCompoundShape();
		//m_collisionShapes.push_back(compound);
		btTransform localTrans;
		localTrans.setIdentity();
		//localTrans effectively shifts the center of mass with respect to the chassis
		localTrans.setOrigin(CarOptions->vectorOpt["CMOffset"]);

		compound->addChildShape(localTrans, chassisShape);

		btTransform tr;
		tr.setIdentity();
		tr.setOrigin(btVector3(0,0.f,0));

		//chassisShape);
		//btChassis->setDamping(0.2,0.2);

		mChassis = mSceneMgr->getRootSceneNode()->createChildSceneNode(namePrefix + "CarBody");

		OgreMax::OgreMaxScene* scene = new OgreMax::OgreMaxScene;
		scene->SetNamePrefix(namePrefix);
		scene->Load(CarOptions->stringOpt["BodyMesh"], AppMngr->mRenderWindow, 0, mSceneMgr, mChassis);

		btVector3 localInertia(0,0,0);
		compound->calculateLocalInertia(CarOptions->floatOpt["mass"], localInertia);
		btChassis = new btRigidBody(CarOptions->floatOpt["mass"], 0, compound, localInertia);
		btChassis->setWorldTransform(tr);
		// offset
		btChassis->setMotionState(new BtOgre::RigidBodyState(mChassis, btTransform::getIdentity(), btTransform( btQuaternion::getIdentity(), btVector3(0, -0.2, 1)))); 
		mWorld->addRigidBody(btChassis);

		mWheelShape = new btCylinderShapeX(btVector3(CarOptions->floatOpt["wheelWidth"], CarOptions->floatOpt["wheelRadius"], CarOptions->floatOpt["wheelRadius"]) / 2);

		/// create vehicle

		mVehicleRayCaster = new MyRayCaster(mWorld);
		mVehicle = new btRaycastVehicle(mTuning, btChassis, mVehicleRayCaster);

		///never deactivate the vehicle
		btChassis->setActivationState(DISABLE_DEACTIVATION);

		mWorld->addVehicle(mVehicle);		

		bool isFrontWheel=true;

		//choose coordinate system
		mVehicle->setCoordinateSystem(CarOptions->floatOpt["rightIndex"],CarOptions->floatOpt["upIndex"],CarOptions->floatOpt["forwardIndex"]);

		// Left Forward Wheel
		Ogre::Entity* wEnt = mSceneMgr->createEntity(namePrefix + "whelLF", CarOptions->stringOpt["FWheelMesh"]);
		mWheelNodes[0] = mSceneMgr->getRootSceneNode()->createChildSceneNode(namePrefix + "whelLFNode");
		mWheelNodes[0]->attachObject(wEnt);

		//btVector3 connectionPointCS0(CUBE_HALF_EXTENTS-(0.3*(*CarOptions->optMap)["wheelWidth"]),connectionHeight,2*CUBE_HALF_EXTENTS-(*CarOptions->optMap)["wheelRadius"]);
		mVehicle->addWheel(CarOptions->vectorOpt["LFWconnectionPoint"],wheelDirectionCS0,wheelAxleCS,CarOptions->floatOpt["suspensionRestLength"],CarOptions->floatOpt["wheelRadius"],mTuning,isFrontWheel);

		// Right Forward Wheel
		wEnt = mSceneMgr->createEntity(namePrefix + "whelRF", CarOptions->stringOpt["FWheelMesh"]);
		mWheelNodes[1] = mSceneMgr->getRootSceneNode()->createChildSceneNode(namePrefix + "whelRFNode");
		mWheelNodes[1]->attachObject(wEnt);

		//connectionPointCS0 = btVector3(-CUBE_HALF_EXTENTS+(0.3*(*CarOptions->optMap)["wheelWidth"]),connectionHeight,2*CUBE_HALF_EXTENTS-(*CarOptions->optMap)["wheelRadius"]);
		mVehicle->addWheel(CarOptions->vectorOpt["RFWconnectionPoint"],wheelDirectionCS0,wheelAxleCS,CarOptions->floatOpt["suspensionRestLength"],CarOptions->floatOpt["wheelRadius"],mTuning,isFrontWheel);

		wEnt = mSceneMgr->createEntity(namePrefix + "whelLR", CarOptions->stringOpt["RWheelMesh"]);
		mWheelNodes[2] = mSceneMgr->getRootSceneNode()->createChildSceneNode(namePrefix + "whelLRNode");
		mWheelNodes[2]->attachObject(wEnt);

		//connectionPointCS0 = btVector3(-CUBE_HALF_EXTENTS+(0.3*(*CarOptions->optMap)["wheelWidth"]),connectionHeight,-2*CUBE_HALF_EXTENTS+(*CarOptions->optMap)["wheelRadius"]);
		isFrontWheel = false;
		mVehicle->addWheel(CarOptions->vectorOpt["LRWconnectionPoint"],wheelDirectionCS0,wheelAxleCS,CarOptions->floatOpt["suspensionRestLength"],CarOptions->floatOpt["wheelRadius"],mTuning,isFrontWheel);

		wEnt = mSceneMgr->createEntity(namePrefix + "whelRR", CarOptions->stringOpt["RWheelMesh"]);
		mWheelNodes[3] = mSceneMgr->getRootSceneNode()->createChildSceneNode(namePrefix + "whelRRNode");
		mWheelNodes[3]->attachObject(wEnt);

		//connectionPointCS0 = btVector3(CUBE_HALF_EXTENTS-(0.3*(*CarOptions->optMap)["wheelWidth"]),connectionHeight,-2*CUBE_HALF_EXTENTS+(*CarOptions->optMap)["wheelRadius"]);
		mVehicle->addWheel(CarOptions->vectorOpt["RRWconnectionPoint"],wheelDirectionCS0,wheelAxleCS,CarOptions->floatOpt["suspensionRestLength"],CarOptions->floatOpt["wheelRadius"],mTuning,isFrontWheel);

		for (int i=0;i<mVehicle->getNumWheels();i++)
		{
			mWheelAddRottation[i] = btQuaternion (btVector3(0, 0, 1), btScalar(SIMD_PI) * (i % 2 + 1));
			mWheelAddRottation[i].normalize();

			btWheelInfo& wheel = mVehicle->getWheelInfo(i);
			wheel.m_suspensionStiffness = CarOptions->floatOpt["suspensionStiffness"];
			wheel.m_wheelsDampingRelaxation = CarOptions->floatOpt["suspensionDamping"];
			wheel.m_wheelsDampingCompression = CarOptions->floatOpt["suspensionCompression"];
			wheel.m_frictionSlip = CarOptions->floatOpt["wheelFriction"];
			wheel.m_rollInfluence = CarOptions->floatOpt["rollInfluence"];
		}
		
		mFrictionConstraint = new MyFrictionConstraint(*btChassis);
		mFrictionConstraint->setVehicle(mVehicle);
		mWorld->addConstraint(mFrictionConstraint);
		mFrictionConstraint->setDbgDrawSize(btScalar(5.f));

		engine = new CarEngine(mVehicle);

		TireTracksNode = mSceneMgr->getRootSceneNode()->createChildSceneNode(namePrefix + "_TireTracks");
		tDecal = new TierTracks(TireTracksNode);
		
	}

	void mCar::update(Real time)
	{
		engine->update(time);

		if(mHandBrake)
			for(int i = 2; i < 4; i++)
	  			mVehicle->getWheelInfo(i).m_deltaRotation = 0;

		if (mSteeringLeft)
			mSteering += steeringClamp * time;
		else if (mSteeringRight)
			mSteering -= steeringClamp * time;
		else if (mSteering < steeringIncrement && mSteering > -steeringIncrement)
			mSteering = 0;
		else if(mSteering > 0)
			mSteering -= steeringIncrement;
		else if(mSteering < 0)
			mSteering += steeringIncrement;

		if (mSteering > steeringClamp)
			mSteering = steeringClamp;
		else if (mSteering < -steeringClamp)
			mSteering = -steeringClamp;

		for (int i = 0; i < mVehicle->getNumWheels(); i++)
		{
			if(mBrake)
				mVehicle->setBrake(btScalar(maxBreakingForce), i);
			else
				mVehicle->setBrake(btScalar(0), i);

			if (mVehicle->getWheelInfo(i).m_bIsFrontWheel == true)
				mVehicle->setSteeringValue (mSteering, i);
			else if(mHandBrake)
				mVehicle->setBrake(btScalar(maxBreakingForce * 2), i);

			mVehicle->updateWheelTransform(i,true);

			btQuaternion rot = mVehicle->getWheelInfo(i).m_worldTransform.getRotation();
			btVector3 pos = mVehicle->getWheelInfo(i).m_worldTransform.getOrigin();

			btQuaternion axelRot = btQuaternion(quatRotate(rot, btVector3(1, 0, 0)), mVehicle->getWheelInfo(i).m_rotation);
			axelRot = axelRot * rot;
			mWheelNodes[i]->setOrientation(BtOgre::Convert::toOgre(axelRot * mWheelAddRottation[i]));
			mWheelNodes[i]->setPosition(BtOgre::Convert::toOgre(pos));
		}

		tDecal->update(mVehicle);
		TireTracksNode->needUpdate(true);
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
}