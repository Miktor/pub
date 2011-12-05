#ifndef vehicleOptions_H_
#define vehicleOptions_H_

#include "stdafx.h"

#include "LinearMath/btScalar.h"
#include "LinearMath/btVector3.h"
#include <map>
#include <string>

namespace RacingGame
{
	class mVehicleOptions
	{
	public:
		mVehicleOptions ()
			:maxBreakingForce(100.f),
			gVehicleSteering(0.f),
			steeringIncrement(0.1f),
			steeringClamp(0.4f),
			wheelRadius(0.3f),
			wheelWidth(0.4f),
			wheelFriction(1.5f),
			suspensionStiffness(40.f),
			suspensionDamping(.4f),
			suspensionCompression(.5f),
			rollInfluence(0.1f),
			suspensionRestLength(1.f),
			connectionHeight(1.2f),
			mass(1645), extensons(1.985, 1.395, 4.580), CMOffset(0, 0.8, 0),
			rightIndex(0), upIndex(1), forwardIndex(2),
			BodyMesh("../../Media/BMW M3 E92/bmw_m3_e92.scene"),
			FWheelMesh("wheelF.mesh"), RWheelMesh("wheelR.mesh")
		{
			floatOpt = floatMap();
			stringOpt = stringMap();
			vectorOpt = vector3Map();
			intOpt = intMap();

			floatOpt["maxBreakingForce"]		 = maxBreakingForce;

			floatOpt["gVehicleSteering"]		 = gVehicleSteering;
			floatOpt["steeringIncrement"]		 = steeringIncrement;
			floatOpt["steeringClamp"]			 = steeringClamp;
			floatOpt["wheelRadius"]				 = wheelRadius;
			floatOpt["wheelWidth"]				 = wheelWidth;
			floatOpt["wheelFriction"]			 = wheelFriction;
			floatOpt["suspensionStiffness"]		 = suspensionStiffness;
			floatOpt["suspensionDamping"]		 = suspensionDamping;
			floatOpt["suspensionCompression"]	 = suspensionCompression;
			floatOpt["rollInfluence"]			 = rollInfluence;
			floatOpt["suspensionRestLength"]	 = suspensionRestLength;
			floatOpt["mass"]					 = mass;
			intOpt["rightIndex"]				 = rightIndex ;
			intOpt["upIndex"]					 = upIndex ;
			intOpt["forwardIndex"]			 = forwardIndex ;

			LFWconnectionPoint = btVector3(0.782f, connectionHeight, 1.5f);
			RFWconnectionPoint = btVector3(-0.782f, connectionHeight, 1.5f);
			LRWconnectionPoint = btVector3(0.763f, connectionHeight, -1.247f);
			RRWconnectionPoint = btVector3(-0.763f, connectionHeight, -1.247f);

			vectorOpt["extensons"]			 = extensons;
			vectorOpt["CMOffset"]			 = CMOffset;
			vectorOpt["LFWconnectionPoint"]	 = LFWconnectionPoint;
			vectorOpt["RFWconnectionPoint"]	 = RFWconnectionPoint;
			vectorOpt["LRWconnectionPoint"]	 = LRWconnectionPoint;
			vectorOpt["RRWconnectionPoint"]	 = RRWconnectionPoint;

			stringOpt["BodyMesh"]	= BodyMesh;
			stringOpt["FWheelMesh"] = FWheelMesh;
			stringOpt["RWheelMesh"] = RWheelMesh;
		}

		~mVehicleOptions();

		floatMap	floatOpt;
		stringMap	stringOpt;
		vector3Map	vectorOpt;
		intMap		intOpt;

		float	maxBreakingForce;

		float	gVehicleSteering;
		float	steeringIncrement;
		float	steeringClamp;
		float	wheelRadius;
		float	wheelWidth;
		float	wheelFriction;
		float	suspensionStiffness;
		float	suspensionDamping;
		float	suspensionCompression;
		float	rollInfluence;
		float	suspensionRestLength;
		float	connectionHeight;
		float	mass;
		int		rightIndex;
		int		upIndex;
		int		forwardIndex;

		btVector3 wheelDirectionCS0;
		btVector3 wheelAxleCS;

		btVector3 extensons;
		btVector3 CMOffset;
		btVector3 LFWconnectionPoint;
		btVector3 RFWconnectionPoint;
		btVector3 LRWconnectionPoint;
		btVector3 RRWconnectionPoint;

		std::string BodyMesh;
		std::string FWheelMesh;
		std::string RWheelMesh;
	};
}

#endif