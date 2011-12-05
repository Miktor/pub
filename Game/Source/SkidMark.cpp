#include "SkidMark.h"

namespace RacingGame
{
	SkidMark::SkidMark() : index(0), capsity(0)
	{	
		
	}

	SkidMark::~SkidMark()
	{
		delete mRenderOp.vertexData;
	}

	void SkidMark::init(size_t maxCapsity)
	{		
		capsity = maxCapsity;

		mRenderOp.vertexData = new Ogre::VertexData();
		mRenderOp.indexData = 0;	
		mRenderOp.vertexData->vertexCount = maxCapsity;
		mRenderOp.vertexData->vertexStart = 0;
		mRenderOp.operationType = Ogre::RenderOperation::OT_TRIANGLE_STRIP;
		mRenderOp.useIndexes = false;	

		Ogre::VertexDeclaration* decl = mRenderOp.vertexData->vertexDeclaration;
		Ogre::VertexBufferBinding* bind = mRenderOp.vertexData->vertexBufferBinding;

		decl->addElement(0, 0, Ogre::VET_FLOAT3, Ogre::VES_POSITION);

		Ogre::HardwareVertexBufferSharedPtr vbuf = Ogre::HardwareBufferManager::getSingleton().createVertexBuffer(
			decl->getVertexSize(0), mRenderOp.vertexData->vertexCount, Ogre::HardwareBuffer::HBU_DYNAMIC, false);

		// Bind buffer
		bind->setBinding(0, vbuf);	

		this->setCastShadows(false);
		//this->setQueryFlags(QF_UNKNOWN); // set a query flag to exlude from queries (if necessary).
		this->setMaterial("TireTracks");
	}

	void SkidMark::addPoints(Vector3 right, Vector3 left)
	{
		addPoint(left);
		addPoint(right);

		mBox.setExtents(vaabMin, vaabMax);
	}

	void SkidMark::addPoint(Vector3 vec)
	{
		if(index == capsity)
			index = 0;
		Ogre::HardwareVertexBufferSharedPtr vbuf = mRenderOp.vertexData->vertexBufferBinding->getBuffer(0);
		Ogre::Real* pPos = static_cast<Ogre::Real*>(vbuf->lock(index * 4, 12, Ogre::HardwareBuffer::HBL_NORMAL));

		pPos[0] = vec.x;
		pPos[1] = vec.y;
		pPos[2] = vec.z;

		vbuf->unlock();

		index += 3;

		if(vec.x < vaabMin.x)
			vaabMin.x = vec.x;
		if(vec.y < vaabMin.y)
			vaabMin.y = vec.y;
		if(vec.z < vaabMin.z)
			vaabMin.z = vec.z;

		if(vec.x > vaabMax.x)
			vaabMax.x = vec.x;
		if(vec.y > vaabMax.y)
			vaabMax.y = vec.y;
		if(vec.z > vaabMax.z)
			vaabMax.z = vec.z;
		
	}

	Real SkidMark::getBoundingRadius(void) const
	{
		return Math::Sqrt(std::max(mBox.getMaximum().squaredLength(), mBox.getMinimum().squaredLength()));
	}
	//------------------------------------------------------------------------------------------------
	Real SkidMark::getSquaredViewDepth(const Camera* cam) const
	{
		Vector3 vMin, vMax, vMid, vDist;
		vMin = mBox.getMinimum();
		vMax = mBox.getMaximum();
		vMid = ((vMax - vMin) * 0.5) + vMin;
		vDist = cam->getDerivedPosition() - vMid;

		return vDist.squaredLength();
	}
}