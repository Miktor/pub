#include "Physics.h"
#include "DebugDrawer.h"
#include "Object.h"

namespace RacingGame
{
	Physics::~Physics()
	{
		delete mWorld;
	}

	Physics::Physics(Ogre::SceneManager* _mSceneMgr)
	{
		mNumEntitiesInstanced = 0;

		mSceneMgr=_mSceneMgr;
	}
	// -------------------------------------------------------------------------
	void Physics::initWorld(const Ogre::Vector3 &gravityVector, const Ogre::AxisAlignedBox &bounds)
	{
		collisionConfiguration = new btDefaultCollisionConfiguration();
		///use the default collision dispatcher. For parallel processing you can use a diffent dispatcher (see Extras/BulletMultiThreaded)
		dispatcher = new btCollisionDispatcher(collisionConfiguration);
		///btDbvtBroadphase is a good general purpose broadphase. You can also try out btAxis3Sweep.
		overlappingPairCache = new btDbvtBroadphase();
		///the default constraint solver. For parallel processing you can use a different solver (see Extras/BulletMultiThreaded)
		solver = new btSequentialImpulseConstraintSolver;

		mWorld = new btDiscreteDynamicsWorld(dispatcher,overlappingPairCache,solver,collisionConfiguration);
		mWorld->setGravity(btVector3(0,-9.8,0));

	#ifdef DEBUG
		physicsDebug = new BtOgre::DebugDrawer(mSceneMgr->getRootSceneNode(), mWorld);
		physicsDebug->setDebugMode(0);
		mWorld->setDebugDrawer(physicsDebug);
	#endif// DEBUG

		Entity* mGroundEntity = mSceneMgr->createEntity("groundEntity", "groundMesh.mesh");
		mGroundEntity->setMaterialName("apshalt");
		SceneNode* node = mSceneMgr->getRootSceneNode()->createChildSceneNode("groundNode");
		node->attachObject(mGroundEntity);

		BtOgre::StaticMeshToShapeConverter converter2(mGroundEntity);
		btCollisionShape* mGroundShape = converter2.createTrimesh();

		btDefaultMotionState* groundState = new btDefaultMotionState(btTransform(btQuaternion(0,0,0,1),btVector3(0,0,0)));

		//Create the Body.
		btRigidBody* mGroundBody = new btRigidBody(0, groundState, mGroundShape, btVector3(0,0,0));
		mWorld->addRigidBody(mGroundBody);

	
	}

	bool Physics::frameStarted(const Ogre::FrameEvent &evt)
	{
		mWorld->stepSimulation(evt.timeSinceLastFrame, 5);
	#ifdef DEBUG
		physicsDebug->step();
	#endif

		return true;
	}
	bool Physics::frameRenderingQueued(const Ogre::FrameEvent &evt){ return true;}
	bool Physics::frameEnded(const Ogre::FrameEvent &evt) { return true;}
	// -------------------------------------------------------------------------
	void Physics::addGround()
	{
	}
	// -------------------------------------------------------------------------

	// -------------------------------------------------------------------------
}