#ifndef Object_H_
#define Object_H_

#include "stdafx.h"

using namespace Ogre;

namespace RacingGame
{
	class Object
	{
	public:

		virtual void init(std::string _name)
		{
			name = _name;
		}
		virtual void update(const float time){ return;}

		SceneNode* getGraphics()			{return mNode;}
		void setGraphics(SceneNode* _mNode) {mNode = _mNode;}

		btRigidBody* getPhysics()			 {return mBody;}
		void setPhysics(btRigidBody* _mBody) {mBody = _mBody;}

		std::string name;
		bool loaded;

	protected:
		
		SceneNode*		mNode;
		btRigidBody*	mBody;
	};

	class UpdatableObject
	{
	public:
		virtual void update(Ogre::Real deltat)
		{

		}
	};
	class StaticObject : virtual public Object
	{
	public:
		StaticObject()
		{
		}
		~StaticObject()
		{
		}

		void init(std::string _name)
		{
			name = _name;
			loaded = true;
		}

		void update(const float time)
		{
			if (!loaded)
				return;
		}
	};

	class LandskaypeObject : virtual public StaticObject
	{
	public:
		LandskaypeObject()
		{
		}
		~LandskaypeObject()
		{
		}

		void init(std::string _name)
		{
			name = _name;
			loaded = true;
		}

		void update(const float time)
		{
			if (!loaded)
				return;
		}
	};

	class DynamicObject : virtual public Object
	{
	public:

		DynamicObject()
		{
		}
		~DynamicObject()
		{
		}

		void init(std::string _name)
		{
			name = _name;
			loaded = true;
		}

		void update(const float time)
		{
			if (!loaded)
				return;
		}
	};

	class VehicleObject : virtual public DynamicObject
	{
	public:

		VehicleObject()
		{
		}
		~VehicleObject()
		{
		}

		void init(std::string _name, btDiscreteDynamicsWorld* mWorld, Ogre::SceneManager* mSceneMgr)
		{
			name = _name;
			loaded = true;
		}

		void update(const float time)
		{
			if (!loaded)
				return;
		}

		btRaycastVehicle* getVehicle()			{ return mVehicle;}
		void setVehicle(btRaycastVehicle* _mVehicle) {mVehicle = _mVehicle;}
	protected:
		btRaycastVehicle* mVehicle;
	};

	class WheelObject : virtual public DynamicObject
	{
	public:

		WheelObject()
		{
		}
		~WheelObject()
		{
		}

		void init(std::string _name)
		{
			name = _name;
			loaded = true;
		}

		void update(const float time)
		{
			if (!loaded)
				return;
		}
	};
}

#endif