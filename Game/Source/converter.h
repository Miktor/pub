#include "OgreVector3.h"
#include "OgreQuaternion.h"
#include "LinearMath/btQuaternion.h"
#include "LinearMath/btVector3.h"

class Converter
{
public:
	Converter(){};
	~Converter(){};

	static btVector3 to(const Ogre::Vector3 &V)
	{
		return btVector3(V.x, V.y, V.z);
	};

	static btQuaternion to(const Ogre::Quaternion &Q)
	{
		return btQuaternion(Q.x, Q.y, Q.z, Q.w);
	};

	static Ogre::Vector3 to(const btVector3 &V)
	{
		return Ogre::Vector3(V.x(), V.y(), V.z());
	};

	static Ogre::Quaternion to(const btQuaternion &Q)
	{
		return Ogre::Quaternion(Q.w(), Q.x(), Q.y(), Q.z());
		//return Ogre::Quaternion(Q.x(), Q.y(), Q.z(), Q[3]);
	};
};