#ifndef TIERTRACKS_H_
#define TIERTRACKS_H_

#include "stdafx.h"
#include "SkidMark.h"
#include "OgreSceneManager.h"

namespace RacingGame
{
	#define MAXTRACKCOUNT 300
	class TierTracks
	{		
	public:
		TierTracks(Ogre::SceneNode*);
		~TierTracks();

		void update(btRaycastVehicle* vehicle);
	protected:

		void getTexCoord(Ogre::Vector2* rez, btVector3& r1, btVector3& r2, btVector3& r3);		

		SkidMark* tracks[4];
		bool interapted[4];
	};
}
#endif