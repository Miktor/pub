#ifndef STDAFX_H_
#define STDAFX_H_

#include <windows.h>
#include <vector>
#include <map>
#include <string>
#include "math.h"

#include "Ogre.h"

#include "MyGUI.h"
#include "MyGUI_OgrePlatform.h"

#include "OIS.h"

#include "OgreMaxScene.hpp"
#include "tinyxml.h"

#include "btBulletDynamicsCommon.h"
#include "BulletOgreConv.h"
#include "myMotionState.h"

#include "BulletOgreConv.h"
#include "BtOgreGP.h"
#include "BtOgrePG.h"

#include "AppState.h"

#include "StatisticInfo.h"

#include "Object.h"

typedef void* (*upFuncReal) (Ogre::Real a);

typedef std::map<std::string, float> floatMap;
typedef std::map<std::string, int> intMap;
typedef std::map<std::string, btVector3> vector3Map;
typedef std::map<std::string, std::string> stringMap;

#endif