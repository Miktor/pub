#ifndef _VehicleImprovement_H_
#define _VehicleImprovement_H_

#include "btBulletDynamicsCommon.h"
#include "MyUtils.h"

namespace RacingGame
{
#define WHEEL_FRICTION_CFM 0.1

	struct  MyClosestRayResultCallback : public btCollisionWorld::ClosestRayResultCallback
	{
		const btCollisionShape * m_hitTriangleShape;
		int                 m_hitTriangleIndex;
		int                 m_hitShapePart;


		MyClosestRayResultCallback (const btVector3 & rayFrom,const btVector3 & rayTo)
			: btCollisionWorld::ClosestRayResultCallback(rayFrom, rayTo),
			m_hitTriangleShape(NULL),
			m_hitTriangleIndex(0),
			m_hitShapePart(0)
		{
		}

		virtual ~MyClosestRayResultCallback()
		{
		}

		virtual btScalar addSingleResult(btCollisionWorld::LocalRayResult & rayResult, bool normalInWorldSpace)
		{
			if (rayResult.m_localShapeInfo)
			{
				m_hitTriangleShape = rayResult.m_collisionObject->getCollisionShape();
				m_hitTriangleIndex = rayResult.m_localShapeInfo->m_triangleIndex;
				m_hitShapePart = rayResult.m_localShapeInfo->m_shapePart;
				
			} else 
			{
				m_hitTriangleShape = NULL;
				m_hitTriangleIndex = 0;
				m_hitShapePart = 0;
			}
			return ClosestRayResultCallback::addSingleResult(rayResult,normalInWorldSpace);
		}
	};

	class MyRayCaster : public btVehicleRaycaster
	{
		btDynamicsWorld*	m_dynamicsWorld;
	public:
		MyRayCaster(btDynamicsWorld* world):m_dynamicsWorld(world)
		{
		}

		/// Compute the Barycentric coordinates of position inside triangle p1, p2, p3
		static btVector3 BarycentricCoordinates(const btVector3& position, const btVector3& p1, const btVector3& p2, const btVector3& p3)
		{
			btVector3 edge1 = p2 - p1;
			btVector3 edge2 = p3 - p1;

			// Area of triangle ABC
			btScalar p1p2p3 = edge1.cross(edge2).length2();
			// Area of BCP
			btScalar p2p3p = (p3 - p2).cross(position - p2).length2();
			// Area of CAP
			btScalar p3p1p = edge2.cross(position - p3).length2();

			btScalar s = btSqrt(p2p3p / p1p2p3);
			btScalar t = btSqrt(p3p1p / p1p2p3);
			btScalar w = 1.0f - s - t;

			//#ifdef BUILD_DEBUG
			//              // Unit test...
			//              btVector3 regen_position = s * p1 + t * p2 + w * p3;
			//              btAssert((regen_position - position).length2() < 0.0001f);
			//#endif

			return btVector3(s, t, w);
		}

		// shape, subpart and triangle come from the ray callback.
		// transform is the mesh shape's world transform
		// position is the world space hit point of the ray
		static btVector3 InterpolateMeshNormal( const btTransform& transform, btCollisionShape* shape, int subpart, int triangle, btVector3& position )
		{
			
			// Get the geometry from somewhere...
			btVector3* normal = new btVector3[3];
			btVector3* positions = new btVector3[3];
			unsigned short* indices = new unsigned short[3];

			MyUtils::getTriangleConers(shape, subpart, positions, normal, indices);

			unsigned int i = indices[0], j = indices[1], k = indices[2];

			btVector3 barry = BarycentricCoordinates(transform.invXform(position), positions[ i ], positions[ j ], positions[ k ]);

			// Interpolate from barycentric coordinates
			btVector3 result = barry.x() * normal[i] + barry.y() * normal[j] + barry.z() * normal[k];

			// Transform back into world space
			result = transform.getBasis() * result;
			result.normalize();
			return result;
		}

		void* castRay(const btVector3& from,const btVector3& to, btVehicleRaycasterResult& result)
		{
			//	RayResultCallback& resultCallback;

			MyClosestRayResultCallback rayCallback(from,to);

			m_dynamicsWorld->rayTest(from, to, rayCallback);

			if (rayCallback.hasHit())
			{

				btRigidBody* body = btRigidBody::upcast(rayCallback.m_collisionObject);
				if (body && body->hasContactResponse())
				{		

					//result.m_hitNormalInWorld = rayCallback.m_hitNormalWorld;
					//result.m_hitNormalInWorld.normalize();

					result.m_hitNormalInWorld = InterpolateMeshNormal(
						rayCallback.m_collisionObject->getWorldTransform(), // transform is the mesh shape's world transform
						rayCallback.m_collisionObject->getCollisionShape(), 
						rayCallback.m_hitShapePart,					//subpart
						rayCallback.m_hitTriangleIndex,					//triangle
						rayCallback.m_hitPointWorld); // position is the world space hit point of the ray

					result.m_hitPointInWorld = rayCallback.m_hitPointWorld;
					
					result.m_distFraction = rayCallback.m_closestHitFraction;
					return body;
				}
			}
			return 0;
		}
	};

	class MyFrictionConstraint : public btTypedConstraint 
	{

	protected:
		btRaycastVehicle* vehicle;

	public:

		MyFrictionConstraint(btRigidBody& body) : btTypedConstraint(CONTACT_CONSTRAINT_TYPE, body)
		{
			m_rbA = body;
		}

		virtual ~MyFrictionConstraint()
		{			
		}

		void setVehicle(btRaycastVehicle* _vehicle)
		{
			vehicle = _vehicle;
		}

		void getInfo1(btConstraintInfo1* info)
		{
			// Add two constraint rows for each wheel on the ground
			info->m_numConstraintRows = 0;
			for (int i = 0; i < vehicle->getNumWheels(); ++i)
			{
				const btWheelInfo& wheel_info = vehicle->getWheelInfo(i);
				info->m_numConstraintRows += 2 * (wheel_info.m_raycastInfo.m_groundObject != NULL);
			}
		}

		void getInfo2(btConstraintInfo2* info)
		{
			const btRigidBody* chassis = vehicle->getRigidBody();

			int row = 0;
			// Setup sideways friction.
			for (int i = 0; i < vehicle->getNumWheels(); ++i)
			{
				const btWheelInfo& wheel_info = vehicle->getWheelInfo(i);

				// Only if the wheel is on the ground:
				if (!wheel_info.m_raycastInfo.m_groundObject)
					continue;

				int row_index = row++ * info->rowskip;

				// Set axis to be the direction of motion:
				const btVector3& axis = wheel_info.m_raycastInfo.m_wheelAxleWS;
				for (int i = 0; i<3; i++)
					info->m_J1linearAxis[row_index + i] = axis[i];

				// Set angular axis.
				btVector3 rel_pos = wheel_info.m_raycastInfo.m_contactPointWS - chassis->getCenterOfMassPosition();
				for (int i = 0; i < 3; i++)
					info->m_J1angularAxis[row_index + i] = rel_pos.cross(axis)[i];

				// Set constraint error (target relative velocity = 0.0)
				info->m_constraintError[row_index] = 0.0f;

				info->cfm[row_index] = WHEEL_FRICTION_CFM; // Set constraint force mixing
			
				// Set maximum friction force according to Coulomb's law
				// Substitute Pacejka here
				btScalar max_friction = wheel_info.m_wheelsSuspensionForce * wheel_info.m_frictionSlip / info->fps;
				// Set friction limits.
				info->m_lowerLimit[row_index] = -max_friction;
				info->m_upperLimit[row_index] =  max_friction;
			}

			// Setup forward friction.
			for (int i = 0; i < vehicle->getNumWheels(); ++i)
			{
				const btWheelInfo& wheel_info = vehicle->getWheelInfo(i);

				// Only if the wheel is on the ground:
				if (!wheel_info.m_raycastInfo.m_groundObject)
					continue;

				int row_index = row++ * info->rowskip;

				// Set axis to be the direction of motion:
				btVector3 axis = wheel_info.m_raycastInfo.m_wheelAxleWS.cross(wheel_info.m_raycastInfo.m_wheelDirectionWS);
				for (int i = 0; i<3; i++)
					info->m_J1linearAxis[row_index + i] = axis[i];

				// Set angular axis.
				btVector3 rel_pos = wheel_info.m_raycastInfo.m_contactPointWS - chassis->getCenterOfMassPosition();
				for (int i = 0; i<3; i++)
					info->m_J1angularAxis[row_index + i] = rel_pos.cross(axis)[i];

				// FIXME: Calculate the speed of the contact point on the wheel spinning.
				//        Estimate the wheel's angular velocity = m_deltaRotation
				btScalar wheel_velocity = wheel_info.m_deltaRotation * wheel_info.m_wheelsRadius;
				// Set constraint error (target relative velocity = 0.0)
				info->m_constraintError[row_index] = wheel_velocity;

				info->cfm[row_index] = WHEEL_FRICTION_CFM; // Set constraint force mixing

				// Set maximum friction force
				btScalar max_friction = wheel_info.m_wheelsSuspensionForce * wheel_info.m_frictionSlip / info->fps;
				// Set friction limits.
				info->m_lowerLimit[row_index] = -max_friction;
				info->m_upperLimit[row_index] =  max_friction;
			}

		}
	};	
}

#endif // _VehicleImprovement_H_
