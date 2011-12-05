#ifndef _SKIDMARK_H_
#define _SKIDMARK_H_

#include "OgreSimpleRenderable.h"
#include "OgreHardwareBufferManager.h"
#include "Camera.h"

using namespace Ogre;
namespace RacingGame
{
	class SkidMark : public SimpleRenderable
	{

	public:

		SkidMark();
		~SkidMark();

		void init(size_t maxCount);

		void addPoints(Vector3, Vector3);
		void addPoint(Vector3);

		/// Implementation of Ogre::SimpleRenderable
		virtual Ogre::Real getBoundingRadius(void) const;
		/// Implementation of Ogre::SimpleRenderable
		virtual Ogre::Real getSquaredViewDepth(const Ogre::Camera* cam) const;

		void setAABB(Vector3 min, Vector3 max)
		{
			vaabMin = min;
			vaabMax = max;
		}

	private:
		size_t index;
		size_t capsity;	

		Vector3 vaabMin;
		Vector3 vaabMax;
	};
}
#endif //_SKIDMARK_H_