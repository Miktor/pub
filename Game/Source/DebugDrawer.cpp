#include "DebugDrawer.h"
#include "DynamicLines.h"

using namespace Ogre;

namespace BtOgre
{
	DebugDrawer::DebugDrawer(Ogre::SceneNode *node, btDynamicsWorld *world)
		: mNode(node),
		mWorld(world),
		mDebugOn(true)
	{
		mLineDrawer = new DynamicLines(Ogre::RenderOperation::OT_LINE_LIST);
		mNode->attachObject(mLineDrawer);

		if (!Ogre::ResourceGroupManager::getSingleton().resourceGroupExists("BtOgre"))
			Ogre::ResourceGroupManager::getSingleton().createResourceGroup("BtOgre");
		if (!Ogre::MaterialManager::getSingleton().resourceExists("BtOgre/DebugLines"))
		{
			Ogre::MaterialPtr mat = Ogre::MaterialManager::getSingleton().create("BtOgre/DebugLines", "BtOgre");
			mat->setReceiveShadows(false);
			mat->setSelfIllumination(1,1,1);
		}

		mLineDrawer->setMaterial("BtOgre/DebugLines");
	}

	DebugDrawer::~DebugDrawer()
	{
		Ogre::MaterialManager::getSingleton().remove("BtOgre/DebugLines");
		Ogre::ResourceGroupManager::getSingleton().destroyResourceGroup("BtOgre");
		delete mLineDrawer;
	}

	void DebugDrawer::step()
	{
		if (mDebugOn)
		{
			mWorld->debugDrawWorld();
			mLineDrawer->update();
			mNode->needUpdate();
			mLineDrawer->clear();
		}
		else
		{
			mLineDrawer->clear();
			mLineDrawer->update();
			mNode->needUpdate();
		}
	}

	void DebugDrawer::drawLine(const btVector3& from,const btVector3& to,const btVector3& color)
	{
		mLineDrawer->addPoint(Convert::toOgre(from));
		mLineDrawer->addPoint(Convert::toOgre(to));
	}

	void DebugDrawer::drawContactPoint(const btVector3& PointOnB,const btVector3& normalOnB,btScalar distance,int lifeTime,const btVector3& color)
	{
		mLineDrawer->addPoint(Convert::toOgre(PointOnB));
		mLineDrawer->addPoint(Convert::toOgre(PointOnB) + (Convert::toOgre(normalOnB) * distance * 20));
	}	

	void DebugDrawer::reportErrorWarning(const char* warningString)
	{
		Ogre::LogManager::getSingleton().logMessage(warningString);
	}

	void DebugDrawer::draw3dText(const btVector3& location,const char* textString)
	{
	}

	//0 for off, anything else for on.
	void DebugDrawer::setDebugMode(int isOn)
	{
		mDebugOn = (isOn == 0) ? false : true;

		if (!mDebugOn)
			mLineDrawer->clear();
		else
			mNode->setVisible(true);
	}

	//0 for off, anything else for on.
	int	DebugDrawer::getDebugMode() const
	{
		return mDebugOn;
	}
}