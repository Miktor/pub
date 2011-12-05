#ifndef PHYSICS_H
#define PHYSICS_H

#ifndef _PRECOMP
#define _PRECOMP
#endif

#include "stdafx.h"
#include "DebugDrawer.h"

using namespace Ogre;

namespace RacingGame
{
	class Physics
	{
	public:
		Physics(Ogre::SceneManager* _mSceneMgr);
		~Physics();

		void initWorld (const Ogre::Vector3 &gravityVector = Ogre::Vector3 (0,-9.81,0), const Ogre::AxisAlignedBox &bounds = Ogre::AxisAlignedBox (Ogre::Vector3 (-10000, -10000, -10000),	Ogre::Vector3 (10000,  10000,  10000)));

		void AddCar(std::string name);
		void addGround();
		btDiscreteDynamicsWorld* getWorld() {return mWorld;}

		bool frameStarted(const Ogre::FrameEvent &evt);
		bool frameRenderingQueued(const Ogre::FrameEvent &evt);
		bool frameEnded(const Ogre::FrameEvent &evt);

		void setDebugType( int type)
		{
			physicsDebug->setDebugMode(type);		
		}

	protected:

		Ogre::SceneManager* mSceneMgr;

		//OgreBulletCollisions::DebugDrawer *debugDrawer;
		//OgreDebugDrawer* mDebug;

		btDiscreteDynamicsWorld *mWorld;
		btDefaultCollisionConfiguration* collisionConfiguration;
		btCollisionDispatcher* dispatcher;
		btBroadphaseInterface* overlappingPairCache;
		btSequentialImpulseConstraintSolver* solver;

		BtOgre::DebugDrawer* physicsDebug;
		size_t mNumEntitiesInstanced;

		//std::deque<Ogre::Entity *>                          mEntities;
		//std::deque<OgreBulletDynamics::RigidBody *>         mBodies;
		//std::deque<OgreBulletCollisions::CollisionShape *>  mShapes;
	};
}
#endif