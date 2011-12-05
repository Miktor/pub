#include "MyCars.h"

mCar::mCar(Physics* _physics)
{ 
	mNumEntitiesInstanced = 0;

	mPhysics = _physics;
	mWorld = mPhysics->mWorld;

	mSceneMgr = mPhysics->mSceneMgr;
	AppMngr = mPhysics->AppMngr;

	wheelInfo[0] = true;
	wheelInfo[1] = true;
	wheelInfo[2] = false;
	wheelInfo[3] = false;

	mBrake = false;
}

void mCar::AddCar()
{	
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
	mWheelsSteerable[0] = 0;
	mWheelsSteerable[1] = 1;

	mWheelEngineStyle = 0;
	mWheelSteeringStyle = 0;

	mSteeringLeft = false;
	mSteeringRight = false;

	mEngineForce = 0;
	mSteering = 0;

	{
		const Ogre::Vector3 chassisShift(0, 1.0, 0);
		float connectionHeight = 0.7f;

		mChassis = mSceneMgr->createEntity(
			"chassis" + StringConverter::toString(mCar::mNumEntitiesInstanced++),
			"chassis.mesh");

		SceneNode *node = mSceneMgr->getRootSceneNode ()->createChildSceneNode ();

		SceneNode *chassisnode = node->createChildSceneNode ();
		chassisnode->attachObject (mChassis);
		chassisnode->setPosition (chassisShift);		

		mChassis->setQueryFlags (GEOMETRY_QUERY_MASK);
		mChassis->setCastShadows(true);



		BoxCollisionShape* chassisShape = new BoxCollisionShape(Ogre::Vector3(1.f,0.75f,2.1f));
		CompoundCollisionShape* compound = new CompoundCollisionShape();
		compound->addChildShape(chassisShape, chassisShift); 

		mCarChassis = new WheeledRigidBody("carChassis", mWorld);

		mCarChassis->setShape (node, compound, 0.6, 0.6, 800, CarPosition, Quaternion::IDENTITY);
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

		{
			int rightIndex = 0;
			int upIndex = 1;
			int forwardIndex = 2;

			mVehicle->setCoordinateSystem(rightIndex, upIndex, forwardIndex);

			Ogre::Vector3 wheelDirectionCS0(0,-1,0);
			Ogre::Vector3 wheelAxleCS(-1,0,0);

			for (size_t i = 0; i < 4; i++)
			{
				mWheels[i] = mSceneMgr->createEntity(
					"wheel" + StringConverter::toString(car::mNumEntitiesInstanced++),
					"wheel.mesh");

				mWheels[i]->setQueryFlags (GEOMETRY_QUERY_MASK);
				mWheels[i]->setCastShadows(true);

				mWheelNodes[i] = mSceneMgr->getRootSceneNode ()->createChildSceneNode ();
				mWheelNodes[i]->attachObject (mWheels[i]);

			}

			{
				bool isFrontWheel = true;

				Ogre::Vector3 connectionPointCS0 (
					CUBE_HALF_EXTENTS-(0.3*gWheelWidth),
					connectionHeight,
					2*CUBE_HALF_EXTENTS-gWheelRadius);


				mVehicle->addWheel(
					mWheelNodes[0],
					connectionPointCS0,
					wheelDirectionCS0,
					wheelAxleCS,
					gSuspensionRestLength,
					gWheelRadius,
					isFrontWheel, gWheelFriction, gRollInfluence);
				mVehicle->getBulletVehicle()->getWheelInfo(0).m_clientInfo = &wheelInfo[0];

				connectionPointCS0 = Ogre::Vector3(
					-CUBE_HALF_EXTENTS+(0.3*gWheelWidth),
					connectionHeight,
					2*CUBE_HALF_EXTENTS-gWheelRadius);


				mVehicle->addWheel(
					mWheelNodes[1],
					connectionPointCS0,
					wheelDirectionCS0,
					wheelAxleCS,
					gSuspensionRestLength,
					gWheelRadius,
					isFrontWheel, gWheelFriction, gRollInfluence);
				mVehicle->getBulletVehicle()->getWheelInfo(1).m_clientInfo = &wheelInfo[1];


				connectionPointCS0 = Ogre::Vector3(
					-CUBE_HALF_EXTENTS+(0.3*gWheelWidth),
					connectionHeight,
					-2*CUBE_HALF_EXTENTS+gWheelRadius);

				isFrontWheel = false;
				mVehicle->addWheel(
					mWheelNodes[2],
					connectionPointCS0,
					wheelDirectionCS0,
					wheelAxleCS,
					gSuspensionRestLength,
					gWheelRadius,
					isFrontWheel, gWheelFriction, gRollInfluence);
				mVehicle->getBulletVehicle()->getWheelInfo(2).m_clientInfo = &wheelInfo[2];

				connectionPointCS0 = Ogre::Vector3(
					CUBE_HALF_EXTENTS-(0.3*gWheelWidth),
					connectionHeight,
					-2*CUBE_HALF_EXTENTS+gWheelRadius);

				mVehicle->addWheel(
					mWheelNodes[3],
					connectionPointCS0,
					wheelDirectionCS0,
					wheelAxleCS,
					gSuspensionRestLength,
					gWheelRadius,
					isFrontWheel, gWheelFriction, gRollInfluence);
				mVehicle->getBulletVehicle()->getWheelInfo(3).m_clientInfo = &wheelInfo[3];

				//mVehicle->setWheelsAttached();
			}
		}
	}
	engine = new CarEngine(mVehicle);
	AppMngr->mCamera->setTrackObj(mCarChassis->getSceneNode(), mVehicle->getBulletVehicle());
	/*btTypedConstraint::btConstraintInfo2* info = new btTypedConstraint::btConstraintInfo2();
	getInfo2(info);*/
}


void mCar::getInfo2(btTypedConstraint::btConstraintInfo2* info)
{
	const btRigidBody* chassis = mVehicle->getBulletVehicle()->getRigidBody();

	int row = 0;
	// Setup sideways friction.
	for (int i = 0; i < mVehicle->getBulletVehicle()->getNumWheels(); ++i)
	{
		const btWheelInfo& wheel_info = mVehicle->getBulletVehicle()->getWheelInfo(i);

		// Only if the wheel is on the ground:
		if (!wheel_info.m_raycastInfo.m_groundObject)
			continue;

		int row_index = row++ * info->rowskip;

		// Set axis to be the direction of motion:
		const btVector3& axis = wheel_info.m_raycastInfo.m_wheelAxleWS;
		info->m_J1linearAxis[row_index] = axis.length();

		// Set angular axis.
		btVector3 rel_pos = wheel_info.m_raycastInfo.m_contactPointWS - chassis->getCenterOfMassPosition();
		info->m_J1angularAxis[row_index] = rel_pos.cross(axis).length();

		// Set constraint error (target relative velocity = 0.0)
		info->m_constraintError[row_index] = 0.0f;

		info->cfm[row_index] = 0; // Set constraint force mixing

		// Set maximum friction force according to Coulomb's law
		// Substitute Pacejka here
		btScalar max_friction = wheel_info.m_wheelsSuspensionForce * wheel_info.m_frictionSlip / info->fps;
		// Set friction limits.
		info->m_lowerLimit[row_index] = -max_friction;
		info->m_upperLimit[row_index] =  max_friction;
	}

	// Setup forward friction.
	for (int i = 0; i < mVehicle->getBulletVehicle()->getNumWheels(); ++i)
	{
		const btWheelInfo& wheel_info = mVehicle->getBulletVehicle()->getWheelInfo(i);

		// Only if the wheel is on the ground:
		if (!wheel_info.m_raycastInfo.m_groundObject)
			continue;

		int row_index = row++ * info->rowskip;

		// Set axis to be the direction of motion:
		btVector3 axis = wheel_info.m_raycastInfo.m_wheelAxleWS.cross(wheel_info.m_raycastInfo.m_wheelDirectionWS);
		info->m_J1linearAxis[row_index] = axis.length();

		// Set angular axis.
		btVector3 rel_pos = wheel_info.m_raycastInfo.m_contactPointWS - chassis->getCenterOfMassPosition();
		info->m_J1angularAxis[row_index] = rel_pos.cross(axis).length();

		// FIXME: Calculate the speed of the contact point on the wheel spinning.
		//        Estimate the wheel's angular velocity = m_deltaRotation
		btScalar wheel_velocity = wheel_info.m_deltaRotation * wheel_info.m_wheelsRadius;
		// Set constraint error (target relative velocity = 0.0)
		info->m_constraintError[row_index] = wheel_velocity;

		info->cfm[row_index] = 0; // Set constraint force mixing

		// Set maximum friction force
		btScalar max_friction = wheel_info.m_wheelsSuspensionForce * wheel_info.m_frictionSlip / info->fps;
		// Set friction limits.
		info->m_lowerLimit[row_index] = -max_friction;
		info->m_upperLimit[row_index] =  max_friction;
	}
}

void mCar::update(Real time)
{
	//if(mAcselerate)
	//	mEngineForce = gMaxEngineForce;
	//else if(mBrake)
	//	mEngineForce -= gMaxEngineForce / 2;
	//else
	//	mEngineForce = 0;

	//for (int i = mWheelsEngine[0]; i < mWheelsEngineCount; i++)
	//{
	//	mVehicle->applyEngineForce (mEngineForce, mWheelsEngine[i]);
	//}

	engine->update();

	if (mBrake)
		for(int i = 0; i < mVehicle->getBulletVehicle()->getNumWheels(); i++)
			mVehicle->getBulletVehicle()->setBrake(btScalar(100), i);
	else
		for(int i = 0; i < mVehicle->getBulletVehicle()->getNumWheels(); i++)
			mVehicle->getBulletVehicle()->setBrake(btScalar(0), i);

	
	if (mSteeringLeft)	
		mSteering += gSteeringIncrement;	
	else if (mSteeringRight)
		mSteering -= gSteeringIncrement;
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

	// apply Steering on relevant wheels
	for (int i = mWheelsSteerable[0]; i < mWheelsSteerableCount; i++)
	{
		if (i < 2)
			mVehicle->setSteeringValue (mSteering, mWheelsSteerable[i]);
		else
			mVehicle->setSteeringValue (-mSteering, mWheelsSteerable[i]);
	}	
}

void mCar::steerLeft(){mSteeringLeft = true;}
void mCar::steerRight(){mSteeringRight = true;}
void mCar::stopSteering(){mSteeringRight = false; mSteeringLeft = false;}
void mCar::Acselerate(){engine->Throttle = 1.;}
void mCar::stopAcselerate(){engine->Throttle = 0.;}
void mCar::Brake(){mBrake = true;}
void mCar::stopBrake(){mBrake = false;}