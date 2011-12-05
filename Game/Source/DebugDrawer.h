#ifndef DebugDrawer_H_
#define DebugDrawer_H_

#include "stdafx.h"
#include "DynamicLines.h"

namespace BtOgre
{
	class DebugDrawer : public btIDebugDraw
	{
	protected:
		Ogre::SceneNode *mNode;
		btDynamicsWorld *mWorld;
		DynamicLines *mLineDrawer;
		bool mDebugOn;

	public:

		DebugDrawer(Ogre::SceneNode *node, btDynamicsWorld *world);

		~DebugDrawer();

		void step();

		void drawLine(const btVector3& from,const btVector3& to,const btVector3& color);

		void drawContactPoint(const btVector3& PointOnB,const btVector3& normalOnB,btScalar distance,int lifeTime,const btVector3& color);

		void reportErrorWarning(const char* warningString);

		void draw3dText(const btVector3& location,const char* textString);

		//0 for off, anything else for on.
		void setDebugMode(int isOn);

		//0 for off, anything else for on.
		int	getDebugMode() const;
	};
}

#endif