#ifndef _myMotionState_H_
#define _myMotionState_H_

#include "Ogre.h"
#include "btBulletDynamicsCommon.h"

namespace BtOgre
{
	typedef std::vector<Ogre::Vector3> Vector3Array;

	class myMotionState : public btMotionState
	{
	protected:
		btTransform mTransform;
		btTransform mCenterOfMassOffset;

		Ogre::SceneNode *mNode;

	public:
		myMotionState(Ogre::SceneNode *node, const btTransform &transform, const btTransform &offset = btTransform::getIdentity())		: mTransform(transform), mCenterOfMassOffset(offset), mNode(node)
		{
		}

		myMotionState(Ogre::SceneNode *node)		: mTransform(((node != NULL) ? Convert::toBullet(node->getOrientation()) : btQuaternion(0,0,0,1)),
			((node != NULL) ? Convert::toBullet(node->getPosition())    : btVector3(0,0,0))),
			mCenterOfMassOffset(btTransform::getIdentity()), mNode(node)
		{
		}

		virtual void getWorldTransform(btTransform &ret) const
		{
			ret = mCenterOfMassOffset.inverse() * mTransform;
		}

		virtual void setWorldTransform(const btTransform &in)
		{
			if (mNode == NULL)
				return;

			mTransform = in;
			btTransform transform = in * mCenterOfMassOffset;

			btQuaternion rot = transform.getRotation();
			btVector3 pos = transform.getOrigin();
			mNode->setOrientation(rot.w(), rot.x(), rot.y(), rot.z());
			mNode->setPosition(pos.x(), pos.y(), pos.z());
		}

		void setNode(Ogre::SceneNode *node)
		{
			mNode = node;
		}
	};
}

#endif