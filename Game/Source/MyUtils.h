#ifndef _MyUtils_H_
#define _MyUtils_H_

#include "btBulletDynamicsCommon.h"

namespace RacingGame
{
	class MyUtils
	{
	public:

		static void getTriangleConers(btCollisionShape* shape, int subpart, btVector3* pos, btVector3* normals, unsigned short* indices)
		{
			btStridingMeshInterface * meshInterface = NULL;

			/*if (shape->getShapeType() == GIMPACT_SHAPE_PROXYTYPE)
			{
				meshInterface = (static_cast<btGImpactMeshShape*>(shape))->getMeshInterface();
			}
			else */if (shape->getShapeType() == TRIANGLE_MESH_SHAPE_PROXYTYPE)
			{
				meshInterface = (static_cast<btBvhTriangleMeshShape*>(shape))->getMeshInterface();
			}
			else
			{
				return;
			}

			if (!meshInterface) false;

			unsigned char *vertexbase;
			int numverts;
			PHY_ScalarType type;
			int stride;
			unsigned char *indexbase;
			int indexstride;
			int numfaces;
			PHY_ScalarType indicestype;

			meshInterface->getLockedVertexIndexBase(
				&vertexbase,
				numverts,
				type,
				stride,
				&indexbase,
				indexstride,
				numfaces,
				indicestype,
				subpart);

			unsigned int * gfxbase = (unsigned int*)(indexbase);
			const btVector3 & meshScaling = shape->getLocalScaling();

			for (int j=2;j>=0;j--)
			{
				indices[j] = indicestype == PHY_SHORT ? ((unsigned short*)gfxbase)[j] : gfxbase[j];

				btScalar * graphicsbase = (btScalar*)(vertexbase + indices[j] * stride);

				pos[j] = btVector3(graphicsbase[0]*meshScaling.getX(),
					graphicsbase[1]*meshScaling.getY(),
					graphicsbase[2]*meshScaling.getZ());	
			}

			*normals = (pos[2]-pos[0]).cross(pos[1] - pos[0]);
			*normals->normalize();

			meshInterface->unLockVertexBase(0);	

		}

		static btVector3* getSubPartNormals (const btVector3* triangle)
		{
			return &(triangle[1] - triangle[0]).cross((triangle[2] - triangle[0])).normalized();
		}

		static btVector3* getSubPartPosition (btCollisionShape* shape, int subpart)
		{
			btVector3* pos;
			return pos;
		}

		static unsigned short* getSubPartIndices (btCollisionShape* shape, int subpart)
		{
			unsigned short* indices;			

			return indices;
		}
	};
}
#endif // _MyUtils_H_
