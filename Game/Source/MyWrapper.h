#include "converter.h"
#include "btBulletDynamicsCommon.h"
#include "Ogre.h"

class MyMotionState : public btMotionState {
public:
	MyMotionState(const btTransform &initialpos, Ogre::SceneNode *node) {
		mVisibleobj = node;
		mPos1 = initialpos;
	}

	virtual ~MyMotionState() {
	}

	void setNode(Ogre::SceneNode *node) {
		mVisibleobj = node;
	}

	virtual void getWorldTransform(btTransform &worldTrans) const {
		worldTrans = mPos1;
	}

	virtual void setWorldTransform(const btTransform &worldTrans) {
		if(NULL == mVisibleobj) 
			return;
		mVisibleobj->setOrientation(Converter::to(worldTrans.getRotation()));
		mVisibleobj->setPosition(Converter::to(worldTrans.getOrigin()));
	}

protected:
	Ogre::SceneNode *mVisibleobj;
	btTransform mPos1;
};