#ifndef BulletOgreConv_H_
#define BulletOgreConv_H_

#include "OgreVector3.h"
#include "OgreQuaternion.h"

#include "LinearMath/btQuaternion.h"
#include "LinearMath/btVector3.h"

namespace BtOgre
{
	enum
	{
		POSITION_BINDING,
		TEXCOORD_BINDING
	};
	typedef std::vector<Ogre::Vector3> Vector3Array;

	class Convert
	{
	public:
		Convert() {};
		~Convert() {};

		static btQuaternion toBullet(const Ogre::Quaternion &q)
		{
			return btQuaternion(q.x, q.y, q.z, q.w);
		}
		static btVector3 toBullet(const Ogre::Vector3 &v)
		{
			return btVector3(v.x, v.y, v.z);
		}

		static Ogre::Quaternion toOgre(const btQuaternion &q)
		{
			return Ogre::Quaternion(q.w(), q.x(), q.y(), q.z());
		}
		static Ogre::Vector3 toOgre(const btVector3 &v)
		{
			return Ogre::Vector3(v.x(), v.y(), v.z());
		}
	};
}

#endif