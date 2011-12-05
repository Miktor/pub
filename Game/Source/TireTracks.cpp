#include "TireTracks.h"

using namespace Ogre;

namespace RacingGame
{
	TierTracks::TierTracks(Ogre::SceneNode* node)
	{	
		static size_t Counter;

		for(int i = 0; i < 4; i++)
		{
			tracks[i] = new SkidMark();
			tracks[i]->init(MAXTRACKCOUNT);

			node->attachObject(tracks[i]);
			interapted[i] = true;
		}
	};

	TierTracks::~TierTracks()
	{
		for(int i = 0; i < 4; i++)
		{
			delete tracks[i];
		}
		//for(int i = 0; i < 4; i++)
		//	tracks[i].track->clearAllChains();
		//delete tiretracks;
	};

	void TierTracks::getTexCoord(Vector2* rez, btVector3& r1, btVector3& r2, btVector3& r3)
	{
		btVector3 tex_scale, rot_x, vt, norm;
		btMatrix3x3 rot;

		norm = (r2 - r1).cross(r3 - r2).normalize();

		rot[0] = (r2 - r1).normalize();
		rot[1] = norm.cross((r2 - r1).normalize());
		rot[2] = norm;

		rot = rot.inverse();

		tex_scale = btVector3(1, 1, 1) * 1;

		vt = (rot * r1) / tex_scale;   rez[0] = Vector2(vt.getX(), vt.getY());
		vt = (rot * r2) / tex_scale;   rez[1] = Vector2(vt.getX(), vt.getY());
		vt = (rot * r3) / tex_scale;   rez[2] = Vector2(vt.getX(), vt.getY());
	}

	void TierTracks::update(btRaycastVehicle* vehicle)
	{	

		for (int i =0; i < vehicle->getNumWheels(); i++)
		{
			btWheelInfo& wInfo = vehicle->getWheelInfo(i);
			if(wInfo.m_raycastInfo.m_groundObject != NULL && wInfo.m_skidInfo < 1. && wInfo.m_skidInfo > .01)
			{				
				btVector3 wCP = wInfo.m_raycastInfo.m_contactPointWS + btVector3(0, 0.001, 0);
				btVector3 toRight = quatRotate(wInfo.m_worldTransform.getRotation(), btVector3(0.3 / 2 , 0, 0));				

				//Vector3 norm = BtOgre::Convert::toOgre(wInfo.m_raycastInfo.m_contactNormalWS).normalisedCopy();

				/*if (tracks[i].lastCoords[0].distance2(wCP + toForward - toRight) < 0.2)
					return;*/

			
				//Vector2* tCor = new Vector2[3];
				//getTexCoord(tCor, wCP + orig, tracks[i].lastCoords[0], tracks[i].lastCoords[1]);

				if(interapted)
					tracks[i]->setAABB(BtOgre::Convert::toOgre(wCP), BtOgre::Convert::toOgre(wCP));

				tracks[i]->addPoints(BtOgre::Convert::toOgre(wCP + toRight), BtOgre::Convert::toOgre(wCP - toRight));				

				/*tracks[i].haveInterup = false;
			} else
				tracks[i].haveInterup = true;*/
			}
		}
	}
}